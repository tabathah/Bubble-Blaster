using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserScript : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    private float timer = 0;
    private float upTime = 0;
    private float lastTime;
    private bool onCd;

    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        lastTime = Time.realtimeSinceStartup;
        onCd = false;
    }

    // Update is called once per frame
    void Update()
    {
        float currTime = Time.realtimeSinceStartup;
        if(teleportAction.GetState(handType) && timer <= 0 && upTime <= 0.3f)
        {
            onCd = false;
            upTime += currTime - lastTime;
            RaycastHit hit;
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                if(hit.collider.gameObject.tag == "Dispenser")
                {
                    hit.collider.gameObject.GetComponent<BubbleDispenser>().pressButton();
                }
                else if(hit.collider.gameObject.tag == "Bubble")
                {
                    hit.collider.gameObject.GetComponent<Bubble>().shootBubble();
                }
            }
        }
        else
        {
            if(!onCd)
            {
                onCd = true;
                timer = 1;
                upTime = 0;
            }
            laser.SetActive(false);
            timer -= currTime - lastTime;
        }
        lastTime = currTime;
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, 0.5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }

}
