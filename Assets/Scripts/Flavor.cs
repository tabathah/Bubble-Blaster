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
   


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flavor == 0)
        {
            this.gameObject.GetComponent<Renderer>().materials[0].color = Color.blue;

        }
        else if (flavor == 1)
        {
            this.gameObject.GetComponent<Renderer>().materials[0].color = Color.red;

        }
        else if (flavor == 2) {

            this.gameObject.GetComponent<Renderer>().materials[0].color = Color.green;

        }
    }

    public void changeFlavor(int f)
    {
        flavor = f;
    }
}
