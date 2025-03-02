using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectScripts : MonoBehaviour
{
    private readonly int fallingSpeed = 7;
    // Update is called once per frame
    private void Update()
    {
        transform.position += fallingSpeed * Time.deltaTime * Vector3.down;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player hurt them
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.HitPlayer();
            Destroy(gameObject);
        }
        //Otherwise if it has enter the delete area remove it
        else if (other.gameObject.name == "DeleteArea")
        {
            Destroy(gameObject);
        }
    }
}
