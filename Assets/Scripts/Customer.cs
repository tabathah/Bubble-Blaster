using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public int[] order;

    public float custSatisfaction;

    private AudioSource chompSound;

    // Start is called before the first frame update
    void Start()
    {
        int numFlavs = Flavor.maxFlavors;
        order = new int[numFlavs];
        for(int i = 0; i < numFlavs; i++)
        {
            order[i] = (int)Mathf.Floor(Random.Range(0, 2));
        }
        print(numFlavs);
        print(order[0]);
        print(order[1]);
        print(order[2]);

        chompSound = gameObject.GetComponent<AudioSource>();
        custSatisfaction = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatIceCream(GameObject[] scoops)
    {
        chompSound.Play();

        Debug.Log("eating ice cream");
        for (int i = 0; i < scoops.Length; i++)
        {
            custSatisfaction -= Mathf.Abs(order[i] - scoops[i].GetComponent<Flavor>().amount);
        }
    }
}
