﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMachine : MonoBehaviour
{
    public float[] amount = new float[Flavor.maxFlavors];
    public GameObject bowl;
    public GameObject bowlDispenser;
    public GameObject icecreamScoop;
    private GameObject lastBowl;

    private AudioSource playSound;
    public AudioClip dispenseSound;
    public AudioClip clearSound;
    
    // Start is called before the first frame update
    void Start()
    {
        //myBDispenser = Instantiate(bowlDispenser, this.transform.position + new Vector3(0, this.transform.lossyScale.y * 0.5f, 0), Quaternion.identity);
        playSound = gameObject.GetComponent<AudioSource>();
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

        if (!playSound.isPlaying)
        {
            playSound.clip = clearSound;
            playSound.Play();
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
            playSound.clip = dispenseSound;
            playSound.Play();

            GameObject b = Instantiate(bowl, bowlDispenser.transform.position + new Vector3(0, bowlDispenser.transform.lossyScale.y * 0.5f, 0), Quaternion.identity);
            b.GetComponent<IceCreamBowl>().addIceCream(this.amount);
            b.GetComponent<IceCreamBowl>().icecreamScoop = this.icecreamScoop;
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
