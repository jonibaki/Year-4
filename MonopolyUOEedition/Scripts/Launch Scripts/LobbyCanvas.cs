using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monopoly
{
    public class LobbyCanvas : MonoBehaviour
    {
        [SerializeField]
        private RoomLayoutGroup _roomLayoutGroup;
        public RoomLayoutGroup RoomLayoutGroup => _roomLayoutGroup;

        public void OnClickJoinRoom(string roomName)
        {
            if (PhotonNetwork.JoinRoom(roomName))
            {
                Debug.Log("Join Room Succed");
                NetworkPanelController.Instance.CustomePanel.transform.SetAsLastSibling();
            }
            else {
                Debug.Log("Join Room Failed");
            }
        }
    }
}
