using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pitcher : MonoBehaviour
{
    public float[] amount = new float[Flavor.maxFlavors];
    private float totalAmount;
    private float maxCapacity;
    public GameObject fillObject;
    int currentFillIndex = 0;
    private GameObject[] fills = new GameObject[50];
    //private int[] fillOrder = new int[50];
    private Text m_Text;
    private float disableTime;
    private Vector3 disablePos;
    private Quaternion disableRot;
    public Vector3 startPos;
    public Quaternion startRot;
    public Text val1;
    public Text val2;
    public Text val3;

    private AudioSource sendToFreezeMachineSound;

    // Start is called before the first frame update
    void Start()
    {
        m_Text = gameObject.GetComponentInChildren<Text>();

        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;

        sendToFreezeMachineSound = gameObject.GetComponent<AudioSource>();

        totalAmount = 0.0f;
        maxCapacity = 5.0f;

    }

    public void instantiateFill(int flav) {
        if (!fills[currentFillIndex])
        {
            fills[currentFillIndex] = Instantiate(fillObject, gameObject.transform.position, Quaternion.identity);
        }
        fills[currentFillIndex].SetActive(true);
        fills[currentFillIndex].GetComponent<Flavor>().changeFlavor(flav);

        currentFillIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetComponent<Rigidbody>().useGravity)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        if(GetComponent<Rigidbody>().freezeRotation)
        {
            GetComponent<Rigidbody>().freezeRotation = false;
        }


        for (int i = 0; i < currentFillIndex; i++) {

            Vector3 currPos = new Vector3(0, 0, 0);
            currPos.y -= gameObject.transform.lossyScale.y * 0.9f;
            currPos.y += 0.1f * 0.02f + (i * 0.1f * 0.02f);
            fills[i].transform.localScale = new Vector3(0.2f, 0.1f * 0.02f, 0.2f);
            fills[i].transform.position = gameObject.transform.rotation * currPos + gameObject.transform.position;
            fills[i].transform.rotation = gameObject.transform.rotation;

        }
      
        if (disableTime > 0)
        {
            disableTime -= Time.deltaTime;
            gameObject.transform.position = disablePos;
            gameObject.transform.rotation = disableRot;
        }
    }

    void UpdateText()
    {
        val1.text = System.Math.Round(amount[0], 1).ToString() + " Blue";
        val2.text = System.Math.Round(amount[1], 1).ToString() + " Red";
        val3.text = System.Math.Round(amount[2], 1).ToString() + " Green";
    }

    public void clearPitcher()
    {
        currentFillIndex = 0;
        for (int i = 0; i < amount.Length; i++)
        {
            amount[i] = 0;
        }

        for (int i = 0; i < 50; i++) {
            if (fills[i])
            {
                fills[i].SetActive(false);
            }
        }
        totalAmount = 0;
        UpdateText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bubble")
        {
            Bubble b = collision.collider.gameObject.GetComponent<Bubble>();
            Vector3 distVec = collision.collider.gameObject.transform.position - gameObject.transform.position;
            distVec = Quaternion.Inverse(gameObject.transform.rotation) * distVec;
            if (!b.floating && distVec.y >= gameObject.transform.lossyScale.y * 0.5f)
            {
                Flavor flav = collision.collider.gameObject.GetComponent<Flavor>();
                if(flav)
                {
                    instantiateFill(flav.flavor);
                    if (totalAmount < maxCapacity)
                    {
                        amount[flav.flavor] += 0.1f;
                        amount[flav.flavor] = (float)System.Math.Round(amount[flav.flavor], 1);
                        totalAmount += 0.1f;
                        totalAmount = (float)System.Math.Round(totalAmount, 1);
                    }
                    UpdateText();
                }
                Destroy(collision.collider.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PitcherSlot")
        {
            if (!sendToFreezeMachineSound.isPlaying)
            {
                sendToFreezeMachineSound.Play();
            }

            FreezeMachine fm = other.gameObject.GetComponentInParent<FreezeMachine>();
            fm.loadPitcher(amount);
            clearPitcher();
            UpdateText();
            disableTime = 0.1f;
            disablePos = gameObject.transform.position;
            disableRot = gameObject.transform.rotation;
        }
    }
}
