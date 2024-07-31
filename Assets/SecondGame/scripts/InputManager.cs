using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.PlayerActions player;
    private PlayerMovemente movement;
    private Animator animator;
    public Vector2 moveDirection;
    public bool walkPressed;
    public bool runPressed;
    public bool jumpPressed;
    public bool crouchPressed;
    public GameObject root;
    private void Awake()
    {
        movement = GetComponent<PlayerMovemente>();
        playerInput = new PlayerInput();
        player = playerInput.Player;
        animator = GetComponent<Animator>();
        player.Movement.performed += ctx => this.moveDirection = ctx.ReadValue<Vector2>();
        player.Movement.performed += ctx => walkPressed = true;
        player.Movement.canceled += ctx => walkPressed = false;
        player.Movement.canceled += ctx => this.moveDirection = Vector2.zero;
        player.Sprint.performed += ctx => ctx.ReadValueAsButton();
    }

   
    private void Update()
    {

        if(moveDirection.x == 1)
        {
            transform.eulerAngles = new Vector3(0,90f,0);
            movement.orientation.transform.eulerAngles = Vector3.zero;
        }
        if (moveDirection.x == -1)
        {
            transform.eulerAngles = new Vector3(0, -90f, 0);
            movement.orientation.transform.eulerAngles = Vector3.zero;
        }
        if (moveDirection.y == -1)
        {
            transform.eulerAngles = new Vector3(0, -180f, 0);
            movement.orientation.transform.eulerAngles = Vector3.zero;
        }
        if (moveDirection.y == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movement.orientation.transform.eulerAngles = Vector3.zero;
        }
        if (walkPressed)
        {
            animator.SetBool("walking", true);
        }
        //if(player.Sprint.IsPressed())
        //{
        //    animator.SetBool("isRunning", true);
        //}
        if (!walkPressed)
        {
            animator.SetBool("walking", false);
        }
        //if(!player.Sprint.IsPressed())
        //{
        //    animator.SetBool("isRunning", false);

        //}
        //if (player.Jump.triggered || player.Jump.IsPressed())
        //{
        //    animator.SetBool("isGrounded", false);

        //    animator.SetBool("isJumping", true);

        //}
        //if (movement.grounded)
        //{
        //    animator.SetBool("isGrounded", true);
        //    animator.SetBool("isJumping", false);

        //}

    }

    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

    
  
}
