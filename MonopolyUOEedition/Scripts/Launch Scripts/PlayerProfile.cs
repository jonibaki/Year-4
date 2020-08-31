using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Monopoly
{
    public class PlayerProfile : MonoBehaviourPun
    {
        //[SerializeField] private PlayerMove playerMove;
        public Player PhotonPlayer { get; private set; }
        [SerializeField]
        private Text _playerName;
        public Text PlayerName=> _playerName;
        

        public int Money;
    
        private int PlayerID = 0;

        public void ApplyPhotonPlayer(Player photonPlayer)
        {
            Debug.Log("ApplyPhotonPlayer Called");
            PhotonPlayer = photonPlayer;
            PlayerName.text = photonPlayer.NickName;
            //GameManager.RegisterPlayer(photonPlayer, playerMove);
        }
        
    }
}