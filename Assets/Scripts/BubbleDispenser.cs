using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDispenser : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject bubble;
    //public Camera camera;
    public float speed = .15f;
    private bool on = false;
    private float onTime = 5;
    private float lastTime;
    public int flavor;

    public Color startColor;
    public Color onColor;

    private AudioSource bubbleSound;


    private void Start()
    {
        startSpawnBubbles();
        lastTime = Time.realtimeSinceStartup;
        //gameObject.GetComponent<Renderer>().materials[0].color = Color.red;

        startColor = gameObject.GetComponent<MeshRenderer>().materials[0].color;
        onColor = Color.Lerp(startColor, Color.white, 0.75f);

        bubbleSound = gameObject.GetComponent<AudioSource>();

    }
    private void Update()
    {
        float currTime = Time.realtimeSinceStartup;
        if(on)
        {
            onTime -= currTime - lastTime;
            if(onTime < 0)
            {
                if (bubbleSound.isPlaying)
                {
                    bubbleSound.Stop();
                }

                gameObject.GetComponent<MeshRenderer>().materials[0].color = startColor;
                on = false;
                onTime = 5;
            }
        }
        lastTime = currTime;
    }

    public void pressButton()
    {
        if(!on)
        {
            bubbleSound.Play();

            gameObject.GetComponent<MeshRenderer>().materials[0].color = onColor;
            on = true;
        }
    }

    void startSpawnBubbles() {

        StartCoroutine(spawnBubbles());

    }

    IEnumerator spawnBubbles() {

        if (on)
        {
            GameObject b = Instantiate(bubble, this.gameObject.transform.position, Quaternion.identity);
            b.GetComponent<Flavor>().changeFlavor(this.flavor);
            //b.transform.parent = gameObject.transform;
            //b.GetComponent<Bubble>().camera = camera;
        }

        yield return new WaitForSeconds(speed);
        startSpawnBubbles();
    }

}
