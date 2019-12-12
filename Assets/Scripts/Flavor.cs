using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flavor : MonoBehaviour
{
    // 0 = vanilla
    // 1 = chocolate
    // 2 = strawberry
    public int flavor;
    public static int maxFlavors = 3; // CHANGE THIS IF ADDING MORE FLAVORS
    public float amount;
	public bool frozen;
   


    // Start is called before the first frame update
    void Start()
    {
		frozen = false;
    }

    // Update is called once per frame
    void Update()
    {
		Color thisCol = Color.white;
		
        if (flavor == 0)
        {
            thisCol = Color.blue;

        }
        else if (flavor == 1)
        {
			thisCol = Color.red;

        }
        else if (flavor == 2) {

			thisCol = Color.green;

        }
		if (frozen)
		{
			thisCol = Color.Lerp(thisCol, Color.Lerp(Color.cyan, Color.white, 0.5f), 0.6f);
		}
		this.gameObject.GetComponent<Renderer>().materials[0].color = thisCol;
	}

    public void changeFlavor(int f)
    {
        flavor = f;
    }

    public void changeAmount(float amt)
    {
        amount = amt;
    }
}
