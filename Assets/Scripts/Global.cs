using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public GameObject custToSpawn;
    public float timer;
    public float spawnPeriod;
    float counterRadius = 4.6f;
    public GameObject counter;
    float circleOriginX;
    float circleOriginZ;
    int numCustomers = 0;
    public int maxCustNum;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 counterPos = counter.transform.position;
        circleOriginX = counterPos.x + 2.54f;
        circleOriginZ = counterPos.z - 2.1f;

        spawnPeriod = 60.0f;
        timer = spawnPeriod - 5;
        maxCustNum = 15;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Updating...");
        //Debug.Log(timer.ToString());
        //Debug.Log(Time.deltaTime.ToString());
        timer += Time.deltaTime;
        if (numCustomers < maxCustNum && timer > spawnPeriod)
        {
            //Debug.Log(timer.ToString());
            //Debug.Log("spawning cust");
            timer = 0.0f;
            float angle = Random.Range(0.0f, 230);
            angle += 150.0f;

            float z = Mathf.Sin(Mathf.Deg2Rad * angle);
            float x = Mathf.Cos(Mathf.Deg2Rad * angle);

            Vector3 vector = new Vector3(x, 0.0f, z);
            vector = vector.normalized;

            vector *= counterRadius;

            float xPos = circleOriginX + vector.x;
            float yPos = -0.25f;
            float zPos = circleOriginZ + vector.z;

            // TODO: restrict respawn area of customers so they don't spawn too close to each other or spawn colliding with another customer (they like their personal space)

            Vector3 at = new Vector3(-xPos, yPos, -zPos);
            Quaternion quat = new Quaternion();
            quat.SetFromToRotation(new Vector3(0, 0, 1), at);

            Instantiate(custToSpawn, new Vector3(xPos, yPos, zPos), quat);
            numCustomers++;
        }
    }
}
