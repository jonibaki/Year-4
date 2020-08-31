using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monopoly {
    public class PlayerNetwork : MonoBehaviour
    {
        public static PlayerNetwork Instance;
        private PhotonView PhotonView;
        private int PlayersInGame=0;
        public string PlayerName { get; private set; }
    
        public PlayerMove[] PlayerPrefab;
        private PlayerMove LocalPlayer;

        public int PlayerID;
        
      
        private void Awake()
        {
            Instance = this;
            PhotonView = GetComponent<PhotonView>();
            PlayerName = "Player#" + Random.Range(1000, 9999); //use DB id insread of Random number
            SceneManager.sceneLoaded += OnSceneFinishedLoading;
        }

        private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
            if (scene.name == "Game") {
                if (PhotonNetwork.IsMasterClient)
                {
                    MasterLoadedGame();
                }
                else {
                    ClientLoadedGame();
                }
            }

        }

        private void MasterLoadedGame() {
            PhotonView.RPC("RPCLoadScene", RpcTarget.MasterClient);
            PhotonView.RPC("RPCLoadGame", RpcTarget.Others);
        }

        private void ClientLoadedGame() {
            PhotonView.RPC("RPCLoadScene", RpcTarget.MasterClient);

        }

        [PunRPC]
        private void RPCLoadGame() {
            PhotonNetwork.LoadLevel(1);
        }

        [PunRPC]
        private void RPCLoadScene()
        {
            PlayersInGame++;
            if (PlayersInGame == PhotonNetwork.PlayerList.Length) {
                Debug.Log("All Player in game scene");
                PhotonView.RPC("RPCCreatePlayer", RpcTarget.All);

            }
            
        }
       
        [PunRPC]
        private void RPCCreatePlayer ()
            {
           
            for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++)
            {
                int randomNumber = Random.Range(0, Spot.SpawnSpot.Length);
                Debug.Log("Print for everyone: "+ PlayerPrefab[j].GetComponent<PhotonView>().ViewID.ToString());

                switch (j)
                {
                    case 0:
                        if (PhotonNetwork.PlayerList[j].UserId != null)
                        {
                            Debug.Log("Spawn at 0");
                            //PlayerMove.RefershInstance(ref LocalPlayer, PlayerPrefab[0], Spot.SpawnSpot[randomNumber]);
                            PlayerMove.RefershInstance(ref LocalPlayer, PlayerPrefab[0], Spot.SpawnSpot[randomNumber], PhotonNetwork.PlayerList[j]);

                            //GameManager.RegisterPlayer(PhotonNetwork.PlayerList[j], PlayerPrefab[0]);
                        }
                            
                        break;
                    case 1:
                        if (PhotonNetwork.PlayerList[j].UserId != null ) {
                            Debug.Log("Spawn at 1");
                            //PlayerMove.RefershInstance(ref LocalPlayer, PlayerPrefab[1], Spot.SpawnSpot[randomNumber]);
                            PlayerMove.RefershInstance(ref LocalPlayer, PlayerPrefab[1], Spot.SpawnSpot[randomNumber], PhotonNetwork.PlayerList[j]);
                            //GameManager.RegisterPlayer(PhotonNetwork.PlayerList[1], PlayerPrefab[1]);
                        }
                        break;
                    case 2:
                        if (PhotonNetwork.PlayerList[j].UserId != null)
                        {
                            Debug.Log("Spawn at 2");
                            //PlayerMove.RefershInstance(ref LocalPlayer, PlayerPrefab[2], Spot.SpawnSpot[randomNumber]);
                            PlayerMove.RefershInstance(ref LocalPlayer, PlayerPrefab[2], Spot.SpawnSpot[randomNumber], PhotonNetwork.PlayerList[j]);
                            //GameManager.RegisterPlayer(PhotonNetwork.PlayerList[2], PlayerPrefab[2],PlayerPrefab[0]);
                        }
                        break;
                    case 3:
                        if (PhotonNetwork.PlayerList[j].UserId != null )
                        {
                            Debug.Log("Spawn at 3");
                            //PlayerMove.RefershInstance(ref LocalPlayer, PlayerPrefab[3], Spot.SpawnSpot[randomNumber]);
                           PlayerMove.RefershInstance(ref LocalPlayer, PlayerPrefab[3], Spot.SpawnSpot[randomNumber], PhotonNetwork.PlayerList[j]);
                           //GameManager.RegisterPlayer(PhotonNetwork.PlayerList[3], PlayerPrefab[3]);
                        }
                        break;
                }

            }

        }           
        
    }
  
}
