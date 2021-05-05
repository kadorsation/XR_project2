using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks//, IDamageable
{
	
    public float m_Gravity = 30.0f;
    public float m_Sensitivity = 0.1f;
    public float m_Maxspeed = 1.0f;
    public float m_RotateIncrement = 90;
	
    public SteamVR_Action_Boolean isFire = null;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_Speed = 0.0f;
    private bool iswalk = false;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;
    
    private Animator animator;
    [SerializeField] 
	GameObject cameraHolder;
    
	[SerializeField] 
	float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    // Controll
	float verticalLookRotation;
	//bool grounded;
	Vector3 smoothMoveVelocity;
	Vector3 moveAmount;
    
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
        m_CharacterController = GetComponent<CharacterController>();
        //playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		if(PV.IsMine)
		{
            //m_CameraRig = SteamVR_Render.Top().origin;
            m_Head = SteamVR_Render.Top().head;
            animator = GetComponent<Animator>();
            EquipItem(0);
		}
		else
		{
			Destroy(GetComponentInChildren<Camera>().gameObject);
			Destroy(rb);
		}
	}

	void Update()
	{
		if(!PV.IsMine)
			return;
        /*
		Look();
		Move();
		Jump();
        */
        
        //HandleHeight();
        CalculateMovement();
        
        /*
        for (int i = 0; i < items.Length; i++)
		{
			if(Input.GetKeyDown((i + 1).ToString()))
			{
				EquipItem(i);
				break;
			}
		}
        
		if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
		{
			if(itemIndex >= items.Length-1)
			{
				EquipItem(0);
			}
			else
			{
				EquipItem(itemIndex + 1);
			}
		}
		else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
		{
			if(itemIndex <= 0)
			{
				EquipItem(items.Length - 1);
			}
			else
			{
				EquipItem(itemIndex - 1);
			}
		}
        */
        
		if(isFire.GetStateDown(SteamVR_Input_Sources.RightHand))
		{
            animator.SetBool("isFire", true);
            items[itemIndex].Use();
		}
		
        /*
		if(Input.GetMouseButtonDown(0))
		{
			animator.SetBool("IsFire", true);
			items[itemIndex].Use();
		}
        */
        /*
		if(transform.position.y < -10f)
		{
			Die();
		}
        */        
	}
	
    private void HandleHeight()
    {
        /*
        // Cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height;// / 2;
        newCenter.y += m_CharacterController.skinWidth;

        newCenter.x = m_CharacterController.center.x;
        newCenter.z = m_CharacterController.center.z;

        m_CharacterController.center = newCenter;
        */
        
    }

    private void CalculateMovement()
    {
        // Figure out  movement 
        Quaternion orientation = CalculateOrientation();
        Vector3 movement = Vector3.zero;

        // If not moving
        if (m_MoveValue.axis.magnitude == 0)
        {
            m_Speed = 0;
            iswalk = false;
        }
        // Add, clamp
        m_Speed += m_MoveValue.axis.magnitude * m_Sensitivity;
        m_Speed = Mathf.Clamp(m_Speed, -m_Maxspeed, m_Maxspeed);

        // Orientation, and Gravity
        movement += orientation * (m_Speed * Vector3.forward);

        // Gravity
        movement.y -= m_Gravity * Time.deltaTime;
        // Apply 
        m_CharacterController.Move(movement * Time.deltaTime);
        if (m_Speed > 0)
        {
            iswalk = true;
        }
    }

    private Quaternion CalculateOrientation()
    {
        float rotation = Mathf.Atan2(m_MoveValue.axis.x, m_MoveValue.axis.y);
        rotation *= Mathf.Rad2Deg;

        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }
    
    
    void Look()
	{
		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

		verticalLookRotation = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

		cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
	}

	void Move()
	{
		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0 , Input.GetAxisRaw("Vertical")).normalized;
		moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift)? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
	}

	void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			rb.AddForce(transform.up * jumpForce);
		}
	}
    
    
	void EquipItem(int _index)
	{
		if(_index == previousItemIndex)
		 	return;
		itemIndex = _index;

		items[itemIndex].itemGameObject.SetActive(true);

		if(previousItemIndex != -1)
		{
			items[previousItemIndex].itemGameObject.SetActive(false);

		}

		previousItemIndex = itemIndex;

		if(PV.IsMine)
		{
			Hashtable hash = new Hashtable();
			hash.Add("itemIndex", itemIndex);
			PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
		}
	}

	public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
	{
		if(!PV.IsMine && targetPlayer == PV.Owner)
		{
			EquipItem((int)changedProps["itemIndex"]);
		}
	}
    
	public void SetGroundedState(bool _grounded)
	{
		grounded = _grounded;
	}
    /*
	void FixedUpdate()
	{
		if(!PV.IsMine)
			return;

		rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
	}
    */
    /*
	public void TakeDamage(float damage)
	{
		Debug.Log("I do");
        
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
            //Die();
        }
        
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
	}

	[PunRPC]
	void RPC_TakeDamage(float damage)
	{
		if(!PV.IsMine)
			return;

		Debug.Log("took damage: " + damage);

		currentHealth -= damage;
		if(currentHealth <= 0)
		{
			Die();
           
        }
	}

	void Die()
	{
		playerManager.Die();
	}
*/
}
