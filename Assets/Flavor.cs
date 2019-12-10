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
        // default is vanilla
        flavor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (flavor == 0)
        {
            this.gameObject.GetComponent<Renderer>().materials[0].color = Color.white;

        }
        else if (flavor == 1)
        {
            this.gameObject.GetComponent<Renderer>().materials[0].color = new Color(153, 102, 51);

        }
        else if (flavor == 2) {

            this.gameObject.GetComponent<Renderer>().materials[0].color = new Color(255, 153, 204);

        }
    }
}
