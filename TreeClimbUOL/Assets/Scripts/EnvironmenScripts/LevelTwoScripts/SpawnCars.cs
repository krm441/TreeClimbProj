using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles spawning the cars for level two
public class SpawnCars : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject carChaseSection;
    public GameObject rushHour;
    // Start is called before the first frame update
    List<Vector3> positions;
    void Start()
    {
        positions = new List<Vector3> { CreateVector3(-10.16f, 0.82615f, 2.8145f),
        CreateVector3(-10.161f,1.4507f, 7.971f),CreateVector3(93.91371f,-11.3f, 16.75f),
            CreateVector3(93.91371f,-16.275f, 21.127f)};
        for (int i = 0; i < positions.Count; i++)
        {
            GameObject obj;
            switch (i)
            {
                case 2: obj = Instantiate(carChaseSection, transform.position + positions[i], Quaternion.Euler(0, 180, 0)); break;
                case 3: obj = Instantiate(rushHour, transform.position + positions[i], Quaternion.Euler(0, 180, 0)); break;
                default: obj = Instantiate(carPrefab, transform.position + positions[i], Quaternion.Euler(0, 0, 0)); break;
            }
            obj.transform.parent = this.transform;
        }
    }
    //Short cut for creating a vector 3 object
    private Vector3 CreateVector3(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }
}
