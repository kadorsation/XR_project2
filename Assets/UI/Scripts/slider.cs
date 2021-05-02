using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    public Image slider_full;
    public float Max = 30;
    static public float now;
    public Text slider_number;
    // Start is called before the first frame update
    void Start()
    {
        slider_number.text = "0 / Max";
        now = Max;
    }

    // Update is called once per frame
    void Update()
    {
        if(now < 0)
        {
            now = 0;
            return;
        }
        slider_full.fillAmount = now / Max;
        slider_number.text = now.ToString() + "/" + Max.ToString();
    }
}
