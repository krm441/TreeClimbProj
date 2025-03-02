using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter2DTrigger: MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Switch to 2d mode if in contact with the player
        if (other.gameObject.name == "Player")
        {
            EventManger.SwitchTo2D();
        }
    }

}
