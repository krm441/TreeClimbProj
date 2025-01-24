using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPileScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        /*If  touching the player given them some snowballs and then dissappear*/
        if (other.gameObject.name == "PlayerTemplate" && other.gameObject.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        { 
            stateModel.GiveSnowBalls(); 
            Destroy(gameObject);
        }
    }
}
