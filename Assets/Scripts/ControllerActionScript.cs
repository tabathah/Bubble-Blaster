using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerActionScript : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Behaviour_Pose controllerPose;
    private GameObject collidingObject;
    private GameObject objectInHand;

    public GameObject attrPrefab;
    private GameObject attractor;
    private Transform attrTransform;
    private Vector3 hitPoint;

    private void Start()
    {
        attractor = Instantiate(attrPrefab);
        attrTransform = attractor.transform;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.tag);
        if(other.tag == "DispenseButton")
        {
            other.gameObject.GetComponentInParent<FreezeMachine>().dispense();
        }
        else if (other.tag == "ClearButton")
        {
            other.gameObject.GetComponentInParent<FreezeMachine>().clearIceCream();
        }
        else
        {
            SetCollidingObject(other);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "DispenseButton" && other.gameObject.tag != "ClearButton")
        {
            SetCollidingObject(other);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
		if(objectInHand.tag == "Pitcher")
		{
			objectInHand.transform.position = controllerPose.transform.position + (controllerPose.transform.rotation * 
				new Vector3(0, -objectInHand.transform.lossyScale.y * 1.75f, objectInHand.transform.lossyScale.z * 0.75f));
			objectInHand.transform.rotation = controllerPose.transform.rotation * Quaternion.Euler(0, -90, -30);
		}
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }

        objectInHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAction.GetLastStateDown(handType))
        {
            if (collidingObject && collidingObject.tag != "Bubble")
            {
                GrabObject();
            }
        }

        if (grabAction.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();

            }
        }

        if (teleportAction.GetLastState(handType))
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 1000))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                if(hit.collider.gameObject.tag == "Pitcher" || hit.collider.gameObject.tag == "IceCreamBowl")
                {
                    Vector3 distVec = gameObject.transform.position - hit.collider.gameObject.transform.position;
                    hit.collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Normalize(distVec) * 2;
                    hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    hit.collider.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                    //hit.collider.gameObject.transform.rotation = controllerPose.transform.rotation * Quaternion.Euler(0, -90, -30);
                }
            }
        }
        else
        {
            attractor.SetActive(false);
        }
    }

    public bool GetTeleportDown()
    {
        return teleportAction.GetStateDown(handType);
    }

    public bool GetGrab()
    {
        return grabAction.GetState(handType);
    }

    private void ShowLaser(RaycastHit hit)
    {
        attractor.SetActive(true);
        attrTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, 0.5f);
        attrTransform.LookAt(hitPoint);
        attrTransform.localScale = new Vector3(attrTransform.localScale.x, attrTransform.localScale.y, hit.distance);
    }

}
