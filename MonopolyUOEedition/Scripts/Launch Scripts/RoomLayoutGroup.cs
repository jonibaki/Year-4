using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monopoly
{
    public class RoomLayoutGroup : MonoBehaviourPunCallbacks
    {
        
        [SerializeField]
        private GameObject _roomListingPrefab;
        public GameObject RoomListingPrefab => _roomListingPrefab;
       
        private List<ListRoomController> _roomListingButtons  = new List<ListRoomController>();
        private List<ListRoomController> RoomListingButtons { get { return _roomListingButtons; } }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            /*
            foreach (RoomInfo info in roomList){
                if (info.RemovedFromList)
                {
                    int index = RoomListingButtons.FindIndex(x => x.RoomName == info.Name);
                    if (index != -1) {
                        Destroy(RoomListingButtons[index].gameObject);
                        RoomListingButtons.RemoveAt(index);
                    }
                }
                else {
                    GameObject roomListingObj = Instantiate(RoomListingPrefab);
                    roomListingObj.transform.SetParent(transform, false);
                    
                    ListRoomController roomListing = roomListingObj.GetComponent<ListRoomController>();
                    roomListing.SetRoomNameText(info.Name);
                    RoomListingButtons.Add(roomListing);

                }
            }
            */
            
            foreach (RoomInfo rooms in roomList) {
                RoomReceived(rooms);
            }
           RemoveOldRooms();
       
        }
        
        private void RoomReceived(RoomInfo room)
        {
            
            int index = RoomListingButtons.FindIndex(x => x.RoomName == room.Name);
            if (index == -1) {
                if (room.IsVisible && room.PlayerCount < room.MaxPlayers) {
                    GameObject roomListingObj = Instantiate(RoomListingPrefab);
                    roomListingObj.transform.SetParent(transform, false);

                    ListRoomController roomListing = roomListingObj.GetComponent<ListRoomController>();
                    RoomListingButtons.Add(roomListing);
                    Debug.Log("Room Added in the list");
                    index = (RoomListingButtons.Count - 1);
                }
            }
            if (index != -1) {

                ListRoomController roomListing = RoomListingButtons[index];
                roomListing.SetRoomNameText(room.Name);
                roomListing.Updated = true;

            }
        }

        private void RemoveOldRooms() {
            List<ListRoomController> removeRooms = new List<ListRoomController>();
            foreach (ListRoomController roomList in RoomListingButtons) {
                if (!roomList.Updated)
                {
                    removeRooms.Add(roomList);
                }
                else {
                    roomList.Updated = false;
                }
            }
            foreach (ListRoomController roomList in removeRooms) {
                GameObject roomListObj = roomList.gameObject;
                Debug.Log("Room deleted from the list");
                RoomListingButtons.Remove(roomList);
                Destroy(roomListObj);

            }
        }

        public void OnClickLeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

    }
 
}