using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Monopoly
{
    public class NetworkPanelController : MonoBehaviour
    {
        public Button homeButton, backButton, createButton, randomButton;
        public Canvas NetworkCanvas, MainCanvas;
        public GameObject CustomePanel, RandomRoomPanel, MultiplayerPanel, StartUpPanel;

        public static NetworkPanelController Instance;

        [SerializeField]
        private LobbyCanvas _lobbyCanvas;
        
        public LobbyCanvas LobbyCanvas {
            get { return _lobbyCanvas;
            }
        }

        [SerializeField]
        private CustomePanel _customePanel;

        public CustomePanel CustomeRoomPanel
        {
            get
            {
                return _customePanel;
            }
        }

        private void Awake()
        {
            Instance = this;

        }

        public void ToNetworkPanel()
        {
            if (CustomePanel.gameObject == true)
            {
                NetworkCanvas.gameObject.SetActive(false);
                CustomePanel.gameObject.SetActive(false);
                MainCanvas.gameObject.SetActive(true);
                MultiplayerPanel.gameObject.SetActive(true);

            }
            else if (RandomRoomPanel.gameObject == true)
            {
                NetworkCanvas.gameObject.SetActive(false);
                RandomRoomPanel.gameObject.SetActive(false);
                MainCanvas.gameObject.SetActive(true);
                MultiplayerPanel.gameObject.SetActive(true);
            }
        }

        public void ToMainPanel()
        {
            NetworkCanvas.gameObject.SetActive(false);
            MultiplayerPanel.gameObject.SetActive(false);
            MainCanvas.gameObject.SetActive(true);
            StartUpPanel.gameObject.SetActive(true);
        }

    }
}