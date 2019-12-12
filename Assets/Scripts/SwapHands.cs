using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SwapHands : MonoBehaviour
{
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean swapAction;
	public SteamVR_Action_Boolean gripAction;
	public SteamVR_Behaviour_Pose controllerPose;
	private GameObject collidingObject;
	private GameObject objectInHand;
	private bool swapped = false;

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
		if (other.tag == "DispenseButton")
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
		if (!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}

	private void GrabObject()
	{
		objectInHand = collidingObject;
		collidingObject = null;
		if (objectInHand.tag == "Pitcher")
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
		if (gripAction.GetLastStateDown(handType))
		{
			if (collidingObject && collidingObject.tag != "Bubble")
			{
				GrabObject();
			}
		}

		if (gripAction.GetLastStateUp(handType))
		{
			if (objectInHand)
			{
				ReleaseObject();

			}
		}
		if (swapAction.GetState(SteamVR_Input_Sources.LeftHand) && swapAction.GetState(SteamVR_Input_Sources.RightHand) && !swapped)
		{
			print("Swapped");
			swapped = true;
			ControllerActionScript controlScript = GetComponent<ControllerActionScript>();
			controlScript.enabled = !controlScript.enabled;
			LaserScript laserScript = GetComponent<LaserScript>();
			laserScript.enabled = !laserScript.enabled;

		}
		else if(!swapAction.GetState(SteamVR_Input_Sources.LeftHand) && !swapAction.GetState(SteamVR_Input_Sources.RightHand) && swapped)
		{
			print("deswap");
			swapped = false;
		}
	}
}
