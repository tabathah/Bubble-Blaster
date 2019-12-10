using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5.0f;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bubble")
        {
            if(!collision.collider.gameObject.GetComponent<Bubble>().floating)
            {
                //Debug.Log("Bubble Collided with Bowl!");
                Destroy(collision.collider.gameObject);
            }
        }

    }
}
