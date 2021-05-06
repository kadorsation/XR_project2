using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
	GameObject t;
	float timer = 0.0f;
	bool s;
    // Start is called before the first frame update
    void Start()
    {
    	t = transform.Find("BigExplosion").gameObject;
    	s=false;
        t.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    	timer += Time.deltaTime;
    	if(timer > 4.0f && timer < 6.0f){
    		if(Random.Range(0.0f, 10.0f) < 2.0f){
    			t.SetActive(true);
    		}
    	}
    	if(timer > 6.0f){
    		t.SetActive(true);
    	}
        
    }
}
