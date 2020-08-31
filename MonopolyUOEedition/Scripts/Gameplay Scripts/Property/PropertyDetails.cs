using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monopoly {
    public class PropertyDetails : MonoBehaviour
    {
        
        public string PropertyName;
        public int PropertyID, PropertyPrice;
        public int rent, rent1, rent2, rent3, rent4, rent5;
        public int mortgage, houseCost, hotelCost;
        public int PropertyOwner;

        //needs use the constructor someway
        /*
        public PropertyDetails(string propName, int propID, int propPrice, string propOwner) {
            PropertyName = propName;
            PropertyID = propID;
            PropertyPrice = propPrice;
            PropertyOwner= propOwner;
        }
        public PropertyDetails(string propName, int propID, int propPrice, int mortgage, int rent, int rent1,
            int rent2, int rent3, int rent4, int rent5, int houseCost, int hotelCost, string propOwner)
        {
            PropertyName = propName;
            PropertyID = propID;
            PropertyPrice = propPrice;
            this.rent = rent;
            this.rent1 = rent1;
            this.rent2 = rent2;
            this.rent3 = rent3;
            this.rent4 = rent4;
            this.rent5 = rent5;
            this.mortgage = mortgage;
            this.houseCost = houseCost;
            this.hotelCost = hotelCost;
            PropertyOwner = propOwner;
        }
        */
    }



}
