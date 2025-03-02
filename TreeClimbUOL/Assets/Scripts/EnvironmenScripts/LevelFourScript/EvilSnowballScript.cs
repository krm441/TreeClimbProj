using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSnowballScript : MonoBehaviour
{
    readonly int moveSpeed = 5;
    //The intial position of the snowball
    Vector3 startingPosition;
    void Start()
    {
        //Get the position the snowball was given
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * transform.forward;
        //Remove the snowball after it moves a certain distance
        if (Vector3.Distance(startingPosition, transform.position) > 10)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player hurt them
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.HitPlayer();
        }
        Destroy(gameObject);

    }
}
