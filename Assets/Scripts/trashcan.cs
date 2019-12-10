using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashcan : MonoBehaviour
{
    public GameObject[] items = new GameObject[1];
    public GameObject[] itemSpawner = new GameObject[1];

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (collision.collider.gameObject.tag == items[i].tag)
            {
                Debug.Log("Trashed: " + collision.collider.gameObject.tag);
                Destroy(collision.collider.gameObject);
                GameObject b = Instantiate(items[i], itemSpawner[i].transform.position + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);
            }
        }

    }
}
