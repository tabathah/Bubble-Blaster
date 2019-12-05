using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pitcher : MonoBehaviour
{
    private float bubbleAmt = 0;
    private Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = bubbleAmt.ToString("F1");
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
                Destroy(collision.collider.gameObject);
                bubbleAmt += 0.1f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
