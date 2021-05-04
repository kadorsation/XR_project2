using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{
    public Camera m_Camera = null;
    public string mention = null;

    private float viewAngle = 120f;
    private bool iskey = true;

    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    
    private void FixedUpdate()
    {
        if (iskey)
        {
            Detect();
        }
        //Detect();
    }
    
    private void Detect()
    {
        Vector3 cameraPosition = m_Camera.transform.position;
        Debug.Log("In");
        if (Vector3.Angle(m_Camera.transform.forward, transform.position - m_Camera.transform.position) <= viewAngle / 2)
        {
            gameObject.SetActive(true);
        }
    }
}
