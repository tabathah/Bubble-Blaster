using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    private int totalScoops;
    public int[] order;
    public Text order1;
    public Text order2;
    public Text order3;
    public Text thanks;

    public float totalWaitTime;                  // time since the customer spawned
    public Text waitTimeText;
    private float custPatience;             // patience of customer before their satisfaction begins to drop
    private float speedyServiceThresh;      // bonus is awarded for speedy service (i.e. fulfilling an order before a certain wait time)
    public float custSatisfaction;
    public Text satisfactionText;
    bool toDestroy = false;
    float destroyTimer = 0.0f;
    float timeToDestroy = 5.0f;

    private AudioSource chompSound;
    private bool served;

    // Start is called before the first frame update
    void Start()
    {
        int numFlavs = Flavor.maxFlavors;
        totalScoops = 0;
        order = new int[numFlavs];
        for(int i = 0; i < numFlavs; i++)
        {
            order[i] = (int)Mathf.Floor(Random.Range(0, 3));
            totalScoops += order[i];
;       }

        if (totalScoops == 0)
        {
            // make sure that there is at least 1 flavor with 1 scoop
            int randFlav = (int)Mathf.Floor(Random.Range(0, 3));
            order[randFlav] = 1;
            totalScoops = 1;
        }

        order1.text = order[0].ToString() + " Blue";
        order2.text = order[1].ToString() + " Red";
        order3.text = order[2].ToString() + " Green";

        chompSound = gameObject.GetComponent<AudioSource>();

        totalWaitTime = 0.0f;
        speedyServiceThresh = 10.0f;
        custPatience = 30.0f * totalScoops;
        custSatisfaction = 10.0f;

        satisfactionText.text = "Satisfaction: " + custSatisfaction.ToString() + "/10";
        waitTimeText.text = "Wait Time: " + totalWaitTime.ToString() + " s";

        served = false;

        //float r = Random.Range(0.0f, 1.0f);
        //float g = Random.Range(0.0f, 1.0f);
        //float b = Random.Range(0.0f, 1.0f);
        List<GameObject> childrenList = GetChildObjects(transform, "Snail");
        childrenList[0].GetComponent<MeshRenderer>().materials[0].color = new Color(order[1]/2.0f, order[2] / 2.0f, order[0] / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (custSatisfaction <= 0)
        {
            custSatisfaction = 0.0f;
        }

        if (!served)
        {
            totalWaitTime += Time.deltaTime;
            satisfactionText.text = "Satisfaction: --/10";
        }
        else
        {
            satisfactionText.text = "Satisfaction: " + System.Math.Round(custSatisfaction,1).ToString() + "/10";
        }

        
        waitTimeText.text = "Wait Time: " + System.Math.Round(totalWaitTime,0).ToString() + " s";

        if(toDestroy)
        {
            destroyTimer += Time.deltaTime;
            if(destroyTimer > timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

    public void EatIceCream(GameObject[] scoops)
    {
        served = true;
        chompSound.Play();

        Debug.Log("eating ice cream");

        float totalIceCream = 0;
        // calculate customer satisfaction based on accuracy of order
        for (int i = 0; i < scoops.Length; i++)
        {
            float scoopAmt = scoops[i].GetComponent<Flavor>().amount;
            totalIceCream += scoopAmt;
            custSatisfaction -= Mathf.Abs(order[i] - scoopAmt) * 5;
        }

        // calculate customer satisfaction based on total wait time
        if (totalWaitTime < speedyServiceThresh)
        {
            custSatisfaction += 0.5f; // bonus is awarded for speedy service
        }
        else
        {
            custSatisfaction -= Mathf.Max((totalWaitTime - custPatience), 0) / 100;
        }

        if (totalIceCream == 0)
        {
            // no ice cream given :(
            custSatisfaction = 0;
        }

        if (custSatisfaction > 8)
        {
            thanks.text = "Thanks!";
        }
        else if (custSatisfaction > 5)
        {
            thanks.text = "Thanks?";
        }
        else
        {
            thanks.fontSize = 70;
            thanks.text = "Can I speak to the manager?";
        }

        order1.text = "";
        order2.text = "";
        order3.text = "";

        toDestroy = true;
    }

    private List<GameObject> GetChildObjects(Transform parent, string _tag)
    {
        List<GameObject> childrenList = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                childrenList.Add(child.gameObject);
            }
        }

        return childrenList;
    }
}
