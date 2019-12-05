using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Camera camera;
    public float popTime = 5.0f;
    public bool floating = true;
    private Vector3 velocity = new Vector3(0, 0, 0);
    private float lastTime;


    Vector3 squareToDiskUniform(Vector2 v)
    {
        // maps sample x to radius, and sample y to angle
        float x = Mathf.Pow(v[0], 0.5f) * Mathf.Cos(Mathf.Deg2Rad * (v[1] * 360.0f));
        float y = Mathf.Pow(v[0], 0.5f) * Mathf.Sin(Mathf.Deg2Rad * (v[1] * 360.0f));
        return new Vector3(-1.5f, x, y);

    }
    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.realtimeSinceStartup;
        this.GetComponent<Rigidbody>().useGravity = false;
        while (velocity == new Vector3(0, 0, 0))
        {
            velocity = squareToDiskUniform(new Vector2(Random.Range(-1.0f, 1.0f),
                                                       Random.Range(-0.5f, 0.5f))).normalized;
            velocity.y = Mathf.Abs(velocity.y);
            GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    public void shootBubble()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
        floating = false;
        gameObject.GetComponent<Renderer>().materials[0].color = Color.cyan;
        popTime = 4;
    }

    // Update is called once per frame
    void Update()
    {
        float currTime = Time.realtimeSinceStartup;
        popTime -= currTime - lastTime;
        print(popTime.ToString());
        if (popTime <= 0)
        {
            Destroy(gameObject);
        }
        if (floating) {
            // bubble floating behavior
            Rigidbody rb = GetComponent<Rigidbody>();
            if(Vector3.Magnitude(rb.velocity) >= 0.25f)
            {
                rb.velocity = rb.velocity * 0.99f;
            }
        }
        lastTime = currTime;
    }

}
