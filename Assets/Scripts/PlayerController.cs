using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float jumpHeight = 20f;
    public float speed = 5f;
    public int health = 3;

    
    [Header("Ground Check")]
    public LayerMask whatIsGround; 
    public Transform groundCheckPoint; 
    public float groundCheckRadius = 0.2f;

    
    

    private Rigidbody2D body; 
    private Animator anim; 
    private bool grounded = true; 
    private bool canDoubleJump = false; 
     private bool facingRight = true; 
    // Start is called before the first frame update
   
    private void Awake()
    {
        InitializeComponents();
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
        HandleDeath();
        HandleDamage();
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
        grounded = false; 
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
    void Attack()
    {
        anim.SetTrigger("Attack");
    }
    private void HandleAttack()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    //Death Section
    void Die()
    {
        anim.SetTrigger("Death");
    }
    private void HandleDeath()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Die();
        }
    }
    //Hit Section
    void TookDamage()
    {
        anim.SetTrigger("Hit");
    }
    private void HandleDamage()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TookDamage();
        }
    }
    //Ground Section
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        grounded = true;
    }

    //Checks if the player is on the ground
    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
    }
}
