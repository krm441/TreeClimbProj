using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperEnemyScripts : MonoBehaviour
{
    readonly float moveSpeed = 2.5f;
    readonly float dropDelay = 2f;
    bool canDropObject = true;
    public GameObject fallingDecoration;
    //The max
    private static Vector3 leftPoint = new Vector3(-3.222969f, -0.8788376f, -6.0f);
    private static Vector3 rightPoint = new Vector3(-3.222969f, -0.8788376f, 5.2f);
    private Vector3 target;

    private void Start()
    {
        target = leftPoint;
    }

    void Update()
    {
        //Once the enemy gets close enough to the target node swap the target node
        if (Vector3.Distance(transform.localPosition, target) < 0.1f)
        {
            target = target == leftPoint ? rightPoint : leftPoint;
        }
        //Move towards the target nodes
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, moveSpeed * Time.deltaTime);
        //Drop a decoration after an interval of time
        if (canDropObject)
        {
            StartCoroutine(SpawnDecoration());
        }
    }


    IEnumerator SpawnDecoration()
    {
        //Prevent any other objects from being spawning until the enumerator is finished 
        canDropObject = false;
        Vector3 decorationPosition = transform.position;
        decorationPosition.x += 0.5f;
        //Spawn the decoration
        Instantiate(fallingDecoration, decorationPosition, Quaternion.Euler(0, 0, 0)); ;
        yield return new WaitForSeconds(dropDelay);
        //Allow another object to be drops
        canDropObject = true;
    }

}