using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Camera camera;
    public float popTime = 20.0f;
    private bool floating = true;
    private Vector3 velocity = new Vector3(0, 0, 0);


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
        this.GetComponent<Rigidbody>().useGravity = false;
        while (velocity == new Vector3(0, 0, 0))
        {
            velocity = squareToDiskUniform(new Vector2(Random.Range(-10.0f, 10.0f),
                                                       Random.Range(-10.0f, 10.0f))).normalized;
        }
        Debug.Log(velocity);
        startBubblePop();
    }

    void startBubblePop() {
        StartCoroutine(bubblePop());

    }

    IEnumerator bubblePop() {

        yield return new WaitForSeconds(popTime);
        if (floating)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                // do whatever you wants
                if (hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("Touched Bubble!");
                    this.GetComponent<Rigidbody>().useGravity = true;
                    floating = false;
                }
            }
        }
        if (floating) {
            // bubble floating behavior
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = velocity * Random.Range(0.6f, 1.0f);
        }

    }
}
