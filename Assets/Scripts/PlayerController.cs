using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
[Header("Player Settings")]
    public float jumpHeight = 7f;
    
    [Header("Ground Check")]
    public LayerMask whatIsGround; 
    public Transform groundCheckPoint; 
    public float groundCheckRadius = 0.2f;

    
    

    private Rigidbody2D body; 
    private Animator anim; 
    private bool grounded = true; 
    private bool canDoubleJump = false; 
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
        HandleJump();
    }
    
    
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

   private void OnCollisionEnter2D(Collision2D other) 
   {
        if(other.gameObject.CompareTag("Ground"))
        grounded = true;
   }

}
