using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnData
{
    public SpawnData(GameObject _prefab, InstantiationDataBase _instantiationClass)
    {
        prefab = _prefab;
        instantiationClass = _instantiationClass;
    }

    public GameObject prefab;
    public InstantiationDataBase instantiationClass;
}

/*Contains the data required to spawn a gameObject*/
public struct InstantiationData
{
    public InstantiationData(Vector3 v, float rX = 0, float rY = 0, float rZ = 0, int i = 0) 
    {
        position = v;
        rotationX = rX;
        rotationY = rY;
        rotationZ = rZ;
        index = i;
    }
    public Vector3 position;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public int index;
}
/*Base class for spawning object in the game*/
public abstract class InstantiationDataBase
{
    /*Spawn the objects for level one*/
   public abstract List<InstantiationData> CreateLevelOneInstantiationData();
    /*Spawn the objects for level two*/
    public abstract  List<InstantiationData> CreateLevelTwoInstantiationData();
    /*Spawn the objects for level three*/
    public abstract  List<InstantiationData> CreateLevelThreeInstantiationData();
    /*Spawn the objects for level four*/
    public abstract  List<InstantiationData> CreateLevelFourInstantiationData();
}
