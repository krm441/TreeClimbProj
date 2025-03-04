using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject tpPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Entered TP Point: ");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TPPointEnt")) {
            tpPoint = other.transform.GetChild(0).gameObject;
            rb.transform.position = tpPoint.transform.position;
        }
    }
   



}
