using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerMovemente : MonoBehaviour
{
    public InputManager InputManager;
    public TextMeshProUGUI speed;
    [Header("Movment")]
    private float movementSpeed;
    public float walkSpeed;
    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public float fallmultiplayer = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public Transform orientation;

    [Header("SlipCheck")]
    public Vector2 wallCheck;
    public float slipSpeed = 10f;
    float horizontalInput, verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    private CapsuleCollider capsulcollider;
    public MovementState state;

    public enum MovementState
    {
        walking,
        air
       
    }
    public bool onLadder;
    private void Start()
    {
        InputManager = GetComponent<InputManager>();

        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        capsulcollider = GetComponent<CapsuleCollider>();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void Update()
    {
        if (!grounded)
        {
            SlipChecker();
        }
       // Если нужно проверить скорость 
       // speed.text = rb.velocity.magnitude.ToString();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        SpeedControl();
        StateHandler();
        MyInput();
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else rb.drag = 0;
        

    }
    private void MyInput()
    {
        

        if (InputManager.player.Jump.triggered && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

     
    }
    private void StateHandler()
    {
        
        
       
        if (grounded)
        {
            state = MovementState.walking;
            movementSpeed = walkSpeed;
       
     
        }
        if (!grounded)
        {
            state = MovementState.air;
        }

        

    }
    public void MovePlayer()
    {
        horizontalInput = InputManager.moveDirection.x;
        verticalInput = InputManager.moveDirection.y;
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
       
        if (OnSlope())
        {
            rb.AddForce(20f * movementSpeed * GetSlopeMoveDirection(), ForceMode.Force);
        }
        
        if (grounded) rb.AddForce(10F * movementSpeed * moveDirection.normalized, ForceMode.Force);
        else if(!grounded) rb.AddForce(10F * airMultiplier * movementSpeed * moveDirection.normalized, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);
        
        if(flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
   

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0f;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }


    private bool SlipChecker()
    {
        RaycastHit hit;
        Vector3 spawnPoint = transform.position + Vector3.up * wallCheck.y;

        Vector3 forward = transform.forward * wallCheck.x;
        Vector3 back = -transform.forward * wallCheck.x;
        Vector3 right = transform.right * wallCheck.x;
        Vector3 left = -transform.right * wallCheck.x;

        Ray front_ray = new Ray(spawnPoint, forward);
        Ray back_ray = new Ray(spawnPoint, back);
        Ray left_ray = new Ray(spawnPoint, left);
        Ray right_ray = new Ray(spawnPoint, right);

        float dis = wallCheck.y;
        
        if(Physics.Raycast(front_ray, out hit, dis, whatIsGround))
        {
            HitForSlip(transform.forward);
            return true;
        }
        if(Physics.Raycast(back_ray, out hit, dis, whatIsGround) ||
            Physics.Raycast(right_ray, out hit, dis, whatIsGround) ||
            Physics.Raycast(left_ray, out hit, dis, whatIsGround))
        {
            HitForSlip(hit.normal);
            return true;
        }
        return false;
    }

    void HitForSlip(Vector3 slipDir)
    {
        if (state == MovementState.air)
        {
            rb.velocity = Vector3.zero;
            state = MovementState.walking;
        }

        rb.AddForce((slipDir * slipSpeed) + Vector3.down);
    }

   







}

