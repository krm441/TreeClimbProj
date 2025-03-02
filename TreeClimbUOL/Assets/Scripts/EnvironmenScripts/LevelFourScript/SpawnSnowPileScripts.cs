using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnowPileScripts : MonoBehaviour
{
    private void OnEnable()
    {
        EventManger.spawnNextSnowpile += SpawnSnowPile;
    }
    private void OnDisable()
    {
        EventManger.spawnNextSnowpile -= SpawnSnowPile;
    }
    int position = 0;
    public GameObject snowpile;
    // Start is called before the first frame update
    void Start()
    {
        //Create a snow pile at the top right corner
        GameObject obj = Instantiate(snowpile, this.transform);
        obj.transform.localPosition = new Vector3(-20.97204f, 17.669f, -331.17f);
        obj.GetComponent<SnowPileScript>().CanRespawn = false;
        SpawnSnowPile();
    }

    //Spawns the snowpile
    void SpawnSnowPile()
    {
        GameObject obj = Instantiate(snowpile, this.transform);
        //Spawn a snowpile at a postion
        if (position == 0)
        {
            obj.transform.localPosition = new Vector3(-20.97204f, 14.57211f, -324.4679f);
        }
        else 
        {
            obj.transform.localPosition = new Vector3(-20.97204f, 14.57211f, -331.324f);
        }
        //Have the next spawning postion swapped.
        position = 1 -  position;
    }
}
