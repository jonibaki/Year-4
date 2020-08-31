using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Control all the networking behaaviours of Photon server
/// </summary>
namespace Monopoly
{
    public class NetworkManager : MonoBehaviourPunCallbacks,IInRoomCallbacks
    {
        public static NetworkManager instance;
        public bool IsConnectToMaster, IsConnectToRoom;
        public Button Online, Offline;
     

        void Awake() {
            if (instance == null)
            {
                instance = this; //Creates a Singleton object

            }
            else {
                if (instance != this) {
                    Destroy(gameObject);
                    instance = this;
                }
            }
            DontDestroyOnLoad(gameObject);

        }
        
        void Start()
        {
            if (!PhotonNetwork.IsConnected) {
                Debug.Log("Connecting to server");
                PhotonNetwork.ConnectUsingSettings(); //Conect to Master Photon Server
                //DontDestroyOnLoad(gameObject);
                IsConnectToMaster = false;
                IsConnectToRoom = false;

            }
         
        }
        
        public override void OnConnectedToMaster()
        {
            IsConnectToMaster = false;
          
            //Require to modify this code, may be fix it within player Quit button in game scene
            Online.gameObject.SetActive(true);
            Offline.gameObject.SetActive(false);
            
            Debug.Log("OnConnectedToMaster called and Master Connected");
            
            PhotonNetwork.NickName = PlayerNetwork.Instance.PlayerName;
            PhotonNetwork.JoinLobby(TypedLobby.Default);
            PhotonNetwork.AutomaticallySyncScene = true;
            //Customized setting for sending data rate
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 10;
        }

        public override void OnJoinedRoom()
        {
           
            Debug.Log("On Joined room called");
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby() called");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
          
            IsConnectToMaster = false;
            IsConnectToRoom = false;
            Debug.Log(cause);
        }

    
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("Called from Script");

        }
    

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 8 });
        }
    }
}