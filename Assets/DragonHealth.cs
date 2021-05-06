using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DragonHealth : MonoBehaviour, IDamageable
{
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    PlayerManager playerManager;
    Rigidbody rb;
    PhotonView PV;
    private Animator animator;
    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            //m_CameraRig = SteamVR_Render.Top().origin;
            //animator = GetComponent<Animator>();
        }
        else
        {
            //Destroy(GetComponentInChildren<Camera>().gameObject);
            //Destroy(rb);
        }      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("I do");
        animator.SetInteger("dragon_motion", 4);
        /*
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            //Die();
        }
        */
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }
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
}
