using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPileScript : MonoBehaviour
{
    //Whether the snowpile respawns
    bool canRespawn = true;
    public bool CanRespawn { set { canRespawn = value; } }
    private void OnEnable()
    {
        EventManger.playerDeath += RemoveSnowPlile; ;
        EventManger.gameWon += RemoveSnowPlile;
    }
    private void OnDisable()
    {
        EventManger.playerDeath -= RemoveSnowPlile; ;
        EventManger.gameWon -= RemoveSnowPlile;
    }

    //Respawn delay
    readonly float respawnDelay = 2.0f;
    private void OnTriggerEnter(Collider other)
    {
        /*If  touching the player given them some snowballs and then dissappear*/
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.GiveSnowBalls();
            //If the snowball can respawn perform action to spawn the next pile
            if (canRespawn)
            {
                //Hide each snowball in the pile as we can't delete the object untill a new one is formed.
                foreach (Transform child in transform)
                {
                    MeshRenderer meshRender = child.GetComponent<MeshRenderer>();
                    if (meshRender)
                    {
                        meshRender.enabled = false;
                    }
                }

                //Prepare to create the next pile
                StartCoroutine(SpawnNextPile());
            }
            else 
            {
                Destroy(gameObject);
            }

        }
    }

    //Has the next snow pile spawn after a delay
    IEnumerator SpawnNextPile()
    {

        yield return new WaitForSeconds(respawnDelay);
        //Create a call for the next snowpile
        EventManger.CreateNextSnowPile();
        //Remove the plie
        Destroy(gameObject);

    }

    //Removes the snowball pile
    private void RemoveSnowPlile()
    {
        Destroy(gameObject);
    }
}
