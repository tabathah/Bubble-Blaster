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

    private void Start()
    {
        startSpawnBubbles();
    }
    private void Update()
    {
        // turn dispenser on or off
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                on = !on;
                Debug.Log("turned on?: " + on);
            }
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
