using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

//This script control all the nevigation of the lanunch scene
namespace Monopoly
{
    public class NavigationMenu : MonoBehaviour
    {
        public Button playAIbtn, playPlayerbtn, newGamebtn, loadGamebtn, backBtn,
            randomRoomButton, createRoomButton, exitButton,offline;
        public Scene currentScene;
        public GameObject MainPanel, AIPanel, MultiplayerPanel, RandomRoomPanel, CreateRoomPanel;
        public Canvas NetworkCanvas, MainCanvas;


        // Start is called before the first frame update
        void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                playPlayerbtn.gameObject.SetActive(false);
                offline.gameObject.SetActive(true);
            }
            NetworkCanvas.gameObject.SetActive(false);
            AIPanel.gameObject.SetActive(false);
            MultiplayerPanel.gameObject.SetActive(false);
            //ConnectingPanel.gameObject.SetActive(false);
            //currentScene= SceneManager.GetActiveScene();


        }



        public void loadScene(int SceneIndex)
        {
            SceneManager.LoadScene(SceneIndex);
        }
        public void ToMainStartUpPanel()
        {
            MainPanel.gameObject.SetActive(true);
            AIPanel.gameObject.SetActive(false);
            MultiplayerPanel.gameObject.SetActive(false);
        }
        public void ToAIPanel()
        {
            MainPanel.gameObject.SetActive(false);
            AIPanel.gameObject.SetActive(true);
            MultiplayerPanel.gameObject.SetActive(false);
        }


        public void BackButton()
        {
            if (AIPanel.gameObject == true)
            {
                MainPanel.gameObject.SetActive(true);
                AIPanel.gameObject.SetActive(false);
                MultiplayerPanel.gameObject.SetActive(false);
            }
            else if (MultiplayerPanel.gameObject == true)
            {
                MainPanel.gameObject.SetActive(true);
                AIPanel.gameObject.SetActive(false);
                MultiplayerPanel.gameObject.SetActive(false);
            }

        }
        public void ToMultiplayerPanel()
        {
            MainPanel.gameObject.SetActive(false);
            AIPanel.gameObject.SetActive(false);
            MultiplayerPanel.gameObject.SetActive(true);
        }
        public void ToRandomRoomPanel()
        {
            MainCanvas.gameObject.SetActive(false);
            NetworkCanvas.gameObject.SetActive(true);
            RandomRoomPanel.gameObject.SetActive(true);
            CreateRoomPanel.gameObject.SetActive(false);
        }
        public void ToCreateRoomPanel()
        {
            MainCanvas.gameObject.SetActive(false);
            NetworkCanvas.gameObject.SetActive(true);
            CreateRoomPanel.gameObject.SetActive(true);
            RandomRoomPanel.gameObject.SetActive(false);
        }



        public void ExitButton()
        {
            //TODO: Make it more efficient for user so that the user aware for leaving the game
            Application.Quit();
        }


    }
}
