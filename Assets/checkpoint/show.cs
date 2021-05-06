using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class show : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject img;
    public GameObject cp_word;
    public GameObject cp;
    bool start_clock;
    float timer;

    void Start()
    {
    	timer=0.0f;
    	start_clock=false;
        // img = GameObject.Find("dialog");
        // cp = GameObject.Find("check_point1");
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		if(Math.Abs(transform.position.x - cp.transform.position.x) < 0.1f && Math.Abs(transform.position.z - cp.transform.position.z) < 0.1f){
			img.SetActive(true);
			cp_word.SetActive(true);
			if(!start_clock){
				timer=0;
				start_clock=true;	
			}
			
		}   

		if(timer > 5.0f){
			img.SetActive(false);
			cp_word.SetActive(false);
		}  
    }
}
