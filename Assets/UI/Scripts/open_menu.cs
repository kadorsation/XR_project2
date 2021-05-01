using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class open_menu : MonoBehaviour
{
    public GameObject menu_item;
    public void click()
    {
        Debug.Log("click!");
        if (menu_item.activeInHierarchy)
        {
            menu_item.SetActive(false);
        }
        else
        {
            menu_item.SetActive(true);
        }

    }
}
