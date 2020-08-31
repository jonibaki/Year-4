using Photon.Pun;
using UnityEngine;
using Monopoly;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerInventory : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject [] invenroty;
    public Text money, playerID;
    public bool IsUpdated = false;


    void Start()
    {

        invenroty = GameObject.FindGameObjectsWithTag("SmallWindow");

        foreach (GameObject g in invenroty)
        {
            g.SetActive(false);
        }
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) {
            invenroty[i].SetActive(true);
            
        }
    }

    void Update()
    {
        if (!IsUpdated) {
            UpdateInventory();
        }
        
    }
    void UpdateInventory() {

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (PhotonView.Find(p.ActorNumber).IsMine)
            {
                Debug.Log(PhotonView.Find(p.ActorNumber).GetComponent<PhotonView>());
                PhotonView pp = PhotonNetwork.GetPhotonView(p.ActorNumber);

                Debug.Log(pp.ViewID);
                playerID.text = p.NickName;
                money.text = "£" + FindObjectOfType<PlayerProfile>().Money.ToString();
            }
            else
            {

                playerID.text = p.NickName;

                money.text = "£" + FindObjectOfType<PlayerProfile>().Money.ToString();

            }
        }
        IsUpdated = true;
    }
}
