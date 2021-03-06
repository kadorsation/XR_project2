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
        
        if(PhotonNetwork.IsMasterClient)
        {
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"),
                new Vector3(7.93f, -0.33f, 16.2f), Quaternion.identity, 0, new object[] { PV.ViewID });
        }
        else
        {
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MyDragon"),
                new Vector3(11.96f, -0.99f, -9.26f), Quaternion.identity, 0, new object[] { PV.ViewID });
        }
    	
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
