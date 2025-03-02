using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    //The maximun enumer of enemies that should spawn.
    readonly int spawnLimit = 3;
    //Whether the spawn is active or not
    bool isSpawnActive = false;
    //The number of enemies spawned;
    int enemiesSpawned = 0;
    public PlayerStateModel playerModel;
    //The decoration that the enemy drop
    public GameObject dropperEnemyPrefab;
    //The snowBall that rolls across the round
    public GameObject rollingSnowBallPrefab;
    // 
    private Coroutine spawnEnemiesRoutine;
    private Coroutine rollingSnowBallsRoutine;
    private void OnEnable()
    {
        EventManger.startTimer += ActiavateSpawner;
        EventManger.stopTimer += DespawnEnemies;
    }

    void OnDisable()
    {
        EventManger.startTimer -= ActiavateSpawner;
        EventManger.stopTimer -= DespawnEnemies;
    }
    //Spawns the enemy that drops things on the player
    void SpawnDropperEnemy(Vector3 position)
    {

        //
        GameObject obj = Instantiate(dropperEnemyPrefab, this.transform);
        obj.transform.localPosition = position;


    }
    //Despawns all the enemies in the object
    void DespawnEnemies()
    {
        //Turn off the spawner
        isSpawnActive = false;
        //Cancel any ongoing routines
        if (rollingSnowBallsRoutine != null)
        {
            StopCoroutine(rollingSnowBallsRoutine);
        }

        if (spawnEnemiesRoutine != null)
        {
            StopCoroutine(spawnEnemiesRoutine);
        }

        foreach (Transform enemy in transform)
        {
            GameObject.Destroy(enemy.gameObject);
        }
        enemiesSpawned = 0;
    }


    //Starts the spawner
    void ActiavateSpawner()
    {
        //Only spawn enemies on level three
        if (playerModel.CurrentLevel == 3)
        {
            isSpawnActive = true;
            SpawnEnemies();
        }
        rollingSnowBallsRoutine = StartCoroutine(SpawnRollingSnowball(10));
    }

    //Handles spawning enemies
    void SpawnEnemies()
    {
        //Spawn the the first enemy
        SpawnDropperEnemy(new Vector3(-3.222969f, -0.8788376f, -0.8788377f));
        enemiesSpawned += 1;
        //recursively spawn the other enemies;
        if (enemiesSpawned <= spawnLimit)
        {
            spawnEnemiesRoutine = StartCoroutine(SpawnEnemy(5));
        }
    }

    //Spawns a rolling snowball after a delay
    IEnumerator SpawnRollingSnowball(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isSpawnActive)
        {
            if (isSpawnActive)
            {
                GameObject obj = Instantiate(rollingSnowBallPrefab, this.transform);
                obj.transform.localPosition = new Vector3(-2.6f, -5.9025f, -5.34f);
                //Spawns another snowballs
                StartCoroutine(SpawnRollingSnowball(delay));
            }
        }

    }

    //Spawns an enemy after a delay
    IEnumerator SpawnEnemy(float delay)
    {

        if (isSpawnActive)
        {
            yield return new WaitForSeconds(delay);
            if (isSpawnActive)
            {
                SpawnDropperEnemy(new Vector3(-3.222969f, -0.8788376f, -0.8788377f));
                enemiesSpawned += 1;
                //Spawns the next enemy if there is space
                if (enemiesSpawned <= spawnLimit)
                {
                    StartCoroutine(SpawnEnemy(delay));
                }
            }
        }
    }
}
