using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ladderPrefab;
    public GameObject playerPrefab;
    public GameObject indicatorPrefab;
    /*Contains the data for spawning object into the game*/
    public List<SpawnData> spawnDatas;

    /*Creates each  prefab instantiation data pair. 
     * If you want to spawn new type of game object, create a prefab and instantiation data class for it
     * then add that pair to the list*/
    private void CreateSpawnData()
    {
        spawnDatas = new List<SpawnData>()
        {
            /*For spawning the player*/
            { new SpawnData(playerPrefab, new PlayerInstantiationData())},
            /*For spawning the ladders*/
            { new SpawnData(ladderPrefab, new LadderInstantiationDataBase())},
            /*For spawning indicator */
            { new SpawnData(indicatorPrefab, new HelpIndicatorInstantiationData())}

        };
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateSpawnData();

        SpawnLevelObjects();
    }

    /*Handles spawning level one game objects*/
    public void SpawnLevelObjects()
    {
        /*For each prefab-InstantiationData pair we have*/
        foreach (SpawnData i in spawnDatas)
        {
            /*Spawn game objects using the prefab and the level one version of it instantiation data */
            Debug.Log(i.prefab.name);
            SpawnPrefabs(i.prefab, i.instantiationClass.CreateLevelOneInstantiationData());

        }
    }

    /*Spawns prefab in the level*/
    void SpawnPrefabs(GameObject prefab, List<InstantiationData> spawnDatas)
    {
        foreach (InstantiationData i in spawnDatas)
        {
             GameObject obj = Instantiate(prefab, i.position, Quaternion.Euler(i.rotationX, i.rotationY, i.rotationZ));
            //
            if (obj.TryGetComponent<MessageTrigger>(out MessageTrigger message))
                {
                message.Index = i.index;
            
            };
        }
    }
}
