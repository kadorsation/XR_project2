using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_state : MonoBehaviour
{
    public int SceneState = 2;
    public int TotalState = 0;
    public int AnimationState = 0;
    public int Max_state = 2;
    public int m_Togo;
    public int m_CurrentState; 

    public bool isKeyHeld = false;
    public bool isOpen = false;
    public bool isShovelHeld = false;
    public bool isNewsPaperHeld = false;

    public GameObject timebox = null;
    public GameObject passwordboard = null;
    public GameObject key = null;
    public GameObject Shovel = null;
    public GameObject Mud = null;
    public GameObject NewsPaper = null;
    public GameObject Lock = null;
    public GameObject Diary = null;

    public bool passwordboardon = false;
    public bool Diaryon = false;

    public void ShowPasswordBoard()
    {
        passwordboard.SetActive(true);
    }

    public void NoPasswordBoard()
    {
        passwordboard.SetActive(false);
    }

    public void ShowTimeBox()
    {
        timebox.SetActive(true);
    }

    public void NoTimeBox()
    {
        timebox.SetActive(false);
    }

    public void ShowNewsPaper()
    {
        NewsPaper.SetActive(true);
    }

    public void NoNewsPaper()
    {
        NewsPaper.SetActive(false);
    }

    // Lock
    public void ShowLock()
    {
        Lock.SetActive(true);
    }

    public void NoLock()
    {
        Lock.SetActive(false);
    }

    // Diary
    public void ShowDiary()
    {
        Diary.SetActive(true);
    }

    public void NoDiary()
    {
        Diary.SetActive(false);
    }

    public int GetSceneState()
    {
        return SceneState;
    }
}
