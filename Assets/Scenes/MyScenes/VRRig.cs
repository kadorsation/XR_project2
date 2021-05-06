using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
	public Transform vrTarget;
	public Transform rigTarget;
	public Vector3 trackingPositionOffset;
	public Vector3 trackingRotationOffset;

	public void Map()
	{
		rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
		rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
	}
}

public class VRRig : MonoBehaviour
{
	//public VRMap head;
	public VRMap leftHand;
	public VRMap rightHand;

	public Transform headConstraint;
	public Vector3 headBodyOffset;
	public float turnSmoothness;
    //
    public Transform head;
    public Transform body;
    public Vector3 headPositionOffset;
    public Vector3 headRotationOffset;
    // Start is called before the first frame update
    void Start()
    {
    	headBodyOffset = transform.position - headConstraint.position;
	}

    // Update is called once per frame
    void FixedUpdate()
    {

       // transform.position = headConstraint.position + headBodyOffset;
        
        //transform.forward = Vector3.Lerp(transform.forward, 
        //	Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, turnSmoothness);
        //

        //head.Map();
        transform.position = head.TransformPoint(headPositionOffset);
        transform.rotation = head.rotation * Quaternion.Euler(headRotationOffset);
        //Debug.Log(transform.rotation.ToString() + head.rotation.ToString() + Quaternion.Euler(headRotationOffset).ToString());
        leftHand.Map();
    	rightHand.Map();
    }
}
