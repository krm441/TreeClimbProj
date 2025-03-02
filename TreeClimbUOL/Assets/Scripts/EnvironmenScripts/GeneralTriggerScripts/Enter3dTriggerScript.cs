using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter3dTriggerScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Switch to 3d mode if in contact with the player
        if (other.gameObject.name == "Player")
        {
            EventManger.SwitchTo3D();
        }
    }

}
