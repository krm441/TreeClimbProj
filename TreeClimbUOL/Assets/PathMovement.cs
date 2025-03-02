using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Console : Object
{

    public static void Log(params object[] v)
    {
        string o = "";
        for (int i = 0; i < v.Length; i++)
        {
            o += ",";
            o += v.ToString();
        }

        Debug.Log(o);

    }
}

public class PathMovement : MonoBehaviour
{
    /*For the key movement*/
    public InputActionReference movement;

    public List<Transform> pathNodes; // Drag and drop nodes in the Inspector
    private int nextNodeIndex = 1;
    private int previousNodeIndex = 0;
    public float moveSpeed = 5f;
    /*Player can only move up and down when on a ladder*/
    private bool isClimbingLadder = false;
    /*Holds the states of the directional key presses */
    private readonly float[] directionalKeyStates = { 0, 0 };
    //
    public Rigidbody rig;

    private void Start()
    {

        rig = GetComponent<Rigidbody>();
    }
    private void DirectionalKeyChange()
    {
        Vector3 vect = Vector3.zero;
        if (movement)
        {
            vect = movement.action.ReadValue<Vector3>();
        }
        directionalKeyStates[0] = vect.x;
        directionalKeyStates[1] = vect.y;
    }

    void Update()
    {
        /*Get any directional key presses*/
        DirectionalKeyChange();

        //Move right
        if (directionalKeyStates[0] == 1 && nextNodeIndex < pathNodes.Count)
        {
            if (MoveTowardsNode(nextNodeIndex))
            {
                previousNodeIndex = nextNodeIndex; // exchange indicies
                nextNodeIndex++;
                if (nextNodeIndex == pathNodes.Count)
                {
                    nextNodeIndex = pathNodes.Count - 1;
                }
            }
        }
        //Move left
        else if (directionalKeyStates[0] == -1 && previousNodeIndex >= 0)
        {
            if (MoveTowardsNode(previousNodeIndex))
            {
                nextNodeIndex = previousNodeIndex; // exchange indicies
                previousNodeIndex--;
                if (previousNodeIndex < 0)
                {
                    previousNodeIndex = 0;
                    nextNodeIndex = 1;
                }

            }
        }

        //Debug.Log(nextNodeIndex + " " + previousNodeIndex);
    }

    bool MoveTowardsNode(int targetIndex)
    {
        Transform targetNode = pathNodes[targetIndex];
        Vector3 target = targetNode.position;


        //transform.position +=  new Vector3(target.x - transform.position.x / moveSpeed * Time.deltaTime, 0, 0);
        Vector3 nodePosition = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime) - transform.position;
        //Prevent vertical movement if not on a ladder

        if (!isClimbingLadder)
        {
            nodePosition.y = 0;
            target.y = transform.position.y;
        }

        transform.position += nodePosition;
        // Check if reached the target node
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            //currentNodeIndex = targetIndex;
            return true;
        }
        return false;
    }

    //Function the 
    public void ClimbingLadder(bool isOnLadder)
    {
        //If the player has just gotten on the ladder allow them to climb the ladder
        //Otherwise return back to horizontal movement
        isClimbingLadder = isOnLadder;
        rig.useGravity = !isOnLadder;
    }

}
