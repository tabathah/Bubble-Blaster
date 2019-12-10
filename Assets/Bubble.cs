using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //public Camera camera;
    public float popTime = 20.0f;
    public bool floating = true;
    private Vector3 velocity = new Vector3(0, 0, 0);

    // these are flavor components
    //public 


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
            velocity.y = Mathf.Abs(velocity.y);
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

    public void shootBubble()
    {
        Debug.Log("Touched Bubble!");
        this.GetComponent<Rigidbody>().useGravity = true;
        floating = false;
        gameObject.GetComponent<Renderer>().materials[0].color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (floating) {
            // bubble floating behavior
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = velocity * Random.Range(0.6f, 1.0f);
        }

    }

}
