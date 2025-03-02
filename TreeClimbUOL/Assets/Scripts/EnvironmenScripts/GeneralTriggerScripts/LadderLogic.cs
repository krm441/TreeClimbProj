using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderLogic : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.5f, 0.25f, 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.LaddersInContactWith += 1;
            stateModel.LadderInteraction(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.LaddersInContactWith -= 1;
            //If the player is not touching any ladders
            if (stateModel.LaddersInContactWith <=0)
            {
                stateModel.LaddersInContactWith = 0;
                stateModel.LadderInteraction(false);
            }
        }
    }
}
