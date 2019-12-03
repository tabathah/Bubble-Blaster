using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // MOVES THE BOWL AROUND
        rb.velocity = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += transform.right * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += -transform.right * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += new Vector3(0, 0, 1) * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += new Vector3(0, 0, -1) * speed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity += new Vector3(0, 1, 0) * speed;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity += new Vector3(0, -1, 0) * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bubble")
        {

            Debug.Log("Bubble Collided with Bowl!");
            Destroy(collision.collider.gameObject);
        }

    }
}
