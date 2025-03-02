
using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    public Transform pivot, player;
    private void OnEnable()
    {
        EventManger.Enter2DMode += SwitchTo2D;
        EventManger.Enter3DMode += Enter3D;
    }

    private void OnDisable()
    {
        EventManger.Enter2DMode -= SwitchTo2D;
        EventManger.Enter3DMode-= Enter3D;
    }
    private Vector3 lastPlayerPosition;
    private float initialDistance;
    //The current mode of the camera
    string mode = "3D";

    void Start()
    {
        lastPlayerPosition = player.position; // Initialize last known position
        initialDistance = Vector3.Distance(transform.position, player.position); // Store initial distance
    }

    void Update()
    {
        //If the game mode is 3d use kamal camera code
        if (mode == "3D")
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

           
        }
        //Otherwise use the 2D camera code
        else {
            transform.position = new Vector3(player.position.x+10, player.position.y, player.position.z);
        } 
        // Always look at the player
            transform.LookAt(player);
    }

    /*Switches the camera to 2D mode*/
    void SwitchTo2D()
    {
        mode = "2D";
    }
    /*Switches the camera to 3D mode*/
    void Enter3D()
    {
        mode = "3D";
    }
}
