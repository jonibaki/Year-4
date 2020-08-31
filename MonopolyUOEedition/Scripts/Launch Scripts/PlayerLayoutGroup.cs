using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Monopoly {

    public class PlayerLayoutGroup : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        [SerializeField]
        private GameObject _playerList;
        private GameObject PlayerList { get { return _playerList; } }

        private List<PlayerProfile> _playerProfileList = new List<PlayerProfile>();
        private List<PlayerProfile> PlayerProfileList { get { return _playerProfileList; } }


        public override void OnMasterClientSwitched(Player player) {
            Debug.Log("Master left");
        }

        public override void OnJoinedRoom()
        {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);

            }
            NetworkPanelController.Instance.CustomePanel.transform.SetAsLastSibling();
            Player[] photonPlayer = PhotonNetwork.PlayerList;
                for (int i = 0; i < photonPlayer.Length; i++) {
                    PlayerJoinedRoom(photonPlayer[i]);
                }
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + "| Player in room: " +
        PhotonNetwork.CurrentRoom.PlayerCount + " Room Name: " +
        PhotonNetwork.CurrentRoom.Name + " Player Name: " + PhotonNetwork.NickName);
        }

        public override void OnPlayerLeftRoom(Player player)
        {
            PlayerLeaveRoom(player);
            Debug.Log("OnPlayerLeftRoom called");
        }

        public override void OnPlayerEnteredRoom(Player player) {
            PlayerJoinedRoom(player);
        }

        private void PlayerJoinedRoom(Player player)
        {
            
            if (player == null)
            {
                return;

            }
            PlayerLeaveRoom(player);
            GameObject playerListObj = Instantiate(PlayerList);
            playerListObj.transform.SetParent(transform, false);

            PlayerProfile playerProf = playerListObj.GetComponent<PlayerProfile>();
            playerProf.ApplyPhotonPlayer(player);

            PlayerProfileList.Add(playerProf);
            Debug.Log("Player prefab added to the list");
        }

        private void PlayerLeaveRoom(Player player)
        {
            int index = PlayerProfileList.FindIndex(x => x.PhotonPlayer == player);
            if (index != -1)
            {
                Destroy(PlayerProfileList[index].gameObject);
                PlayerProfileList.RemoveAt(index);
                Debug.Log("Player left room");
            }
            else {
                Debug.Log("Player in room");
            }
        }

    
    }



}
