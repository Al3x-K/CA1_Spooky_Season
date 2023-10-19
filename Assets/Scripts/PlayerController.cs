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
    private bool grounded; 
    private bool canDoubleJump = false; 
    // Start is called before the first frame update
    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        body = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    
    
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
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

}
