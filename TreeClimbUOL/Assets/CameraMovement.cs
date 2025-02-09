//using UnityEngine;

//public class CameraMovement : MonoBehaviour
//{
//    public float speed = 5f; // Speed of movement
//
//    public Transform pivot, player;
//
//    public float rotationSpeed = 2f;
//
//    void Update()
//    {
//        Vector3 move = Vector3.zero;
//        
//        // Rotate around the tree when the player moves left or right
//        if (Input.GetKey(KeyCode.D))
//        {
//            transform.RotateAround(pivot.position, Vector3.up, -rotationSpeed * Time.deltaTime);
//        }
//        else if (Input.GetKey(KeyCode.A))
//        {
//            transform.RotateAround(pivot.position, Vector3.up, rotationSpeed * Time.deltaTime);
//        }
//        
//        // Always look at the player
//        transform.LookAt(player);
//    }
//}

//using UnityEngine;

//public class CameraMovement : MonoBehaviour
//{
//    public Transform pivot, player;
//
//    void Update()
//    {
//        // Calculate the direction vectors
//        Vector3 pivotToPlayer = player.position - pivot.position;
//        Vector3 pivotToCamera = transform.position - pivot.position;
//        
//        // Compute the angle between the pivot-to-camera and pivot-to-player vectors
//        float angle = Vector3.SignedAngle(pivotToCamera, pivotToPlayer, Vector3.up);
//        
//        // Rotate the camera around the pivot by the exact angle needed
//        transform.RotateAround(pivot.position, Vector3.up, angle);
//        
//        // Always look at the player
//        transform.LookAt(player);
//    }
//}

//using UnityEngine;

//public class CameraMovement : MonoBehaviour
//{
//    public Transform pivot, player;//, helper; // helper
//    private Vector3 lastPlayerPosition;
//    private Vector
//
//    void Start()
//    {
//        lastPlayerPosition = player.position; // Initialize last known position
//    }
//
//    void Update()
//    {
//        // Check if the player is moving
//        if (Vector3.Distance(player.position, lastPlayerPosition) > 0.01f)
//        {
//
//            float angle = Vector3.SignedAngle(player.position - pivot.position, lastPlayerPosition - pivot.position , Vector3.up);
//
//            pivot.transform.RotateAround(pivot.position, Vector3.up, -angle);
//            
//            // Update last known player position
//            lastPlayerPosition = player.position;
//        }
//
//        transform.position = new Vector3(transform.position.x, player.position.y + 1, transform.position.z);
//
//        // Always look at the player
//        transform.LookAt(player);
//    }
//}
//
using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    public Transform pivot, player;
    private Vector3 lastPlayerPosition;
    private float initialDistance;

    void Start()
    {
        lastPlayerPosition = player.position; // Initialize last known position
        initialDistance = Vector3.Distance(transform.position, player.position); // Store initial distance
    }

    void Update()
    {
        // Check if the player is moving
        if (Vector3.Distance(player.position, lastPlayerPosition) > 0.01f)
        {
            float angle = Vector3.SignedAngle(player.position - pivot.position, lastPlayerPosition - pivot.position, Vector3.up);
            pivot.transform.RotateAround(pivot.position, Vector3.up, -angle);

            // Update last known player position
            lastPlayerPosition = player.position;
        }

        // Maintain constant distance
        Vector3 direction = (transform.position - player.position).normalized;
        transform.position = player.position + direction * initialDistance;

        // Maintain height offset
        transform.position = new Vector3(transform.position.x, player.position.y + 1, transform.position.z);

        // Always look at the player
        transform.LookAt(player);
    }
}
