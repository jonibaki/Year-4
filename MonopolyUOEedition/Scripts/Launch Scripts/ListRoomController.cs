using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

//This scripts control implement of all the new room name 
//and it's properties like room name
namespace Monopoly
{
    public class ListRoomController : MonoBehaviour
    {
        [SerializeField]
        private Text _roomNameText;
        public Text RoomNameText => _roomNameText;

        public string RoomName { get; private set; }

        public bool Updated { get; set; }

        private void Start()
        {
            GameObject lobbyCanvasObj = NetworkPanelController.Instance.LobbyCanvas.gameObject;
            if (lobbyCanvasObj == null) {
                return;
            }
            LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

            Button button = GetComponent<Button>();

            if (lobbyCanvas == null)
            {
                Debug.Log("Empty button");

            }

            if (button == null) {
                Debug.Log("Empty button");

            }
         
            button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
            Debug.Log(RoomNameText.text);
        }

        private void OnDestroy()
        {
            Button button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
        }

        public void SetRoomNameText(string text)
        {
            RoomName = text;
            RoomNameText.text = RoomName;

        }

        public void SetRoomInfo(RoomInfo roomInfo)
        {
           
            RoomNameText.text = roomInfo.Name;

        }
    }
}