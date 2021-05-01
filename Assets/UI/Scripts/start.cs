using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    void Start()
    {

    }
    public GameObject menu_item;
    public Text change;
    public void start_click()
    {
        Debug.Log("click start!");
        menu_item.SetActive(false);
        change.text = "CONTINUE";
    }
}
