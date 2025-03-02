using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCameraScripts : MonoBehaviour
{
    private Transform playerTransform;
    /*The offset of the camera*/
    private Vector3 cameraOffset = new Vector3(0.32f,9.14f,-15.08f);
    // Start is called before the first frame update
    private Vector3 currentVelocity = Vector3.zero;

    private readonly float smoothTime = 01f;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        GameObject obj = GameObject.Find("PlayerTemplate(Clone)");
        if (obj)
        {
            playerTransform = obj.transform;
        }
        if (playerTransform)
        {
            Vector3 targetPosition = playerTransform.position + cameraOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        }
    }
}
