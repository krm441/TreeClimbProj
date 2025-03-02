using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //If touching the player start the timer
        if (other.gameObject.name == "Player")
        {
            EventManger.BeginTimer();
        }
    }
}
