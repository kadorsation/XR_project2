using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    // Start is called before the first frame update
    public int time;
    int time_temp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time_temp = time;
        InvokeRepeating("time_count", 1, 1);

    }

    void time_count()
    {
        time_temp -= 1;
        if (time_temp == 0)
        {
            CancelInvoke("time_count");
        }
    }
}
