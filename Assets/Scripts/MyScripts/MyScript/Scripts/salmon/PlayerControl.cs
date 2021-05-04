using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerControl : MonoBehaviour
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

    //private AudioSource walk;

    [SerializeField]
    Item[] items;

    int itemIndex;
    int previousItemIndex = -1;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
        
        //walk = GetComponent<AudioSource>();
       //walk.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        //HandleHead();
        HandleHeight();
        CalculateMovement();
        if (isFire.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            //animator.SetBool("IsFire", true);
            items[0].Use();
        }
        //SnapRotation();

        //if(iswalk && !walk.isPlaying)
        //    walk.Play();
    }
    
    private void HandleHeight()
    { 

        // Cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height;// / 2;
        newCenter.y += m_CharacterController.skinWidth;
        
        newCenter.x = m_CharacterController.center.x;
        newCenter.z = m_CharacterController.center.z;

        m_CharacterController.center = newCenter;
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

        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y+rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }
}

