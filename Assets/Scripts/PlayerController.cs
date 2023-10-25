using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;

    [Header("Player Settings")]
    public float jumpHeight = 25f;
    public float speed = 20f;
    [SerializeField] public int health;
    [SerializeField] public int maxHealth = 10;

    
    [Header("Ground Check")]
    public LayerMask whatIsGround; 
    public Transform groundCheckPoint; 
    public float groundCheckRadius = 0.2f;

    
    
    [Header("Attack")]
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRange = 3f;


    public HealthDisplay healthDisplay;
    public LayerMask enemyLayers;  
    private Rigidbody2D body; 
    private Animator anim; 
    private bool grounded = true; 
    private bool canDoubleJump = false; 
    private bool facingRight = true; 
    // Start is called before the first frame update
   
    private void Awake()
    {
        InitializeComponents();
        health = maxHealth;
    }

    private void InitializeComponents()
    {
        body = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        grounded = CheckIfGrounded();
        HandleMovement();
    }

    void HandleInput()
    {
        HandleJump();
        HandleAttack();
    }

    //Movement Section
    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        anim.SetBool("Walk", horizontalInput !=0);
    
        if((horizontalInput>0&& !facingRight)|| (horizontalInput<0 && facingRight))
        {
            Flip();
        }
        if(horizontalInput != 0 && grounded)
        {
            AudioManager.instance.PlayFootstepSound();;
        }
    }

    private void Flip() 
    {
        Vector3 currentScale = gameObject.transform.localScale; 
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale; 
        facingRight = !facingRight; 
    }

    //Jump Section
    private void Jump()
    {          
        anim.SetTrigger("Jump");
        body.velocity = Vector2.up*jumpHeight; 
        AudioManager.instance.PlayJumpSound();
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&grounded)
        {
            Jump();
            canDoubleJump = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }   
    }

    //Attack Section
    void Attack(int attackDamage)
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                Debug.Log("Enemy Damaged!");
                AudioManager.instance.PlayAttackSound();
                health += 1;
            }

        }
    }
    private void HandleAttack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack(attackDamage);
        }
    }

    //Damage Section

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.GameOver();
        }
        AudioManager.instance.PlayHitSound();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            HandleAttack();
            TakeDamage(1);
        }
    }
    
    
    //Checks if the player is on the ground
    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
    }
}
