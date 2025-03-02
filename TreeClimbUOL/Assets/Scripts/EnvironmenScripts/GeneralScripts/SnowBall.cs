using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    readonly int moveSpeed = 10;
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
        transform.Translate(Time.deltaTime * new Vector3(0, 0, moveSpeed));
        //Remove the snowball after it moves a certain distance
        if (Vector3.Distance(startingPosition, transform.position) > 15)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        Destroy(gameObject);

    }
}
