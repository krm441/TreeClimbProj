using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSnowBallScript : MonoBehaviour
{
    readonly float moveSpeed = 8;
    Vector3 startingPosition;
    private void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Remove the snowball after it travels a distance
        if (Vector3.Distance(startingPosition, transform.position) > 10.2)
        {
            Destroy(gameObject);
        }
        transform.position += moveSpeed * Time.deltaTime * transform.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player hit them
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.HitPlayer();
        }
        //Destory it after the collision
        Destroy(gameObject);

    }
}