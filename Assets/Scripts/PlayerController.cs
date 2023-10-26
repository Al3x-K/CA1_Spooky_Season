using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The base of the script is the code taken from Naoise's class with changes
//CHANGES MADE:
//1. I adjusted the value of various variables
//2. Added a function OnTriggerEnter2D that ends the gaem 
//3. Added health regen to the attack function and changed it to be called on the mouse click
//4. Addedd TakeDamage function
//5. Changed OnCollisionEnter2D function to handle both player's and enemy's damage
public class PlayerController : MonoBehaviour
{
    public static PlayerController player; //it allows me to use things from this script in others

    [Header("Player Settings")]
    public float jumpHeight = 25f;
    public float speed = 20f;
    //SerializeField makes a private variable still accessible in Unity Editor
    [SerializeField] public int health; 
    [SerializeField] public int maxHealth = 10;

    
    [Header("Ground Check")] //needed for player movement
    public LayerMask whatIsGround; 
    public Transform groundCheckPoint; 
    public float groundCheckRadius = 0.2f;

    
    
    [Header("Attack")]
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRange = 3f;


    public HealthDisplay healthDisplay; //a reference to other script
    public LayerMask enemyLayers;  
    private Rigidbody2D body; 
    private Animator anim; 
    private bool grounded = true; 
    private bool canDoubleJump = false; 
    private bool facingRight = true; 
    
   
    private void Awake()
    {
        InitializeComponents();
        health = maxHealth;
        
    }

    //Gets components from Player GameObject
    private void InitializeComponents()
    {
        body = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
    }

    void Update() //called once per frame
    {
        HandleInput();
    }

    void FixedUpdate() //called once per physics frame
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
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //creates a variable and sets it to either "A" or "D" (or "Left" or "Right")
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); //sets velocity to the result of the calculation
        anim.SetBool("Walk", horizontalInput !=0); //sets a boolean to true, causing animation to change
    

        //checks what key is pressed. if it's either "D" or "Right" it sets facingRight to true if it was already facingRight nothing happens, if not, the image gets flipped
        //and if it's either "A" or "Left" it sets facingRight to false and flips the image of the sprite
        if((horizontalInput>0&& !facingRight)|| (horizontalInput<0 && facingRight))
        {
            Flip();
        }
        //checks if player is on the ground and plays the sound effect
        if(horizontalInput != 0 && grounded)
        {
            AudioManager.instance.PlayFootstepSound();; //access a function from another script
        }
    }

    private void Flip() //flips the image of the sprite
    {
        Vector3 currentScale = gameObject.transform.localScale; //gets the size of the sprite
        currentScale.x *= -1; //transforms it only on the x axis
        gameObject.transform.localScale = currentScale; //sets the previous scale to the new one
        facingRight = !facingRight; //sets the value to false
    }

    //Jump Section
    private void Jump()
    {          
        anim.SetTrigger("Jump"); //sets the trigger for animation to happen
        body.velocity = Vector2.up*jumpHeight; 
        AudioManager.instance.PlayJumpSound(); //access a function from another script
        }
    }

    private void HandleJump()
    {
        //goes through if player hits space and the player GameObject is on the ground
        if(Input.GetKeyDown(KeyCode.Space)&&grounded)
        {
            Jump(); //calls the jump function
            canDoubleJump = true;
        }
        //checks the doubleJump variable and goes through if it's true
        else if(Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }   
    }

    //Attack Section
    void Attack(int attackDamage)
    {
        anim.SetTrigger("Attack"); //sets the trigger for animation to happen
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers); //creates an array with position, the range and layer for enemies
        foreach (Collider2D enemy in hitEnemies) // goes through the whole array
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>(); //gets access to the enemyController script
            if (enemyController != null) //checks if there is a value
            {
                Debug.Log("Enemy Damaged!");
                AudioManager.instance.PlayAttackSound(); //access a function from another script
                health += 1; //health regen if you manage to hit an enemy before it hits you
            }

        }
    }
    private void HandleAttack()
    {
        //calls the function on mouse button click
        if(Input.GetMouseButtonDown(0))
        {
            Attack(attackDamage);
        }
    }

    //Damage Section

    public void TakeDamage(int amount)
    {
        health -= amount; //decreases player's health by the amount of taken damage
        if(health <= 0) //checks if player still has any health left and goes through if it's 0 or less
        {
            Destroy(gameObject); //destroys player object
            GameManager.instance.GameOver(); //loads game over scene
        }
        AudioManager.instance.PlayHitSound();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy") //checks if the object that player collided with is an enemy
        {
            HandleAttack(); //calls a function to attack the enemy 
            TakeDamage(1); //calls the function to get damage
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Door") //checks if the object that player collided with is tagged as a "door"
        {
            GameManager.instance.WinGame(); //loads win game scene
        }
    }
    
    
    //Checks if the player is on the ground
    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround); //checks if the player is on the ground by using the checkPoint and correct layer (ground)
    }
}
