using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float groundCheckDistance = 2f;
    [SerializeField] private LayerMask whatIsGround;
    private bool movingRight = true; 
    private bool canChangeDirection = true;
    public float distance = 9f;
    public float distanceBetween = 10f;
    

    [Header("Combat Settings")]
    [SerializeField] public int maxHealth = 5; 
    private int currentHealth; 


    public GameObject player;
    private Rigidbody2D enemyRigidBody; 
    private EnemySpawner spawner; 

    // Start is called before the first frame update
    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Move();
    }

    public void Move()
    {
        Vector2 groundCheckPosition = movingRight ?
            new Vector2(transform.position.x + 0.5f, transform.position.y) :
            new Vector2(transform.position.x - 0.5f, transform.position.y);

        bool isGrounded = Physics2D.Raycast(groundCheckPosition, Vector2.down, groundCheckDistance, whatIsGround);

        if (!isGrounded && canChangeDirection)
        {
            movingRight = !movingRight;
            StartCoroutine(DelayDirectionChange());
        }
    
        enemyRigidBody.velocity = movingRight ?
            new Vector2(moveSpeed + 15f, enemyRigidBody.velocity.y) :
            new Vector2(-moveSpeed - 15f, enemyRigidBody.velocity.y);
    }

    IEnumerator DelayDirectionChange()
    {
        canChangeDirection = false;
        yield return new WaitForSeconds(0.5f);
        canChangeDirection = true;
    }

    public void Chase()
    {
        distance = Vector2.Distance(transform.position,player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if(distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void Initialize(EnemySpawner spawnerReference)
    {
        spawner = spawnerReference;
    }
}
