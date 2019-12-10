using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMachine : MonoBehaviour
{
    public float[] amount = new float[Flavor.maxFlavors];
    public GameObject bowl;
    public GameObject bowlDispenser;
    public GameObject icecreamScoop;
    private GameObject lastBowl;
    
    // Start is called before the first frame update
    void Start()
    {
        //myBDispenser = Instantiate(bowlDispenser, this.transform.position + new Vector3(0, this.transform.lossyScale.y * 0.5f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clearIcecream() {
        print("emptying");
        for (int i = 0; i < amount.Length; i++) {
            amount[i] = 0;
        }
    }

    public void dispense() {
        print("dispensing");
        float len = 100;
        if (lastBowl)
        {
            len = Vector3.Distance(bowlDispenser.transform.position, lastBowl.transform.position);
        }
        if (len > 0.5f)
        {
            GameObject b = Instantiate(bowl, bowlDispenser.transform.position + new Vector3(0, bowlDispenser.transform.lossyScale.y * 0.5f, 0), Quaternion.identity);
            b.GetComponent<Bowl>().addIceCream(this.amount);
            b.GetComponent<Bowl>().icecreamScoop = this.icecreamScoop;
            lastBowl = b;
            clearIcecream();
        }
        

    }

    public void loadPitcher(float[] amt)
    {
        for(int i = 0; i < amount.Length; i++)
        {
            amount[i] += amt[i];
        }
    }

}
