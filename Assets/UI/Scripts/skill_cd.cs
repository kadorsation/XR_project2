using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skill_cd : MonoBehaviour
{
    public int time_set;
    int time_temp;
    public Text time_UI;
    public GameObject cover;
    void Start()
    {
    }
    //call into_cd function when dragon use skill
    public void into_cd()
    {
        //unable skill
        time_temp = time_set;
        cover.SetActive(true);
        time_UI.text = time_temp + "";
        InvokeRepeating("timer", 1, 1);
    }
    void timer()
    {
        time_temp -= 1;
        time_UI.text = time_temp + "";
        if (time_temp == 0)
        {
            time_UI.text = "";
            CancelInvoke("timer");
            cover.SetActive(false);
            //enable skill
        }
    }
}
