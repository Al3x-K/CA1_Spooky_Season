using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the script was taken from Naoise's class, wit most changes made in unity editor
//changes made:
//1. I changed the speed with which the enemies are moving around
//2. made an enemy take damage on collision if that was a player
public class EnemyController : MonoBehaviour
{
    public static EnemyController enemy;
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float groundCheckDistance = 2f;
    [SerializeField] private LayerMask whatIsGround;
    private bool movingRight = true; 
    private bool canChangeDirection = true;
    

    [Header("Combat Settings")]
    public int damage;
    [SerializeField] private int enemyHealth;
    [SerializeField] private int enemyMaxHealth = 5;

    private Rigidbody2D enemyRigidBody; 
    private EnemySpawner spawner; 
    private Animator anim;

   
    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector2 groundCheckPosition = movingRight ? //alternative to an if...else statement 
            new Vector2(transform.position.x + 0.5f, transform.position.y) :
            new Vector2(transform.position.x - 0.5f, transform.position.y);

        bool isGrounded = Physics2D.Raycast(groundCheckPosition, Vector2.down, groundCheckDistance, whatIsGround);

        if (!isGrounded && canChangeDirection) 
        {
            movingRight = !movingRight;
            StartCoroutine(DelayDirectionChange());
        }
    
        //sets velocity for movement
        enemyRigidBody.velocity = movingRight ?
            new Vector2(moveSpeed + 10f, enemyRigidBody.velocity.y) :
            new Vector2(-moveSpeed - 10f, enemyRigidBody.velocity.y);
    }

    //make an enemy have a slight delay before they move the other way 
    IEnumerator DelayDirectionChange()
    {
        canChangeDirection = false;
        yield return new WaitForSeconds(0.5f);
        canChangeDirection = true;
    }


    //access the spawner
    public void Initialize(EnemySpawner spawnerReference)
    {
        spawner = spawnerReference;
    }


    public void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDied();
        }
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemyHealth -= 1; //decreases enemy health on collision if the object was tagged as "Player"
            if(enemyHealth <= 0)
            {
                Destroy(gameObject); //destroys the object if the health got to 0
            }
        }
    }
     
}
