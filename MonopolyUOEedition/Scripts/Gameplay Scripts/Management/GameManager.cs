using Monopoly;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviourPun
{
    public Button rollButton, makeOfferButton;
    public Text randomNumberText, timerText;
    public GameObject Dialoge, PropertyCardPanel, CCDialoug, TrainStationCardPanel, UtilityPanel;
    private int diceValue;
    private static Dictionary<Player, PlayerMove> PlayerInGameMap = new Dictionary<Player, PlayerMove>();

    void Awake()
    {
        CCDialoug.SetActive(false);
        PropertyCardPanel.SetActive(false);
        Dialoge.SetActive(false);
        TrainStationCardPanel.SetActive(false);
        UtilityPanel.SetActive(false);

        if (!PhotonNetwork.IsConnected) {
            SceneManager.LoadScene(0);
            return;
        }
    
    }
    void Start()
    {
        Debug.Log("The size of the dic: "+PlayerInGameMap.Count.ToString());
        foreach (KeyValuePair<Player, PlayerMove> p in PlayerInGameMap)
        {
            Debug.Log("Printing: "+p.Key.NickName.ToString() + "---" + p.Value.GetComponent<PlayerProfile>().Money +
                "---" + p.Value.GetComponent<PhotonView>().ViewID.ToString());
        }
        
        
    }
 
    public static void RegisterPlayer(Player playerId, PlayerMove _player)
    {
        Player _playerId = playerId;
        PlayerInGameMap.Add(_playerId, _player);

    }

    public static PhotonView GetPlayerID(Player player) {

       
        if (!PlayerInGameMap.ContainsKey(player)){
            Debug.Log("hey! this played id is no good! " + player);
            return null;
        }
        return PlayerInGameMap[player].GetComponent<PhotonView>();
    }
    /*
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();
        foreach (KeyValuePair<Player, PlayerMove> p in PlayerInGameMap) {
            //GUILayout.Label("Player: "+"------"+ p.Key.NickName.ToString() );
            GUILayout.Label(p.Key.NickName.ToString() +"---"+ p.Value.GetComponent<PlayerProfile>().Money + 
                "---"+ p.Value.GetComponent<PhotonView>().ViewID.ToString());

        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    */
    #region PunRPC METHODS
    //Show random numbers as dice value in the UI
    [PunRPC]
    public void RPC_DisplayDice(int random)
    {
        FindObjectOfType<AudioManager>().PlayAudio("Roll Dice");
        diceValue = Random.Range(2, 13);
        randomNumberText.enabled = true;
        randomNumberText.text = random.ToString();
    }

    [PunRPC]
    public void RPC_DecrementDice(int random)
    {        
        randomNumberText.text = random.ToString();
    }

    //Show Offer in the panel
    [PunRPC]
    void RPC_ShowOffer() {
        Dialoge.SetActive(true);
    }

    //Show all the property card in the panel
    [PunRPC]
    void RPC_ShowPropertyCardPanel(string title, int id, int price, int mortgage, int rent, int rent1, int rent2, int rent3, int rent4, int rent5,
        int houseCost, int hotelCost)
    {

        PropertyCardPanel.SetActive(true);
        GameObject.Find("PropertyName").GetComponent<Text>().text =title;
        GameObject.Find("RentText").GetComponent<Text>().text     = "£" + rent.ToString();
        GameObject.Find("RentText (1)").GetComponent<Text>().text = "£" + rent1.ToString();
        GameObject.Find("RentText (2)").GetComponent<Text>().text = "£" + rent2.ToString();
        GameObject.Find("RentText (3)").GetComponent<Text>().text = "£" + rent3.ToString();
        GameObject.Find("RentText (4)").GetComponent<Text>().text = "£" + rent4.ToString();
        GameObject.Find("RentText (5)").GetComponent<Text>().text = "£" + rent5.ToString();
        GameObject.Find("RentText (6)").GetComponent<Text>().text = "£" + mortgage.ToString();
        GameObject.Find("RentText (7)").GetComponent<Text>().text = "£" + houseCost.ToString();
        GameObject.Find("RentText (8)").GetComponent<Text>().text = "£" + hotelCost.ToString();



    }


    //Show Rail station card in the panel
    [PunRPC]
    void RPC_ShowPropertyCardPanel(string title, int id, int price, int mortgage, int rent1, int rent2, int rent3, int rent4) {

        TrainStationCardPanel.SetActive(true);
     
        GameObject.Find("PropertyName").GetComponent<Text>().text = title;
        GameObject.Find("RentText (1)").GetComponent<Text>().text = "£" + rent1.ToString();
        GameObject.Find("RentText (2)").GetComponent<Text>().text = "£" + rent2.ToString();
        GameObject.Find("RentText (3)").GetComponent<Text>().text = "£" + rent3.ToString();
        GameObject.Find("RentText (4)").GetComponent<Text>().text = "£" + rent4.ToString();
        GameObject.Find("RentText (6)").GetComponent<Text>().text = "£" + mortgage.ToString();
    }

    //Show Utilities card in the panel
    [PunRPC]
    void RPC_ShowPropertyCardPanel(string title, int id, int price, int mortgage)
    {

        UtilityPanel.SetActive(true);

        GameObject.Find("PropertyName").GetComponent<Text>().text = title;
        GameObject.Find("RentText (6)").GetComponent<Text>().text = "£" + mortgage.ToString();
    }

    //Show Community and Chance card in its panel
    [PunRPC]
    void RPC_ShowCCDialougPanel(string title, string contents) {
    
        CCDialoug.SetActive(true);

        //CCDialoug.transform.GetChild(0).gameObject.SetActive(false);
       
        GameObject.Find("CardTitleText").GetComponent<Text>().text = title;
        GameObject.Find("ContentsText").GetComponent<Text>().text = contents;

        //Take necesaary step here or before calling this RPC
        PropertyOwnership.Instance.AddReward(5);
    }

    //Close the open panel 
    [PunRPC]
    void RPC_ClosePanel() {
        if (PropertyCardPanel.activeInHierarchy)
        {
            PropertyCardPanel.SetActive(false);
        }
        else if (CCDialoug.activeInHierarchy)
        {
            CCDialoug.transform.GetChild(0).gameObject.SetActive(false);
            CCDialoug.SetActive(false);
        }
        else if (Dialoge.activeInHierarchy)
        {
            Dialoge.SetActive(false);
        }
        else if (TrainStationCardPanel.activeInHierarchy)
        {
            TrainStationCardPanel.SetActive(false);
        }
        else if (UtilityPanel.activeInHierarchy) {
            UtilityPanel.SetActive(false);
        }

    }

    [PunRPC]
    void RPC_BuyProperty(int currentposition, int _owner) {
 
        FindObjectOfType<AudioManager>().PlayAudio("Buy");
        
        BlockManager.Instance.childBlockList[currentposition].GetComponent<PropertyDetails>().PropertyOwner =_owner;
        int property_ID = BlockManager.Instance.childBlockList[currentposition].GetComponent<PropertyDetails>().PropertyID;

        Dictionary<string, int> inner_property = new Dictionary<string, int>();

        PropertyOwnership.PropertyMap.Add(property_ID, inner_property);
        (PropertyOwnership.PropertyMap[property_ID]).Add("COLOUR1", _owner);

        PropertyOwnership.Instance.newEntryOfOwner=true;
        

        //check if all the propery has bought buy the player 
        //if all bought then buy house and instantiate house
        //deduct money from player account

        this.GetComponent<PhotonView>().RPC("RPC_ClosePanel", RpcTarget.All);
    }

    [PunRPC]
    void RPC_BuildHouse() {
        //test condition for instanttiate buildings 
        // get the location of the board and look for the building spawn point before instantiate 


        GameObject house = PhotonNetwork.Instantiate("Monopoly House", Spot.BuildingSpots[0].transform.position, Quaternion.identity);

    }
    #endregion
}
