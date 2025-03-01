using UnityEngine;
using System.Collections.Generic;

public class Console : Object 
{

    public static void Log(params object[] v)
    {
        string o="";
        for ( int i = 0; i < v.Length; i++ )
        {
            o	+= ",";
            o	+= v.ToString();
        }

        Debug.Log(o);

    }
}

public class PathMovement : MonoBehaviour
{
    public List<Transform> pathNodes; // Drag and drop nodes in the Inspector
    private int nextNodeIndex = 1;
    private int previousNodeIndex = 0;
    public float moveSpeed = 5f;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.D) && nextNodeIndex < pathNodes.Count)
        {
            if(MoveTowardsNode(nextNodeIndex))
            {
                previousNodeIndex = nextNodeIndex; // exchange indicies
                nextNodeIndex++;
                if(nextNodeIndex == pathNodes.Count)
                {
                    nextNodeIndex = pathNodes.Count - 1;
                }
            }
        }
        else if (Input.GetKey(KeyCode.A) && previousNodeIndex >= 0)
        {
            if(MoveTowardsNode(previousNodeIndex))
            {
                nextNodeIndex = previousNodeIndex; // exchange indicies
                previousNodeIndex--;
                if(previousNodeIndex < 0)
                {
                    previousNodeIndex = 0;
                    nextNodeIndex = 1;
                }
                    
            }
        }

        Debug.Log(nextNodeIndex + " " + previousNodeIndex);
    }

    bool MoveTowardsNode(int targetIndex)
    {
        Transform targetNode = pathNodes[targetIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetNode.position, moveSpeed * Time.deltaTime);
        
        // Check if reached the target node
        if (Vector3.Distance(transform.position, targetNode.position) < 0.1f)
        {
            //currentNodeIndex = targetIndex;
            return true;
        }
        return false;
    }
}
