using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelThreeScript : MonoBehaviour
{
    public GameObject bossPrefab;
    public GameObject snowPileSpawner;
    private void OnTriggerEnter(Collider other)
    {
        //If the touching the player Start the fourth level
        if (other.gameObject.name == "Player")
        {
            GameObject spawner = Instantiate(snowPileSpawner, this.transform);
            spawner.transform.localPosition = new Vector3(58.21863f, -113.0706f, 963.7367f);

            GameObject boss = Instantiate(bossPrefab, this.transform);
            boss.transform.localPosition = new Vector3(-8.51483f, -40.82f, -111.4163f);
        }
    }
}
