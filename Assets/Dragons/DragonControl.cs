using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("dragon_motion", -1);
        if (Input.anyKeyDown)
        {
            Debug.Log(Input.anyKeyDown);
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("Scream");
                animator.SetInteger("dragon_motion", 0);
            }
            if (Input.GetKey(KeyCode.G))
            {
                Debug.Log("attackFlame");
                animator.SetInteger("dragon_motion", 1);
            }
            if (Input.GetKey(KeyCode.H))
            {
                Debug.Log("attackMouth");
                animator.SetInteger("dragon_motion", 2);
            }
            if (Input.GetKey(KeyCode.J))
            {
                Debug.Log("attackFly");
                animator.SetInteger("dragon_motion", 3);
            }
            if (Input.GetKey(KeyCode.K))
            {
                Debug.Log("Defend");
                animator.SetInteger("dragon_motion", 4);
            }
            if (Input.GetKey(KeyCode.L))
            {
                Debug.Log("die");
                animator.SetInteger("dragon_motion", 5);
            }
        }
    }
}
