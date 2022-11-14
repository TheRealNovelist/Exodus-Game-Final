using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/*
 
 */
public class PlayerMove1 : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    [SerializeField]private float walkSpeed;
    [SerializeField]private float sprintingSpeed;
    [SerializeField]private float groundDrag;
    [SerializeField]private float airDrag;
    [SerializeField] private float jumpForce;
    [SerializeField]private float jumpCooldown;
    [SerializeField]private float airMultiplier;
    private bool readyToJump = true;
    
    [Header("Crouching")] 
    [SerializeField]private float crouchSpeed;
    [SerializeField]private float crouchYScale;
    [SerializeField]private float startYScale;
    
    [Header("Keybinds")] 
    [SerializeField]private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField]private KeyCode crouchKey = KeyCode.LeftControl;
    
    [Header("Ground check")] 
    [SerializeField]private float playerHeight;
    [SerializeField]private LayerMask whatisGround;
    [SerializeField] private bool isGrounded;
   
    [Header("Slope Handling")]
    [SerializeField]private float maxSlopeAngle;
    private RaycastHit slopeHit;
    private float anglePlayerOn;
    
    [SerializeField]private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    public Vector3 lastMoveDirection; //Use for ability holder

    private Rigidbody rb;

    [SerializeField]private MovementState state;

    public bool recieveInput = true;

    public bool isInAir = false;    //called in jump input, also referenced in DoubleJump.cs, edited by Minh
    public enum MovementState
    {
        walking,
        crouching,
        sprinting,
        air
    }
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        MyInput();
        GroundCheck();
        StateHandler();
        doubleJump();
      //  Debug.Log(rb.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        Move();
        SpeedControl();
        
        //draw raycast for jump
        Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f),Color.yellow);
        
    }

    public void doubleJump()
    {
        if (!isGrounded) 
        {
            isInAir = true;
        }
        else
        {
            isInAir = false;
        }
    }
    void MyInput()
    {
        if(!recieveInput){return;}
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
           //check when to jump
        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
        //else
        //{
        //    isInAir = false;
        //}

        if (horizontalInput != 0 || verticalInput != 0)
        {
            lastMoveDirection = moveDirection;
        }
        
        
    }

    private void StateHandler()
    {
        //Mode - Spriting
        if (isGrounded && Input.GetKey(sprintKey))
        {  
            moveSpeed = sprintingSpeed;
            state = MovementState.sprinting;
         
        }
        
        //Mode- crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        
        //Mode = Walking
        else if (isGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        //Mode - air
        else
        {
            state = MovementState.air;
        }
    }
    void Move()
    {
        if(!recieveInput){return;}
        
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        //turn off gravity while on slope
        rb.useGravity = !onSlope();
       
        //Refactor for clear code reading
        Vector3 slopeDir = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized; //get move direction on slope
        
        //on slope 
        if (onSlope())
        {
            rb.AddForce(slopeDir * moveSpeed, ForceMode.Force );

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 10f, ForceMode.Force);
            }
        }
        //on ground
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force );
        }
        //in air
        else if (!isGrounded)
        {
            //add force for player if they are in air
            rb.AddForce(moveDirection.normalized * (moveSpeed * airMultiplier), ForceMode.Force);
        }
    }

    void GroundCheck()
    {
        //ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);
        
       
        //apply drag
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else rb.drag = airDrag;
    }

    void SpeedControl()
    {
        //limiting speed on slope
        if (onSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * 1/anglePlayerOn;
            }
        }
        else 
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            //limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    void Jump()
    {
       //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    private bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            anglePlayerOn = Vector3.Angle(Vector3.up, slopeHit.normal);
            return anglePlayerOn < maxSlopeAngle && anglePlayerOn != 0;
        }

        return false;
    }
}
//code bo di co the dung laij trong tuonwg lai?
//Crouch, khong cho nguoi choi nhay
//and change player anmation
// if (Input.GetKey(crouchKey))
// {
//     transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
//     readyToJump = false;
// }
//push player down when they press the crouchKey just one 
// if (Input.GetKeyDown(crouchKey))
// {
//     rb.AddForce(Vector3.down * playerHeight , ForceMode.Force);
// }
//
//Stop Crouch, cho nguoi choi nhay
// if (Input.GetKeyUp(crouchKey))
// {
//     transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
//     readyToJump = true;
// }