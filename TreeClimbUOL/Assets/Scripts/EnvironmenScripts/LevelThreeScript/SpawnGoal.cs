using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGoal : MonoBehaviour
{
    public GameObject teleporterPrefab;

    private void OnEnable()
    {
        EventManger.endLevelThree += SpawnExit;
    }

    private void OnDisable()
    {
        {
            EventManger.endLevelThree -= SpawnExit;
        }
    }
    //Spawns the exit for the level.
    private void SpawnExit()
    {
        GameObject obj = Instantiate(teleporterPrefab, this.transform);
        obj.transform.localPosition = new Vector3(0.081f, -0.3f, 0f);
        if(obj.TryGetComponent<TeleporterTriggerScript>(out TeleporterTriggerScript teleporter));
        {
            teleporter.level = 4;
            teleporter.exitIndex = 0;
        }
    }
}
