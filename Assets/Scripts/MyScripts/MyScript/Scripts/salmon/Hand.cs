using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    public Text T;
    //public SceneManage SM;
    public SteamVR_Action_Boolean m_GrabAction = null;
    public GameObject player;
    //private GameObject SceneManager;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Interact m_CurrentInteractable = null;
    public List<Interact> m_ContactInteractable = new List<Interact>();

    private Vector3 oldposition;
    private AudioSource dropsound;
    private int wheretogo = 0;

    private void Awake()
    {
        T.enabled = false;
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("SceneManger");
        //SceneManager = objs[1];
        //if(SceneManager)
        //    Debug.Log("ss");
        
        dropsound = GetComponent<AudioSource>();
        dropsound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<test_state>().m_CurrentState == 3)
        {
            if (player.GetComponent<test_state>().isOpen)
            {
                m_ContactInteractable.Clear();
                player.GetComponent<test_state>().isOpen = false;
                SceneChange(player.GetComponent<test_state>().m_Togo);
            }
        }
        if (player.GetComponent<test_state>().m_CurrentState == 4)
        {
            if (player.GetComponent<test_state>().isOpen)
            {
                m_ContactInteractable.Clear();
                player.GetComponent<test_state>().isOpen = false;
                SceneChange(player.GetComponent<test_state>().m_Togo);
            }
        }
        if (player.GetComponent<test_state>().m_CurrentState == 7)
        {
            if (player.GetComponent<test_state>().isOpen)
            {
                m_ContactInteractable.Clear();
                player.GetComponent<test_state>().isOpen = false;
                SceneChange(player.GetComponent<test_state>().m_Togo);
                
            }
        }
        // Down 
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            //print(m_Pose.inputSource + "Trigger Down");
            /*
            if (player.GetComponent<test_state>().passwordboardon)
            {
                player.GetComponent<test_state>().NoPasswordBoard();
                player.GetComponent<test_state>().passwordboardon = false;
            }
            */
            if(player.GetComponent<test_state>().isNewsPaperHeld)
            {
                player.GetComponent<test_state>().NoNewsPaper();
                player.GetComponent<test_state>().isNewsPaperHeld = false;
            }
            if (player.GetComponent<test_state>().Diaryon)
            {
                player.GetComponent<test_state>().NoDiary();
                player.GetComponent<test_state>().Diaryon = false;
            }

            Pickup();
        }

        // Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + "Trigger Up");
            //Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;
        Debug.Log("In");
        m_ContactInteractable.Add(other.gameObject.GetComponent<Interact>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;
         
        m_ContactInteractable.Remove(other.gameObject.GetComponent<Interact>());
    }

    public void Pickup()
    {
        // Get nearest
        m_CurrentInteractable = GetNearestInteractable();

        if (m_CurrentInteractable.m_ActiveHand)
        {
            m_CurrentInteractable.m_ActiveHand.Drop();
            return;
        }
        Debug.Log("XD");
        // Show
        T.enabled = true;
        T.text = m_CurrentInteractable.mention;

        Invoke("NoText", 2);
        Debug.Log("GG");

        // Key test
        if (m_CurrentInteractable.gameObject.name == "key")
        {
            player.GetComponent<test_state>().isKeyHeld = true;
            player.GetComponent<test_state>().key.SetActive(false);

            // If held key, then can go
            if (player.GetComponent<test_state>().m_CurrentState == 1)
                player.GetComponent<test_state>().isOpen = true;

            if (player.GetComponent<test_state>().m_CurrentState == 2)
                player.GetComponent<test_state>().isOpen = true;

            return;
        }

        // Shovel test
        if (m_CurrentInteractable.gameObject.name == "shovel")
        {
            player.GetComponent<test_state>().isShovelHeld = true;
            player.GetComponent<test_state>().Shovel.SetActive(false);

            return;
        }

        //Mud test
        if (m_CurrentInteractable.gameObject.name == "mud")
        {
            if (player.GetComponent<test_state>().isShovelHeld)
            {
                player.GetComponent<test_state>().Mud.SetActive(false);
                player.GetComponent<test_state>().key.SetActive(true);
                player.GetComponent<test_state>().isOpen = true;
                /*
                m_CurrentInteractable = player.GetComponent<test_state>().key.gameObject.GetComponent<Interact>();
                m_CurrentInteractable.transform.position = transform.position;

                // Attach
                Rigidbody targetBody2 = m_CurrentInteractable.GetComponent<Rigidbody>();
                m_Joint.connectedBody = targetBody2;

                // Set active hand
                m_CurrentInteractable.m_ActiveHand = this;*/
            }
            return;
        }

        // NewsPaper
        if (m_CurrentInteractable.gameObject.name == "newspaper")
        {
            if (player.GetComponent<test_state>().m_CurrentState == 4)
            {
                Debug.Log("Look News");
                if (!player.GetComponent<test_state>().isNewsPaperHeld)
                {
                    player.GetComponent<test_state>().ShowNewsPaper();
                    player.GetComponent<test_state>().isNewsPaperHeld = true;
                }
                else
                {
                    player.GetComponent<test_state>().NoNewsPaper();
                    player.GetComponent<test_state>().isNewsPaperHeld = false;
                }
            }

            return;
        }

        // Box test
        if (m_CurrentInteractable.gameObject.name == "box")
        {
            if (player.GetComponent<test_state>().isKeyHeld)
            {
                if (player.GetComponent<test_state>().m_CurrentState == 3)
                {
                    Debug.Log("In Box");
                    player.GetComponent<test_state>().ShowTimeBox();
                    player.GetComponent<test_state>().ShowLock();
                    player.GetComponent<test_state>().ShowPasswordBoard();
                    player.GetComponent<test_state>().isKeyHeld = false;
                }
            }
            return;
        }

        // Timebox test
        if (m_CurrentInteractable.gameObject.name == "timebox")
        {
            if (player.GetComponent<test_state>().m_CurrentState == 7)
            {
                if (!player.GetComponent<test_state>().passwordboardon)
                {
                    player.GetComponent<test_state>().ShowPasswordBoard();
                    player.GetComponent<test_state>().passwordboardon = true;
                }
                else
                {
                    player.GetComponent<test_state>().NoPasswordBoard();
                    player.GetComponent<test_state>().passwordboardon = false;
                }
            }
            return;
        }

        // Diary yest
        if (m_CurrentInteractable.gameObject.name == "diary")
        {
            if (player.GetComponent<test_state>().m_CurrentState == 7)
            {
                if (!player.GetComponent<test_state>().Diaryon)
                {
                    player.GetComponent<test_state>().ShowDiary();
                    player.GetComponent<test_state>().Diaryon = true;
                }
                else
                {
                    player.GetComponent<test_state>().NoDiary();
                    player.GetComponent<test_state>().Diaryon = false;
                }
            }
            return;
        }

        // Door test
        if (m_CurrentInteractable.gameObject.name == "door")
        {
            Debug.Log("In");
            if (player.GetComponent<test_state>().isOpen)
            {
                /*
                Debug.Log("");
                //player.GetComponent<test_state>().SceneState++;
                Debug.Log(player.GetComponent<test_state>().SceneState);
                Debug.Log(player.GetComponent<test_state>().Max_state);
                if (player.GetComponent<test_state>().SceneState <= player.GetComponent<test_state>().Max_state)
                {
                    Debug.Log(player.GetComponent<test_state>().GetSceneState());
                    SceneManager.GetComponent<SceneManage>().SceneChange(player.GetComponent<test_state>().GetSceneState()+1);
                }*/
                if (player.GetComponent<test_state>().m_CurrentState == 4)
                {
                    return;
                }
                m_ContactInteractable.Clear();
                SceneChange(m_CurrentInteractable.GetComponent<door_state>().togo);
            }
            return;
        }
        if (m_CurrentInteractable.gameObject.name == "Cube")
        {
                m_ContactInteractable.Clear();
                SceneChange(player.GetComponent<test_state>().m_Togo);
            return;
        }



        // Null check
        if (!m_CurrentInteractable)
            return;

        /* Already held, check
        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();
        */
        // Position
        m_CurrentInteractable.transform.position = transform.position;

        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;
    }

    public void Drop()
    {
        // Null Check
        if (!m_CurrentInteractable)
            return;
        dropsound.PlayDelayed(1);
        
        // Disable text
        T.enabled = false;

        // State
        if (m_CurrentInteractable.gameObject.name == "key")
        {
            player.GetComponent<test_state>().isKeyHeld = false;
        }

        // Apply velocity
        Rigidbody target = m_CurrentInteractable.GetComponent<Rigidbody>();
        target.velocity = m_Pose.GetVelocity();
        target.angularVelocity = m_Pose.GetAngularVelocity();
        
        // Detach
        m_Joint.connectedBody = null;

        // Clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    private Interact GetNearestInteractable()
    {
        Interact nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interact interaction in m_ContactInteractable)
        {
            Debug.Log(interaction.gameObject.name);
            distance = (interaction.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interaction;
            }
        }

        return nearest;
    }
    public void SceneChange(int SceneState)
    {
        wheretogo = SceneState;
        Debug.Log("Load Scene!");
        Invoke("Sc", 2);
    }

    private void Sc()
    {
        SceneManager.LoadScene(wheretogo);
    }

    private void NoText()
    {
        T.enabled = false;
    }
}
