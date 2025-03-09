using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float lookSensitivity = 15;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private float gravity = -9.81f;

    private Vector2 moveVector;
    private Vector2 lookVector;
    private Vector3 rotation;
    private float verticalVelocity;

    private CharacterController controller;
    private Animator animator;
    private NavMeshAgent agent;
    private bool useNavMesh = false;

    private Vector3 lastValidPosition;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            gameObject.SetActive(false);
        }

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = moveSpeed; 
            agent.updateRotation = false; 
            agent.updatePosition = false; 
            useNavMesh = true;
        }

        lastValidPosition = transform.position;
    }

    void Update()
    {
        if (useNavMesh && agent.enabled)
        {
            HandleNavMeshMovement();
        }
        else
        {
            Move();
        }

        Rotate();
    }

    private void HandleNavMeshMovement()
    {
        if (moveVector.magnitude > 0)
        {
            Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;
            Vector3 targetPosition = transform.position + move * moveSpeed * Time.deltaTime;

            if (NavMesh.SamplePosition(targetPosition, out _, 0.5f, NavMesh.AllAreas))
            {
                agent.SetDestination(targetPosition);
                lastValidPosition = transform.position; 
            }
            else
            {
                transform.position = lastValidPosition; 
            }
        }
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

      
        if (NavMesh.SamplePosition(nextPosition, out _, 0.5f, NavMesh.AllAreas))
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
            lastValidPosition = transform.position; 
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
            Jump();
        }
    }

    public void Jump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -gravity);
    }
}
