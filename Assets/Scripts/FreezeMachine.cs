using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text val1;
    public Text val2;
    public Text val3;

	private bool hasIceCream;

    private float meltTime;
    private float maxMeltTime;

    // Start is called abefore the first frame update
    void Start()
    {
		hasIceCream = false;
        //myBDispenser = Instantiate(bowlDispenser, this.transform.position + new Vector3(0, this.transform.lossyScale.y * 0.5f, 0), Quaternion.identity);
        playSound = gameObject.GetComponent<AudioSource>();

        meltTime = 0.0f;
        maxMeltTime = 10.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasIceCream)
        {
            meltTime = -15.0f;
        }

        meltTime += Time.deltaTime;

        if (meltTime >= maxMeltTime)
        {
            meltTime = 0.0f;

            for (int i = 0; i < amount.Length; i++)
            {
                amount[i] = Mathf.Max(amount[i] - 0.1f, 0);
            }
            UpdateText();
        }
    }

    void UpdateText()
    {
        val1.text = System.Math.Round(amount[0], 1).ToString() + " Blue";
        val2.text = System.Math.Round(amount[1], 1).ToString() + " Red";
        val3.text = System.Math.Round(amount[2], 1).ToString() + " Green";
    }

    public void clearIceCream() {
		if(hasIceCream)
		{
			hasIceCream = false;
			print("emptying");
			for (int i = 0; i < amount.Length; i++)
			{
				amount[i] = 0;
			}

			if (!playSound.isPlaying)
			{
				playSound.clip = clearSound;
				playSound.Play();
			}

			val1.text = System.Math.Round(amount[0], 1).ToString() + " Blue";
			val2.text = System.Math.Round(amount[1], 1).ToString() + " Red";
			val3.text = System.Math.Round(amount[2], 1).ToString() + " Green";
		}
        
    }

    public void dispense() {
		if(hasIceCream)
		{
			print("dispensing");
			float len = 100;
			if (lastBowl)
			{
				len = Vector3.Distance(bowlDispenser.transform.position, lastBowl.transform.position);

				if ((len == 0) && (lastBowl.GetComponent<IceCreamBowl>().isEmpty))
				{
					playSound.clip = dispenseSound;
					playSound.Play();

					lastBowl.GetComponent<IceCreamBowl>().addIceCream(this.amount);
					lastBowl.GetComponent<IceCreamBowl>().icecreamScoop = this.icecreamScoop;
					clearIceCream();
				}
				else
				{
					lastBowl.GetComponent<IceCreamBowl>().destroyBowlAndIceCream();

					playSound.clip = dispenseSound;
					playSound.Play();

					GameObject b = Instantiate(bowl, bowlDispenser.transform.position + new Vector3(0, bowlDispenser.transform.lossyScale.y * 0.5f, 0), Quaternion.identity);
					b.GetComponent<IceCreamBowl>().addIceCream(this.amount);
					b.GetComponent<IceCreamBowl>().icecreamScoop = this.icecreamScoop;
					lastBowl = b;
					clearIceCream();
				}
			}
			else
			{
				playSound.clip = dispenseSound;
				playSound.Play();

				GameObject b = Instantiate(bowl, bowlDispenser.transform.position + new Vector3(0, bowlDispenser.transform.lossyScale.y * 0.5f, 0), Quaternion.identity);
				b.GetComponent<IceCreamBowl>().addIceCream(this.amount);
				b.GetComponent<IceCreamBowl>().icecreamScoop = this.icecreamScoop;
				lastBowl = b;
				clearIceCream();
			}
		}
        
    }

    public void loadPitcher(float[] amt)
    {
        for(int i = 0; i < amount.Length; i++)
        {
            amount[i] += amt[i];
			if(amt[i] > 0)
			{
				hasIceCream = true;
			}
        }
        UpdateText();
    }

}
