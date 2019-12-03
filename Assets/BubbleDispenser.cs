using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDispenser : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject bubble;
    public Camera camera;
    public float speed = 2.0f;
    private bool on = false;
    private float onTime = 5;
    private float lastTime;

    private void Start()
    {
        startSpawnBubbles();
        lastTime = Time.realtimeSinceStartup;
        gameObject.GetComponent<Renderer>().materials[0].color = Color.red;
    }
    private void Update()
    {
        float currTime = Time.realtimeSinceStartup;
        if(on)
        {
            onTime -= currTime - lastTime;
            if(onTime < 0)
            {
                gameObject.GetComponent<Renderer>().materials[0].color = Color.red;
                on = false;
                onTime = 5;
                print("off");
            }
        }
        lastTime = currTime;
    }

    public void pressButton()
    {
        if(!on)
        {
            gameObject.GetComponent<Renderer>().materials[0].color = Color.green;
            print("on");
            on = true;
        }
    }

    void startSpawnBubbles() {

        StartCoroutine(spawnBubbles());

    }

    IEnumerator spawnBubbles() {

        if (on)
        {
            Debug.Log("Spawned a Bubble");
            GameObject b = Instantiate(bubble, this.gameObject.transform.position, Quaternion.identity);
            //b.transform.parent = gameObject.transform;
            b.GetComponent<Bubble>().camera = camera;
        }

        yield return new WaitForSeconds(speed);
        startSpawnBubbles();
    }

}
