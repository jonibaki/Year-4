using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PropertyOwnership : MonoBehaviour
{
    public static PropertyOwnership Instance;
    public static Dictionary<int, Dictionary<string, int>> PropertyMap = new Dictionary<int, Dictionary<string,int>>();
    public bool newEntryOfOwner;

    void Start()
    {
        
        Instance = this;
    }

    void Update()
    {
        //test calls
        if (newEntryOfOwner) {
            foreach (KeyValuePair<int , Dictionary<string, int>> owner in PropertyMap) {

                Debug.Log(owner.Key.ToString());
            }
            newEntryOfOwner = false;
        }
    }
    public void AddReward(int rewardValue) {
        switch (rewardValue) {
            case 0:
                Debug.Log(200);
                break;
            case 5:
                Debug.Log(50);
                break;
            case 14:
                Debug.Log(150);
                break;
            case 15:
                Debug.Log(100);
                break;
            default:
                Debug.Log("There is nothing to add");
                break;
        }
       
    }

    void DeductReward(int deductValue) {

    }

    void OrderByTheCommunity() {

    }

    void PersonalWildCard() {

    }
}
