using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
//using Photon.Pun;
//using Photon.Realtime;

public class DragonHealth : MonoBehaviour, IDamageable
{
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    PlayerManager playerManager;
    Rigidbody rb;
    //PhotonView PV;
    private Animator animator;
    public GameObject player;

    public Image slider_full;
    public Text slider_number;
    // Start is called before the first frame update
    public GameObject ani;
    public GameObject dragon;
    private Animator ani_animator, dragon_animator;
    public GameObject player_camera, UI_camera, des_camera, anime_camera, dragon_UI, end_camera;
    int time_set = 26;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();

    }

    void Start()
    {
        slider_number.text = maxHealth.ToString() + "/" + maxHealth.ToString();
        end_camera.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        Debug.Log("I do");
        animator.SetInteger("dragon_motion", 4);

        currentHealth -= damage;
        slider_full.fillAmount = currentHealth / maxHealth;
        slider_number.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            paly_anime();

            //Die();
        }

        //PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }
    /*
    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine)
            return;

        Debug.Log("took damage: " + damage);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            //Die();
        }

    }
    */
    public void paly_anime()
    {
        player_camera.SetActive(false);
        UI_camera.SetActive(false);
        des_camera.SetActive(false);
        anime_camera.SetActive(true);
        dragon_UI.SetActive(false);
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
            Debug.Log("takeOff and copy end");
            dragon_animator.Play("takeOff");
            Instantiate(end_camera, new Vector3(3, 0, 0), new Quaternion(0, 90, 0, 0));

        }
    }


}
