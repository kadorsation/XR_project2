using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class absorb : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ani;
    public GameObject dragon;
    private Animator ani_animator, dragon_animator;
    int time_set = 26;
    void Start()
    {
        
    }
    public void paly_anime()
    {
        ani_animator = ani.GetComponent<Animator>();
        dragon_animator = dragon.GetComponent<Animator>();
        Debug.Log("eat");
        ani_animator.Play("eat");
        InvokeRepeating("timer", 1, 1);

    }

    void timer()
    {
        time_set -= 1;
        if (time_set == 0)
        {
            CancelInvoke("timer");
            Debug.Log("takeOff");
            dragon_animator.Play("takeOff");
        }
    }
}
