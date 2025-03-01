using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickControlLevel1 : MonoBehaviour
{
    public static  string objName;
    public GameObject objText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        objName = gameObject.name;
        Debug.Log(objName);
        Destroy(gameObject);
        Destroy(objText);
    }
}
