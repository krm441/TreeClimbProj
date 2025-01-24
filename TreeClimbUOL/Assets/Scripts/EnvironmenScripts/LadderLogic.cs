using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderLogic : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f,0.5f,0.25f,1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerTemplate(Clone)" &&other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            Debug.Log("Player near");
            stateModel.NearLadderState = true;
            stateModel.LadderBounds = gameObject.GetComponent<Renderer>().bounds;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerTemplate(Clone)" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel state))
        {
            Debug.Log("Player gone");
            state.NearLadderState = false;
        }
    }
}
