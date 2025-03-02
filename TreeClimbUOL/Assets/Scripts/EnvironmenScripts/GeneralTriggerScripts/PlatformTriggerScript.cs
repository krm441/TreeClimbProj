using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player teleport them to a node on the tree
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            if (stateModel.transform.position.y - stateModel.rigidBody.velocity.y > transform.position.y && stateModel.PlayerAirState && stateModel.rigidBody.velocity.y < 0)
            {
                Vector3 vect = transform.position;
                vect.y += stateModel.PlayerRender.bounds.size.y/2;
                vect.z = stateModel.transform.position.z;
                stateModel.transform.position = vect;
                stateModel.OnTriggerPlatform(true);
                Debug.Log("On Platform");
            }
            Debug.Log(stateModel.rigidBody.velocity.y);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            //If the down key is pressed while the player is on the platform make them fall through it
            if (stateModel.GetDirectionalArray()[1] == -1 && stateModel.transform.position.y > transform.position.y && !stateModel.PlayerAirState)
            {
                stateModel.OnTriggerPlatform(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.OnTriggerPlatform(false);
        }
    }
}
