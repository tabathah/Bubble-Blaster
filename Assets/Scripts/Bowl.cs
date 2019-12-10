using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5.0f;
    public float[] amount = new float[Flavor.maxFlavors];

    public GameObject icecreamScoop;
    public GameObject[] currScoops = new GameObject[Flavor.maxFlavors];


    void Start()
    {
        /*for(int i = 0; i < amount.Length; i++)
        {
            currScoops[i] = Instantiate(icecreamScoop, gameObject.transform.position, Quaternion.identity);
            currScoops[i].GetComponent<Flavor>().changeFlavor(i);
        }*/
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
            }
            currPos.y += amount[i] * 0.05f;
            currScoops[i].transform.localScale = new Vector3(amount[i] * 0.1f, amount[i] * 0.1f, amount[i] * 0.1f);
            currScoops[i].transform.position = gameObject.transform.rotation * currPos + gameObject.transform.position;
            currPos.y += amount[i] * 0.05f;
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
