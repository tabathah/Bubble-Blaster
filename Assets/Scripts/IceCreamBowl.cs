using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamBowl : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5.0f;
    public float[] amount = new float[Flavor.maxFlavors];

    public GameObject icecreamScoop;
    public GameObject[] currScoops = new GameObject[Flavor.maxFlavors];

    private float disableTime;
    private Vector3 disablePos;
    private Quaternion disableRot;
    private bool hitCustomer;



    void Start()
    {

    }

    void Update()
    {
        Vector3 currPos = new Vector3(0,0,0);
        for(int i = 0; i < amount.Length; i++)
        {
            if(!currScoops[i])
            {
                currScoops[i] = Instantiate(icecreamScoop, gameObject.transform.position, Quaternion.identity);
                currScoops[i].GetComponent<Flavor>().changeFlavor(i);
                currScoops[i].GetComponent<Flavor>().changeAmount(amount[i]);
            }
            currPos.y += amount[i] * 0.05f;
            currScoops[i].transform.localScale = new Vector3(amount[i] * 0.1f, amount[i] * 0.1f, amount[i] * 0.1f);
            currScoops[i].transform.position = gameObject.transform.rotation * currPos + gameObject.transform.position;
            currPos.y += amount[i] * 0.05f;
        }

        if (disableTime > 0)
        {
            disableTime -= Time.deltaTime;
            gameObject.transform.position = disablePos;
            gameObject.transform.rotation = disableRot;
        }

        if ((disableTime <= 0) && (hitCustomer))
        {
            // remove ice cream scoops and bowl from scene

            for (int i = 0; i < amount.Length; i++)
            {
                Destroy(currScoops[i]);
            }

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bubble")
        {
            if(!collision.collider.gameObject.GetComponent<Bubble>().floating)
            {
                Debug.Log("Bubble Collided with Bowl!");
                Destroy(collision.collider.gameObject);
            }
        }
        else if (collision.collider.gameObject.tag == "Customer")
        {
            Debug.Log("hit customer with bowl, customer maybe upset");
            disableTime = 0.5f;
            hitCustomer = true;
            disablePos = gameObject.transform.position;
            disableRot = gameObject.transform.rotation;
            Customer cust = collision.collider.gameObject.GetComponentInParent<Customer>();
            cust.EatIceCream(currScoops);
        }
    }

    public void addIceCream(float[] amt)
    {
        for(int i = 0; i < amount.Length; i++)
        {
            amount[i] += amt[i];
        }
    }

    // TO DO: VISUALLY SHOW AMOUNT OF ICECREAM
}
