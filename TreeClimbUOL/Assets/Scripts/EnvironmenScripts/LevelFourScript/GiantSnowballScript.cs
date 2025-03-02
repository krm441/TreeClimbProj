using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSnowballScript : MonoBehaviour
{
    private readonly float moveSpeed = 4f;
    private static GameObject player;
    public GameObject evilSnowball;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        //Get the position the snowball was given
        startingPosition = transform.position;

        //If we don't have a player gameobject find one
        if (!player)
        {
            player = GameObject.Find("Player/Player");
        }

        //Get the initial rotation of GiantSnowball
        Quaternion oldRotation = transform.rotation;

        //Point towards the player
        transform.LookAt(player.transform);
        //Get the 
        oldRotation.y = transform.rotation.y;
        //
        //transform.rotation = oldRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (moveSpeed * Time.deltaTime * transform.forward);
        //Remove the snowball after it moves a certain distance
        if (Vector3.Distance(startingPosition, transform.position) > 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player hurt them
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.HitPlayer();

        }
        //Otherwise spawn snowballs from it
        else
        {
            //Spawn snowballs from it
            for (int i = 0; i < 6; i++)
            {
               Instantiate(evilSnowball, this.transform.position, Quaternion.Euler(60 * i, 0, 0));
            }
        }

        //Destroy the object after it hits a rigid body
        Destroy(gameObject);
    }


}
