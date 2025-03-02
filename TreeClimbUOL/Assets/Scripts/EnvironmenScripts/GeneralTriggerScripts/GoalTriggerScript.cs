using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTriggerScript : MonoBehaviour
{
    //The level to go to
    public int level = 0;
    //The index of the node to warp to;
    public int exitIndex = 0;
    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player teleport them to a node on the tree
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            //Entering the goal switches to 3D
            EventManger.SwitchTo3D();
            stateModel.TeleportToNode(level, exitIndex);
            
        }
    }
}
