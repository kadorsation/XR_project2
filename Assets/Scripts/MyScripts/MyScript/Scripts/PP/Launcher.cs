//using System.Collections;
//using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


namespace Com.XRProject2.Team
{
	
    public class Launcher : MonoBehaviourPunCallbacks
    {
    	[Tooltip("顯示/隱藏 遊戲玩家名稱與 Play 按鈕")]
		[SerializeField]
		private GameObject controlPanel;
		[Tooltip("顯示/隱藏 連線中 字串")]
		[SerializeField]
		private GameObject progressLabel;
    	[Tooltip("遊戲室玩家人數上限. 當遊戲室玩家人數已滿額, 新玩家只能新開遊戲室來進行遊戲.")]
		[SerializeField]
		private byte maxPlayersPerRoom = 4;
        // 遊戲版本的編碼, 可讓 Photon Server 做同款遊戲不同版本的區隔.
        string gameVersion = "1";
 		bool isConnecting;

        void Awake()
        {
            // 確保所有連線的玩家均載入相同的遊戲場景
            PhotonNetwork.AutomaticallySyncScene = true;
            progressLabel.SetActive(false);
			controlPanel.SetActive(true);
        }
 
        // 與 Photon Cloud 連線
        public void Connect()
        {
        	progressLabel.SetActive(true);
			controlPanel.SetActive(false);
            // 檢查是否與 Photon Cloud 連線
            isConnecting = true;

            if (PhotonNetwork.IsConnected)
            {
            	PhotonNetwork.AutomaticallySyncScene = true;
                // 已連線, 嚐試隨機加入一個遊戲室
                PhotonNetwork.JoinRandomRoom();

            }
            else
            {
                // 未連線, 嚐試與 Photon Cloud 連線
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = this.gameVersion;
            }
        }

        public override void OnConnectedToMaster()
		{
		    Debug.Log("PUN 呼叫 OnConnectedToMaster(), 已連上 Photon Cloud.");
		    // 確認已連上 Photon Cloud
		    // 隨機加入一個遊戲室
		    if (isConnecting)
			{
			    PhotonNetwork.JoinRandomRoom();
			}
		}

		public override void OnDisconnected(DisconnectCause cause)
		{
		    Debug.LogWarningFormat("PUN 呼叫 OnDisconnected() {0}.", cause);

		    
		    progressLabel.SetActive(false);
			controlPanel.SetActive(true);
			isConnecting = false;
		}

		public override void OnJoinRandomFailed(short returnCode, string message)
		{
		    Debug.Log("PUN 呼叫 OnJoinRandomFailed(), 隨機加入遊戲室失敗.");
		    
		    // 隨機加入遊戲室失敗. 可能原因是 1. 沒有遊戲室 或 2. 有的都滿了.    
		    // 好吧, 我們自己開一個遊戲室.
		    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
		}

		public override void OnJoinedRoom()
		{
		    Debug.Log("PUN 呼叫 OnJoinedRoom(), 已成功進入遊戲室中.");
		    if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
			{
			    Debug.Log("我是第一個進入遊戲室的玩家");
			    Debug.Log("我得主動做載入場景 'Room for 1' 的動作");
			    PhotonNetwork.LoadLevel("BossRoom");
			}
		}
    }
}