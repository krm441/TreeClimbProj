using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpIndicatorInstantiationData : InstantiationDataBase
{


    public override List<InstantiationData> CreateLevelOneInstantiationData()
    {
        List<InstantiationData> IndicatorData = new List<InstantiationData>
        {
            new InstantiationData(new Vector3(0f, -0.502f, 0f),0,0,0 ),
            new InstantiationData(new Vector3(4.49f, -0.502f, -6.18f),0,0,0,1),
            new InstantiationData(new Vector3(2.878f, -0.502f, 3.64f),0,0,0,2),
            new InstantiationData(new Vector3(3.25f, 2.435f, 3.64f),0,0,90,3)
        };
        return IndicatorData;
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
