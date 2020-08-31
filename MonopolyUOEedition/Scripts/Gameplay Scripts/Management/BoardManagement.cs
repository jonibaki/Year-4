using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monopoly
{
    public class BoardManagement : MonoBehaviourPun
    {
        public static BoardManagement Instance;
        #region Public Varible
        public Camera followCamera;
        public Button rollButton, makeOfferButton, endRollButton, buildButton;
        public Text randomNumberText, timeText, FPSText, pingText;
        public float hr, min, sec;
        public bool buttonPressed;
        public int Index_Card;
        public int turnNumber;
        public int Current_Pos;
        public GameObject PropertyHud, SideHud;
        #endregion

        #region Private Variable
        [SerializeField]
        private int diceValue;
        private PhotonView pv;
        private PhotonView playerPV;
        private bool HUDEnabled;
        GameManager gameManager;
        #endregion


        void Start()
        {
            Instance = this;
            sec = 0f;
            randomNumberText.enabled = false;


            //followCamera = Camera.main.GetComponent<CameraRotation>()  //fix later
            gameManager = FindObjectOfType<GameManager>();
            pv = GameObject.FindObjectOfType<GameManager>().GetComponent<PhotonView>();
            StartCoroutine(CalculateGamePlay());

        }

        void Update()
        {

            sec += Time.deltaTime;
            if (sec > 59)
            {
                min += 1;
                sec = 0;
            }
            else if (min > 59) {
                hr += 1;
                min = 0;
            }
            timeText.text = "Time: " + hr.ToString() + ":" + min.ToString() + ":" + Mathf.RoundToInt(sec).ToString();

            //enable and disable side and bottom panel from the window
            if (Input.GetKeyDown(KeyCode.I)) {
                HUDEnabled = !HUDEnabled;
            }
            if (HUDEnabled == true)
            {
                PropertyHud.SetActive(true);
                SideHud.SetActive(true);
            }
            else {
                SideHud.SetActive(false);
                PropertyHud.SetActive(false);
            }

        }

        public void ShowOfferPanel()
        {
            //TODO: Show all the player in the panel
            //Show all the available property 
            //Trade with money and porperty option with specific player

            if (pv == null)
            {
                Debug.Log("NULL PV");

            }
            pv.RPC("RPC_ShowOffer", RpcTarget.All);

        }

        public void ShowPropertyCardPanel(string propName, int id, int propPrice, int mortgage, int rent, int rent1,
            int rent2, int rent3, int rent4, int rent5, int houseCost, int hotelCost)
        {
            CheckUpateForPlayerID();
            if (id == 10 || id == 11 || id == 12 || id == 13 || id == 14 ||
                id == 15 || id == 16 || id == 17)
            {
                if (playerPV.IsMine) {
                    Debug.Log(propName + " " + id + " " + propPrice);
                    pv.RPC("RPC_ShowPropertyCardPanel", RpcTarget.All, propName, id, propPrice, mortgage,
                        rent, rent1, rent2, rent3, rent4, rent5, houseCost, hotelCost);
                    Current_Pos = playerPV.GetComponent<PlayerMove>().routePos;

                }



            }
            else if (id == ObjectReferences.STATION_ID)
            {
                if (playerPV.IsMine) {
                    pv.RPC("RPC_ShowPropertyCardPanel", RpcTarget.All, propName, id, propPrice, mortgage,
                    rent, rent1, rent2, rent3);
                    Current_Pos = playerPV.GetComponent<PlayerMove>().routePos;


                }

            }
            else if (id == ObjectReferences.UTILITIES_ID) {

                if (playerPV.IsMine) {
                    pv.RPC("RPC_ShowPropertyCardPanel", RpcTarget.All, propName, id, propPrice, mortgage);
                    Current_Pos = playerPV.GetComponent<PlayerMove>().routePos;

                }

            }

        }

        public void ShowCCDialougPanel(int random) {

            
            gameManager.CCDialoug.transform.GetChild(0).gameObject.SetActive(true);

            int randomNumber = Random.Range(0, 17);
            Index_Card = randomNumber;
            switch (random) {
                case 4:
                    pv.RPC("RPC_ShowCCDialougPanel", RpcTarget.All, "Community Card", BlockManager.Instance.CommunityList[randomNumber]);
                    break;
                case 5:
                    pv.RPC("RPC_ShowCCDialougPanel", RpcTarget.All, "Chance Card", BlockManager.Instance.ChanceList[randomNumber]);
                    break;
                default:
                    Debug.Log("Nothing found");
                    break;
            }

        }

        /// <summary>
        /// Future update: add features like dice, texture of in the button
        /// 1.allow user to visualise the dice roll in the board or in the UI 
        /// 2.add double numbers to detect "Roll Again" feauture
        /// </summary>
        public void GenerateRandom()
        {
            //test line
            diceValue = 2;
            //uncomment for real gaming
            //diceValue = Random.Range(2, 13);
            pv.RPC("RPC_DisplayDice", RpcTarget.All, diceValue);
            buttonPressed = true;
        }

        public int GetDiceNumber()
        {
            return diceValue;
        }

        public void OnClickLeaveGame()
        {
            //prompt player yes or no panel and warning of losing all the game stats
            //network handle the action correctly
            //activate the button in the main scene
            Destroy(DDOL.instance.gameObject);

            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(0);
        }

        public void OnClickDisable() {

            pv.RPC("RPC_ClosePanel", RpcTarget.All);

        }

        public void OnClickBuyProperty() {

            CheckUpateForPlayerID();

            if (playerPV.IsMine) {
                //check for enough money to buy the property
                Debug.Log(playerPV.GetComponent<PlayerProfile>().Money.ToString());

                Debug.Log(BlockManager.Instance.childBlockList[Current_Pos].GetComponent<PropertyDetails>().PropertyPrice);

                //BlockManager.Instance.childBlockList[Current_pos].GetComponent<PropertyDetails>().PropertyOwner = playerPV.Owner.NickName;
                playerPV.GetComponent<PlayerProfile>().Money -= BlockManager.Instance.childBlockList[Current_Pos].GetComponent<PropertyDetails>().PropertyPrice;

                Debug.Log(playerPV.GetComponent<PlayerProfile>().Money.ToString());

                pv.RPC("RPC_BuyProperty", RpcTarget.All, Current_Pos, playerPV.ViewID);

            }



        }

        public void OnClickBuildHouse() {
            pv.RPC("RPC_BuildHouse", RpcTarget.All);
        }

        //FIX IT: Shoudl be able to skip the turn number when a player leave room 
        //So that an empty turn do not wait for non existing player ID in the future dev
        public void OnClickEndRoll() {
            //Pass to next player in the queue;
            //deactive the button
            turnNumber++;
            if (turnNumber > PhotonNetwork.PlayerList.Length)
            {
                turnNumber = 1;
                endRollButton.gameObject.SetActive(false);
            }
            else {

                endRollButton.gameObject.SetActive(false);
            }

        }

        //Check for player's turns and activate and deactivate different components of the game
        public bool Turn() {

            int My_Turn = 0;
            foreach (Player players in PhotonNetwork.PlayerList)
            {
                if (players.IsLocal)
                {
                    playerPV = GameManager.GetPlayerID(players);
                    My_Turn = players.ActorNumber;
                    break;
                }
            }


            // Activating all the game components for each player accordingly
            if (turnNumber == My_Turn && playerPV.IsMine)
            {
                playerPV.GetComponentInChildren<Camera>().enabled = true;
                rollButton.gameObject.SetActive(true);
                makeOfferButton.gameObject.SetActive(true);
                return true;

            }
            else
            {
                playerPV.GetComponentInChildren<Camera>().enabled = false;
                //followCamera.GetComponent<CameraRotation>().FollowPlaye
                rollButton.gameObject.SetActive(false);
                makeOfferButton.gameObject.SetActive(false);
                return false;
            }


        }

        IEnumerator CalculateGamePlay() {

            while (true) {
                float fps = 1 / Time.deltaTime;
                int ping = PhotonNetwork.GetPing();
                FPSText.text = "FPS:    " + fps.ToString();
                pingText.text = "Ping:   " + ping.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        //Validate the photonView Id for local player and assign into a 
        //global variable which can be access any where from this scripts: playerPV
        public void CheckUpateForPlayerID(){
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                if (p.IsLocal)
                {
                    playerPV = GameManager.GetPlayerID(p);
                    Current_Pos = playerPV.GetComponent<PlayerMove>().routePos;
                }
            }
        }
    }
}
