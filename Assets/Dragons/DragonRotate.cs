using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRotate : MonoBehaviour
{
    // Start is called before the first frame update
    float fRotateSpeed = 30;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PositionControl();
    }

	void PositionControl()
	{

		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(Vector3.up * Time.deltaTime * fRotateSpeed);
		}
		else if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.down * Time.deltaTime * fRotateSpeed);
		}
	}
}
