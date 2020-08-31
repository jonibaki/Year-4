using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Monopoly
{
    public class RoomManagement : MonoBehaviourPunCallbacks
    {
        public InputField UserInput;
        public Button CreateButton;
        public bool IsInputValid;
        public int roomNameLength;



        [SerializeField]
        private Text _roomName;
        private Text RoomName => _roomName;


        void Start()
        {

            IsInputValid = false;
        }

        // Update is called once per frame
        void Update()
        {
            //check for text field empty, it makes the "CreateButton" interactable 
            //when the text field is populated with 6 characters

            if (IsInputValid)
            {
                CreateButton.interactable = true;

            }
            else
            {
                CreateButton.interactable = false;
                roomNameLength = UserInput.text.Length;
                if (UserInput.text.Length >= 6)
                {
                    IsInputValid = true;
                }

            }
        }

        //Pressing Create Button will fire this button and create an 
        //instantiate a room name in the Scroll view panel
        public void OnClickCreateRoom()
        {
            if (IsInputValid)
            {
                RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true,  MaxPlayers = 8 };
                if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
                {
                    UserInput.text = default;
                    IsInputValid = false;
              

                    Debug.Log("Created Room");
                }
                else
                {
                    Debug.Log("Created  room failed");
                }

            }
      

        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            print("It failed " + message);
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Room created successfully");

        }
    }
}
