using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiationData : InstantiationDataBase
{
 

    public override List<InstantiationData> CreateLevelOneInstantiationData()
    {
        return new List<InstantiationData>() { new InstantiationData(new Vector3(0f, 0.5f, 0f)) };
    }

  
    public override List<InstantiationData> CreateLevelTwoInstantiationData()
    {
        throw new System.NotImplementedException();
    } 
    public override List<InstantiationData> CreateLevelThreeInstantiationData()
    {
        throw new System.NotImplementedException();
    }   
    public override List<InstantiationData> CreateLevelFourInstantiationData()
    {
        throw new System.NotImplementedException();
    }

}
