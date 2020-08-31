using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monopoly;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private PhotonView player;
    [SerializeField]
    private Text _playerName;
    public Text PlayerName => _playerName;

    void Start() {
        player = GetComponent<PhotonView>();

        //set player id
        if (!player.IsMine)
        {
            SetNameTag();
        }
        PlaySetUP();
    }

    void PlaySetUP()
    {
        //Set All the Components false as default 
        player.GetComponentInChildren<Camera>().enabled = false;
        BoardManagement.Instance.rollButton.gameObject.SetActive(false);
        BoardManagement.Instance.endRollButton.gameObject.SetActive(false);
        BoardManagement.Instance.makeOfferButton.gameObject.SetActive(false);

    }

    private void SetNameTag()
    {
        PlayerName.text = player.Owner.NickName;
    }

    public bool hasPassOnce() {
        return true;


    }


}
