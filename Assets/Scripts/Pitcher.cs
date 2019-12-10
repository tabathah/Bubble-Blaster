using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pitcher : MonoBehaviour
{
    public float[] amount = new float[Flavor.maxFlavors];
    public GameObject fillObject;
    private GameObject[] fills = new GameObject[Flavor.maxFlavors];
    private Text m_Text;
    private float disableTime;
    private Vector3 disablePos;
    private Quaternion disableRot;
    public Vector3 startPos;
    public Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        m_Text = gameObject.GetComponentInChildren<Text>();

        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = new Vector3(0, 0, 0);
        currPos.y -= gameObject.transform.lossyScale.y * 0.9f;
        for (int i = 0; i < amount.Length; i++)
        {
            if (!fills[i])
            {
                fills[i] = Instantiate(fillObject, gameObject.transform.position, Quaternion.identity);
                fills[i].GetComponent<Flavor>().changeFlavor(i);
                fills[i].SetActive(false);
            }
            currPos.y += amount[i] * 0.02f;
            fills[i].transform.localScale = new Vector3(0.2f, amount[i] * 0.02f, 0.2f);
            fills[i].transform.position = gameObject.transform.rotation * currPos + gameObject.transform.position;
            fills[i].transform.rotation = gameObject.transform.rotation;
            currPos.y += amount[i] * 0.02f;
        }
        if (disableTime > 0)
        {
            disableTime -= Time.deltaTime;
            gameObject.transform.position = disablePos;
            gameObject.transform.rotation = disableRot;
        }
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
                    if(amount[flav.flavor] == 0)
                    {
                        fills[flav.flavor].SetActive(true);
                    }
                    amount[flav.flavor] += 0.1f;
                }
                Destroy(collision.collider.gameObject);
            }
        }
        else if (collision.collider.gameObject.tag == "PitcherSlot")
        {
            FreezeMachine fm = collision.collider.gameObject.GetComponentInParent<FreezeMachine>();
            fm.loadPitcher(amount);
            for(int i = 0; i < amount.Length; i++)
            {
                amount[i] = 0;
                fills[i].SetActive(false);
            }
            disableTime = 0.1f;
            disablePos = gameObject.transform.position;
            disableRot = gameObject.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PitcherSlot")
        {
            FreezeMachine fm = other.gameObject.GetComponentInParent<FreezeMachine>();
            fm.loadPitcher(amount);
            for (int i = 0; i < amount.Length; i++)
            {
                amount[i] = 0;
                fills[i].SetActive(false);
            }
            disableTime = 0.1f;
            disablePos = gameObject.transform.position;
            disableRot = gameObject.transform.rotation;
        }
    }
}
