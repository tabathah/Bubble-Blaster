using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public int[] order;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
