using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public GameObject custToSpawn;
    public float timer = 0.0f;
    public float spawnPeriod = 5.0f;
    float counterRadius = 4.6f;
    public GameObject counter;
    float circleOriginX;
    float circleOriginZ;
    bool firstGen = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 counterPos = counter.transform.position;
        circleOriginX = counterPos.x + 2.54f;
        circleOriginZ = counterPos.z - 2.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Updating...");
        //Debug.Log(timer.ToString());
        //Debug.Log(Time.deltaTime.ToString());
        timer += Time.deltaTime;
        if (!firstGen && timer > spawnPeriod)
        {
            //Debug.Log(timer.ToString());
            //Debug.Log("spawning cust");
            timer = 0.0f;
            float xPos = Random.Range(0.0f, counterRadius + circleOriginX);
            float yPos = -0.25f;
            float zPos = Mathf.Sqrt(Mathf.Pow(counterRadius, 2) - Mathf.Pow(xPos - circleOriginX, 2)) + circleOriginZ;

            // randomly invert x or z
            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                xPos *= -1;
            }

            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                zPos *= -1;
            }

            Vector3 at = new Vector3(-xPos, yPos, -zPos);
            Quaternion quat = new Quaternion();
            quat.SetFromToRotation(new Vector3(0, 0, 1), at);

            Instantiate(custToSpawn, new Vector3(xPos, yPos, zPos), quat);
            firstGen = true;
            // TODO: restrict respawn area of customers
        }
    }
}
