using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotreset : MonoBehaviour
{
    public GameObject target;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target.transform.rotation = camera.transform.rotation;
    }
}
