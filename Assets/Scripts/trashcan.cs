using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private AudioSource trashSound;

    public void Start()
    {
        trashSound = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!trashSound.isPlaying)
        {
            trashSound.Play();
        }

        if (collision.collider.gameObject.tag == "Pitcher")
        {
            // remove the contents of the pitcher
            Pitcher pitcher = collision.collider.gameObject.GetComponent<Pitcher>();
            pitcher.clearPitcher();
        }
        else if (collision.collider.gameObject.tag == "IceCreamBowl")
        {
            // delete the bowl and ice cream
            IceCreamBowl bowl = collision.collider.gameObject.GetComponent<IceCreamBowl>();
            bowl.destroyBowlAndIceCream();
        }

    }
}
