using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class skill_cd : MonoBehaviour
{
    public int time_set;
    int time_temp;
    public Text time_UI;
    public GameObject cover;
    public int skill_id;
    public GameObject projectile;
    public float projectileSpeed;
    public GameObject dragon;
    public GameObject mouthEnd;
    public GameObject wingEnd1;
    public GameObject wingEnd2;
    private GameObject player;

    private Animator animator;
    private WaitForSeconds shortduration = new WaitForSeconds(1);
    void Start()
    {
        animator = dragon.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //call into_cd function when dragon use skill
    public void into_cd()
    {
        switch (skill_id)
        {
            case 1:
                MouthAttack();
                break;
            case 2:
                AttackFly();
                break;
            case 3:
                Fireball();
                break;
        }
        GetComponent<Button>().interactable = false;
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
            GetComponent<Button>().interactable = true;
            //enable skill
        }
    }
    public void MouthAttack()
    {
        Debug.Log("attackMouth");
        animator.Play("attackMouth");
    }

    public void Fireball()
    {
        Debug.Log("Fireball");
        animator.Play("attackFlame");
        //animator.SetInteger("dragon_motion", 1);
        //Thread.Sleep(1000);
        StartCoroutine (WaitEffect());
        
        
    }

    public void AttackFly()
    {
        Debug.Log("attackFly");
        animator.Play("attackFly"); 
        GameObject wind1 = Instantiate(projectile,
                 new Vector3(wingEnd1.transform.position.x, wingEnd1.transform.position.y, wingEnd1.transform.position.z),
                 transform.rotation) as GameObject;
        GameObject wind2 = Instantiate(projectile,
                 new Vector3(wingEnd2.transform.position.x, wingEnd2.transform.position.y, wingEnd2.transform.position.z),
                 transform.rotation) as GameObject;
        Rigidbody rb1 = wind1.GetComponent<Rigidbody>();
        rb1.velocity = (player.transform.position- wingEnd1.transform.position) * projectileSpeed;

        Rigidbody rb2 = wind2.GetComponent<Rigidbody>();
        rb2.velocity = (player.transform.position - wingEnd2.transform.position) * projectileSpeed;
    }

    private IEnumerator WaitEffect()
    {
        yield return shortduration;
        GameObject fireball = Instantiate(projectile,
                new Vector3(mouthEnd.transform.position.x, mouthEnd.transform.position.y, mouthEnd.transform.position.z),
                transform.rotation) as GameObject;
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
    }
}
