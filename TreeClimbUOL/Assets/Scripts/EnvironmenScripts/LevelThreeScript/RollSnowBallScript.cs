using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSnowBallScript : MonoBehaviour
{
    float moveSpeed = 8;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * transform.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player hit them and destory itself
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.HitPlayer();
            Destroy(gameObject);
        }
        //Otherwise if it has hit a wall destroy it
        else if (other.gameObject.name == "Wall")
        {
            Destroy(gameObject);
        }
    }
}