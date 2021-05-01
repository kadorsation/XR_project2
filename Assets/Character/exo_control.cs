using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exo_control : MonoBehaviour
{
    // Start is called before the first frame update
	private Animator animator;
	float fMoveSpeed = 3;
	public enum RotationAxes
	{
 		MouseXAndY = 0,
 		MouseX = 1,
 		MouseY = 2
 	}
 
 	public RotationAxes m_axes = RotationAxes.MouseXAndY;
 	public float m_sensitivityX = 10f;
 	public float m_sensitivityY = 10f;
 
 	public float m_minimumX = -360f;
 	public float m_maximumX = 360f;
 	public float m_minimumY = -45f;
 	public float m_maximumY = 45f;
 
 	float m_rotationY = 0f;

    void Start () {
        animator = GetComponent<Animator>();
        if (GetComponent<Rigidbody>()) {
  			GetComponent<Rigidbody>().freezeRotation = true;
 		}
        //Debug.Log("attack!");
    }
 	void Update () {
 		PositionControl();
 		AngelControl();
 		MotionControl();
    }

    void PositionControl() {
		if(Input.GetKey(KeyCode.W)) {
			transform.Translate(Vector3.forward * Time.deltaTime * fMoveSpeed );
		}
		else if(Input.GetKey(KeyCode.S)) {
			transform.Translate(Vector3.back * Time.deltaTime * fMoveSpeed);
		}

		/*if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(Vector3.up * Time.deltaTime * fRotateSpeed);
		}
		else if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.down * Time.deltaTime * fRotateSpeed);
		}*/
	}

	void AngelControl() {
		if (m_axes == RotationAxes.MouseXAndY) {
	  		float m_rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * m_sensitivityX;
	  		m_rotationY += Input.GetAxis ("Mouse Y") * m_sensitivityY;
	  		m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);
	 
	  		transform.localEulerAngles = new Vector3 (-m_rotationY, m_rotationX, 0);
	 	} 
	 	else if (m_axes == RotationAxes.MouseX) {
	  		transform.Rotate (0, Input.GetAxis ("Mouse X") * m_sensitivityX, 0);
	 	} 
	 	else {
	  		m_rotationY += Input.GetAxis ("Mouse Y") * m_sensitivityY;
	  		m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);
	 
	  		transform.localEulerAngles = new Vector3 (-m_rotationY, transform.localEulerAngles.y, 0);
	 	}
	}

	void MotionControl() {
 		animator.SetBool("shooting", false);
        if (Input.GetKeyDown(KeyCode.Z))
        {
        	//shoot
        	Debug.Log("shoot");
            animator.SetBool("shooting", true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
        	//stand
        	Debug.Log("stand");
            animator.SetBool("running", false);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
        	//run
        	Debug.Log("run");
            animator.SetBool("running", true);
        }
    }

}