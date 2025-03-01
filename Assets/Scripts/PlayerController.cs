using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private float lookSensitivity = 15;

    [SerializeField]
    private float jumpHeight = 1;

    [SerializeField]
    private float gravity = -9.81f;

    private Vector2 moveVector;
    private Vector2 lookVector;
    private Vector3 rotation;

    private float verticalVelocity;

    private CharacterController controller;

    private Animator animator;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        verticalVelocity += gravity*Time.deltaTime;
        if(controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0;
        }
        Vector3 move = transform.right*moveVector.x + transform.forward*moveVector.y + transform.up*(verticalVelocity);
        controller.Move(move*moveSpeed*Time.deltaTime);
    }

    private void Rotate()
    {
        rotation.y += lookVector.x*lookSensitivity*Time.deltaTime;
        transform.localEulerAngles = rotation;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        if(moveVector.magnitude > 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(controller.isGrounded && context.performed)
        {
            animator.Play("Jump");
        }
    }

    public void Jump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight*-gravity);
    }

}