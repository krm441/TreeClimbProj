using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public GameObject startingTunnel;
    public GameObject endTunnel;
    /*Where the car starts from*/
    private Vector3 startingPoint;
    //Car spawning interval 
    public float spawnDelay = 1.0f;
    //How fast the car should move
    public float moveSpeed = 1f;
    /*Where the car ends up*/
    private Vector3 endPoint;
    //Whether the car is moving
    private bool carMoving = true;
    //Delay for the inital spawning of the car 
    private float initalDelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetDelayValueFromName();
        /*Define the start and end points*/
        startingPoint = startingTunnel.transform.position;
        startingPoint.y -= 0.25046f;
        endPoint = endTunnel.transform.position;
        endPoint.y -= 0.25046f;
        //Move the car to the start point
        transform.position = startingPoint;
        //Wait before spawning
        StartCoroutine(WaitToSpawn(initalDelay));

    }

    IEnumerator WaitToSpawn(float delay)
    {
        carMoving = false;
        yield return new WaitForSeconds(delay);
        carMoving = true;
    }
    void Update()
    {
        if (carMoving)
        {
            //Move the car
            transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint) < 0.1)
            {
                transform.position = startingPoint;
                StartCoroutine(WaitToSpawn(spawnDelay));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //If  the car has hit the player 
        if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.HitPlayer();
        }
    }

    /*Attempts to get a delay value from the object name*/
    void GetDelayValueFromName()
    {
        //Search for parentheses in the objects name
        string name = gameObject.name;
        int opening = name.IndexOf("(");
        int closing = name.IndexOf(")");
        //If we can find the opening and closing parentheses
        if (opening > -1 && closing > -1)
        {
            //Get the content of the parentheses
            string delayValue = name.Substring(opening + 1, closing - opening - 1);
            /*Attempt to convert the contents into a number */
            bool conversionSuccessful = float.TryParse(delayValue, out initalDelay);
            /*If the conversion fail set the intial delay back to zero*/
            if (!conversionSuccessful)
            {
                initalDelay = 0;
            }
            //Otherwise remove the spawn delay and slow the cars
            else {
                initalDelay *= 0.8f;
                spawnDelay = 0;
                moveSpeed = 4.0f;
            }
        }
    }
}
