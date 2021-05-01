using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exo_control : MonoBehaviour
{
    // Start is called before the first frame update
	private Animator animator;
    void Start () {
        animator = GetComponent<Animator>();
        //Debug.Log("attack!");
    }
 	void Update () {
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
