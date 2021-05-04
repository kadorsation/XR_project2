using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;
    
    GameObject controller;

    public static GameObject LocalPlayerInstance;

	void Awake()
	{
        PV = GetComponent<PhotonView>();
        DontDestroyOnLoad(gameObject);
	}

    // Start is called before the first frame update
    void Start()
    {
        if(PV.IsMine)
        {
        	CreateController();
            LocalPlayerInstance = gameObject;
        }
    }

    void CreateController()
    {
    	Debug.Log("Instantiated Player Controller");
        
    	controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MyRobot"), 
            new Vector3(0f,5f,0f), Quaternion.identity, 0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
