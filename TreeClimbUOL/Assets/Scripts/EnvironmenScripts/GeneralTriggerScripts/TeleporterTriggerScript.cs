using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTriggerScript : MonoBehaviour
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
            //Entering the Teleport switches to 2D
            EventManger.SwitchTo2D();
            stateModel.TeleportToNode(level, exitIndex);

        }
    }
}
