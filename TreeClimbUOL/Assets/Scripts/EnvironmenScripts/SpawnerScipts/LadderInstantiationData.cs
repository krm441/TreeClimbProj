using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInstantiationDataBase : InstantiationDataBase
{  
    /*The returns the */
    public override  List<InstantiationData> CreateLevelOneInstantiationData()
    {
      
        List<InstantiationData> ladderData = new List<InstantiationData>
        {
            new InstantiationData(new Vector3(3.878f, 2, 3.64f), 0,0,0 ),
                new InstantiationData(new Vector3(3.82f, 4.1025f, -4.64f),0,265,0)

        };
        return ladderData;
    }


    public override  List<InstantiationData> CreateLevelTwoInstantiationData()
    {
        throw new System.NotImplementedException();
    }     
    public override  List<InstantiationData> CreateLevelThreeInstantiationData()
    {
        throw new System.NotImplementedException();
    }

    public override  List<InstantiationData> CreateLevelFourInstantiationData()
    {
        throw new System.NotImplementedException();
    }

}
