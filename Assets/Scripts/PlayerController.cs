using UnityEngine;
using UnityEngine.AI;
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

    private Vector3 lastValidPosition; // Stores last valid position inside the NavMesh

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        lastValidPosition = transform.position; // Initialize with current position
    }

    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        verticalVelocity += gravity * Time.deltaTime;
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0;
        }

        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y + transform.up * verticalVelocity;
        Vector3 nextPosition = transform.position + move * moveSpeed * Time.deltaTime;

        // 🔥 Check if next position is inside the NavMesh
        if (NavMesh.SamplePosition(nextPosition, out _, 0.5f, NavMesh.AllAreas))
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
            lastValidPosition = transform.position; // Update valid position
        }
        else
        {
            transform.position = lastValidPosition; // Prevent movement outside NavMesh
        }
    }

    private void Rotate()
    {
        rotation.y += lookVector.x * lookSensitivity * Time.deltaTime;
        transform.localEulerAngles = rotation;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        animator.SetBool("Walk", moveVector.magnitude > 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded && context.performed)
        {
            animator.Play("Jump");
        }
    }

    public void Jump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -gravity);
    }
}
