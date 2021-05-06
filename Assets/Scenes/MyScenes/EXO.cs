using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class EXO : MonoBehaviour, IDamageable
{
    public SteamVR_Action_Boolean isFire = null;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private Animator animator;
    [SerializeField]
    GameObject cameraHolder;

    // Switch Guns
    [SerializeField]
    Item[] items;
    bool grounded;
    int itemIndex;
    int previousItemIndex = -1;

    //=============================//
    Rigidbody rb;

    PhotonView PV;

    // Health
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    PlayerManager playerManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        //playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
        if (PV.IsMine)
        {
            //m_CameraRig = SteamVR_Render.Top().origin;
            animator = GetComponent<Animator>();
            EquipItem(0);
        }
        else
        {
            //Destroy(GetComponentInChildren<Camera>().gameObject);
            //Destroy(rb);
        }
        
       // animator = GetComponent<Animator>();
       // EquipItem(0);
    }

    void Update()
    {
        if (!PV.IsMine)
            return;
           
        if (isFire.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            animator.SetBool("isFire", true);
            items[itemIndex].Use();
        }
    }

    void EquipItem(int _index)
    {
        if (_index == previousItemIndex)
            return;
        itemIndex = _index;

        items[itemIndex].itemGameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }

        previousItemIndex = itemIndex;
        /*
        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        */
    }
    /*
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }
    */
    public void TakeDamage(float damage)
    {
        Debug.Log("I do");
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
    /*
    void Die()
    {
        playerManager.Die();
    }
    */
}
