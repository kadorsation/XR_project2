using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    Vector3 start_pos = new Vector3(0,10,20);
    Vector3 end_pos = new Vector3(0,0,0);
    float timer = 0.0f;

    float speed = 12.0f;

    public GameObject flame;
    public GameObject big_fire;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("dragon_motion", 3);
    }

    // Update is called once per frame
    void Update()
    {
    	timer += Time.deltaTime;
    	float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, end_pos, step);

        // Check if the position of the cube and sphere are approximately equal.
      //   if (Vector3.Distance(transform.position, end_pos) < 0.001f)
      // {
          	
      //   }
        if(timer > 4.0f){
        	animator.SetInteger("dragon_motion", 1);
        	flame.SetActive(true);
        }
        if(timer > 12.0f){
        	animator.SetInteger("dragon_motion", -1);
        	flame.SetActive(false);
        	big_fire.SetActive(true);
        }
        
    }
}
