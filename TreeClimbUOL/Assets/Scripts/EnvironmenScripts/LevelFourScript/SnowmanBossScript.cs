using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanBossScript : MonoBehaviour
{
    //Move Points
    private readonly Vector3[] points = { new Vector3(-2.8f, 2.2f, -32.0f), new Vector3(-2.8f, 2.2f, -44.6f) };
    private int indexPoint = 1;
    private Vector3 target;
    public GameObject giantSnowballPrefab;
    public GameObject evilSnowball;

    //The number of snowballs that should be fired when using rapid fire
    readonly int rapidFireLimit = 10;
    //The hitpoints of the snowman
    int hp = 20;
    //
    bool isMoving = false;
    readonly float moveSpeed = 3f;

    private void OnEnable()
    {
        EventManger.playerDeath += DestroyBoss;
        EventManger.gameWon += DestroyBoss;
    }
    private void OnDisable()
    {
        EventManger.playerDeath -= DestroyBoss;
        EventManger.gameWon -= DestroyBoss;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = points[indexPoint];
        //Wait two second after spawning
        StartCoroutine(Idling(2f)); 
    }
    private void Update()
    {
        //If the snow is moving
        if (isMoving)
        {
            //Move the snowman to the specified point
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
            transform.LookAt(target);
            //Once the snowman has reached it destination perform the next action and set the next point
            if (Vector3.Distance(transform.position, target) < 1)
            {
                indexPoint = 1 - indexPoint;
                //Wait a bit before performing the next action 
                StartCoroutine(Idling(0.4f));
                isMoving = false;
                target = points[indexPoint];
                transform.LookAt(target);
            }
        }
    }



    //Damages the snowman

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //If hit by a snowball damage the boss and remove the snowball
        if (other.gameObject.name.Contains("Snowball"))
        {
            DamageBoss();
            Destroy(other.gameObject);
        }
        //Harm the player if touching them
        else if (other.gameObject.name == "Player" && other.TryGetComponent<PlayerStateModel>(out PlayerStateModel stateModel))
        {
            stateModel.HitPlayer();
        }
    }


    private void DamageBoss()
    {
        hp -= 1;
        //If the snowman has run out of health despawn it
        if (hp <= 0)
        {
            EventManger.gameWon();
        }
    }

    //Handles selecting boss attacks
    IEnumerator SelectAttack()
    {
        //Select an attack to perform

        int attackIndex = Random.Range(0, 3);

        //Exploding snowball attack
        if (attackIndex == 0)
        {
            GameObject giantSnowball = Instantiate(giantSnowballPrefab);
            giantSnowball.transform.position = this.transform.position;
            yield return new WaitForSeconds(2.2f);
            //Select another attack
            StartCoroutine(SelectAttack());
        }
        //Moves to the otherside of area
        else if (attackIndex == 1)
        {
            isMoving = true;
        }
        //Rapid fire snowballs
        else if (attackIndex == 2)
        {
            StartCoroutine(RapidFire(rapidFireLimit));
        }

    }

    //A make the snow wait for a while
    IEnumerator Idling(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SelectAttack());

    }

    //For rapid firing off snowballs
    IEnumerator RapidFire(int snowballsLeft)
    {
        yield return new WaitForSeconds(0.2f);

        //If we still have snowballs to left to fire spawn the next one
        if (snowballsLeft > 1)
        {
            GameObject snowball = Instantiate(evilSnowball, this.transform);
            snowball.transform.localPosition = Vector3.zero;
            snowball.transform.LookAt(GameObject.Find("Player/Player").transform);
            StartCoroutine(RapidFire(snowballsLeft - 1));
        }
        else
        {
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(SelectAttack());
        }
        //Otherwise perform another attack

    }

    //Destroys the boss
    private void DestroyBoss()
    {
        Destroy(gameObject);
    }
}
