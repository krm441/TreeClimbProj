using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Camera : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false); // Disable the camera at start
    }

    public void ActivateCamera()
    {
        gameObject.SetActive(true); // Activate when called
    }
}
