using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistletoeScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player hurt them
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.HitPlayer();
        }
    }
}
