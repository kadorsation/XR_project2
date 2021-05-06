using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_attack : MonoBehaviour
{
    // Start is called before the first frame update
	private Animator animator;
	bool attack = true;
    void Start () {
        animator = GetComponent<Animator>();
        //Debug.Log("attack!");
    }
 	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
        	Debug.Log("attack: " + attack);
            animator.SetBool("attack", attack);
            attack = !attack;
        }
    }
}
