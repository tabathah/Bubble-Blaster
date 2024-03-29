﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Bowl")
        {
            obj.transform.position = new Vector3(1.5f, 1, 0.5f);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
                rb.transform.eulerAngles = new Vector3(0, 0, 0);
            }

        }
        else if (obj.tag == "Pitcher")
        {
            Debug.Log("pitcher out of bounds");
            Pitcher pitcher = other.GetComponentInParent<Pitcher>();
            pitcher.transform.position = pitcher.startPos;
            pitcher.transform.rotation = pitcher.startRot;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
                rb.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
