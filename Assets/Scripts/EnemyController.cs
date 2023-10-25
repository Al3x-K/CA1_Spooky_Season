using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            new Vector2(moveSpeed + 10f, enemyRigidBody.velocity.y) :
            new Vector2(-moveSpeed - 10f, enemyRigidBody.velocity.y);
    }

    IEnumerator DelayDirectionChange()
    {
        canChangeDirection = false;
        yield return new WaitForSeconds(0.5f);
        canChangeDirection = true;
    }


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
            enemyHealth -= 1;
            if(enemyHealth <= 0)
            {
                Death();
            }
        }
    }
     public void Death()
     {
        anim.SetTrigger("Death");
        Destroy(gameObject);
     }

}
