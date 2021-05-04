using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{

	public static RoomManager Instance;

	void Awake()
	{

		if(Instance)
		{
			Debug.Log("RoomManager set!");
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Instance = this;
	}

	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if(scene.buildIndex >= 1)
		{
			if (PlayerManager.LocalPlayerInstance==null)
				PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerManager"), new Vector3(0f,5f,0f), Quaternion.identity);
		}
	}
}
