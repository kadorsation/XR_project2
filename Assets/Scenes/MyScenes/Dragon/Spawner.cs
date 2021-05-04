using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;
    public GameObject mouthEnd;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
    	{
            Debug.Log("ss");
    		GameObject fireball = Instantiate(projectile, 
    			new Vector3(mouthEnd.transform.position.x, mouthEnd.transform.position.y, mouthEnd.transform.position.z), 
    			transform.rotation) as GameObject;
			Rigidbody rb = fireball.GetComponent<Rigidbody>();
			rb.velocity = transform.forward * projectileSpeed;
    	}
    }
}
