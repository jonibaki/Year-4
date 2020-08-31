using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomePanel : MonoBehaviour
{
    public void OnClickReady(){
        if (!PhotonNetwork.IsMasterClient) {
            Debug.Log("You can't start the match");
            return;
        }
        else {
            PhotonNetwork.LoadLevel(1);
        }
        
    }
   
}
