using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health;

    // Update is called once per frame
    void Update()
    {
        
        if(health <= 0)
        {
        	Destroy(gameObject);
        }
    }
}
