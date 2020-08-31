
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// The file contain two coroutine; one for pull raw data from the server and second one to initialise some varibles from the list
/// There are three list that holds cards property information which can be retrived from another file to display
/// </summary>
namespace Monopoly
{

    public class BlockManager : MonoBehaviour
    {
        public static BlockManager Instance;
        GameObject[] property;
        Transform[] ChildObjects;
        public List<Transform> childBlockList = new List<Transform>();
       //public List<PropertyDetails> PropertyList = new List<PropertyDetails>(); find a better way to call this without a list of class
        public List<string> CommunityList = new List<string>();
        public List<string> ChanceList = new List<string>();
        public List<string> propeList = new List<string>();

        bool LoadFileInScene = false;

     
        private void Start()
        {
            Instance = this;
            StartCoroutine(FetchFiles(ObjectReferences.PATH1, ObjectReferences.PATH2, ObjectReferences.PATH3));
            property = GameObject.FindGameObjectsWithTag("blocks");

        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            FillBlocks();         
            for (int i = 0; i < childBlockList.Count; i++)
            {

                Vector3 currentPos = childBlockList[i].position;
                if (i > 0)
                {
                    Vector3 prevPos = childBlockList[i - 1].position;
                    Gizmos.DrawLine(prevPos, currentPos);

                }
            }

        }

        void FillBlocks()
        {
          
            childBlockList.Clear();
            ChildObjects = GetComponentsInChildren<Transform>();

            foreach (Transform child in ChildObjects)
            {
                if (child != this.transform)
                {
                    childBlockList.Add(child);
                }
            }
        }

        IEnumerator InitialisedProperty()
        {
            ///PropertyList.Clear();

            yield return null;

            try
            {
                
                int count = 0; //keeping track of number of index in the property list and assign into a PropertyDetail type of list
                foreach (GameObject g in property)
                {
                int id = g.GetComponent<PropertyDetails>().PropertyID;
                string[] strArray = propeList[count].Split('\t');
              
                    //implement it only for all the PROPERTY block in the board
                    if (ObjectReferences.IsProperty(id))
                    {
                       // DebugConsole.Log(propeList[count].ToString(), "normal");
                      
                        string propertyName = strArray[0].ToString();
                        int propertyPrice = int.Parse(strArray[1]);
                        int mortgage = int.Parse(strArray[2]);
                        int rent = int.Parse(strArray[3]);
                        int rent1 = int.Parse(strArray[4]);
                        int rent2 = int.Parse(strArray[5]);
                        int rent3 = int.Parse(strArray[6]);
                        int rent4 = int.Parse(strArray[7]);
                        int rent5 = int.Parse(strArray[8]);
                        int houseCost = int.Parse(strArray[9]);
                        int hotelCost = int.Parse(strArray[9]);

                        g.GetComponent<PropertyDetails>().PropertyName = propertyName;
                        g.GetComponent<PropertyDetails>().PropertyPrice = propertyPrice;
                        g.GetComponent<PropertyDetails>().mortgage = mortgage;
                        g.GetComponent<PropertyDetails>().rent = rent;
                        g.GetComponent<PropertyDetails>().rent1 = rent1;
                        g.GetComponent<PropertyDetails>().rent2 = rent2;
                        g.GetComponent<PropertyDetails>().rent3 = rent3;
                        g.GetComponent<PropertyDetails>().rent4 = rent4;
                        g.GetComponent<PropertyDetails>().rent5 = rent5;
                        g.GetComponent<PropertyDetails>().houseCost = houseCost;
                        g.GetComponent<PropertyDetails>().hotelCost = hotelCost;

                        //string tempPlayer = g.GetComponent<PropertyDetails>().PropertyOwner;
                        g.GetComponent<PropertyDetails>().PropertyOwner = 0;
                        //PropertyDetails p = new PropertyDetails(propertyName, id, propertyPrice,
                        //   mortgage, rent, rent1, rent2, rent3, rent4, rent5, houseCost, hotelCost, tempPlayer);


                        //PropertyList.Add(p);
                        count++;
                        
                        
                    }


                    // implement only for all the STATIONS
                    if (id == ObjectReferences.STATION_ID)
                    {
                        //DebugConsole.Log(propeList[count].ToString(), "normal");
                        string propertyName = strArray[0].ToString();
                        int propertyPrice = int.Parse(strArray[1]);
                        int mortgage = int.Parse(strArray[2]);
                        int rent = int.Parse(strArray[3]);
                        int rent1 = int.Parse(strArray[4]);
                        int rent2 = int.Parse(strArray[5]);
                        int rent3 = int.Parse(strArray[6]);


                        g.GetComponent<PropertyDetails>().PropertyName = propertyName;
                        g.GetComponent<PropertyDetails>().PropertyPrice = propertyPrice;
                        g.GetComponent<PropertyDetails>().mortgage = mortgage;
                        g.GetComponent<PropertyDetails>().rent = rent;
                        g.GetComponent<PropertyDetails>().rent1 = rent1;
                        g.GetComponent<PropertyDetails>().rent2 = rent2;
                        g.GetComponent<PropertyDetails>().rent3 = rent3;
                        g.GetComponent<PropertyDetails>().PropertyOwner = 0;
                        count++;
                        //Debug.Log("Initialised :" + count);
                        
                    }
                    // only for all the UTILITIES
                    if (id == ObjectReferences.UTILITIES_ID)
                    {
                        
                       // DebugConsole.Log(propeList[count].ToString(),"normal");
                        string propertyName = strArray[0].ToString();
                        int propertyPrice = int.Parse(strArray[1]);
                        int mortgage = int.Parse(strArray[2]);

                        g.GetComponent<PropertyDetails>().PropertyName = propertyName;
                        g.GetComponent<PropertyDetails>().PropertyPrice = propertyPrice;
                        g.GetComponent<PropertyDetails>().mortgage = mortgage;
                        g.GetComponent<PropertyDetails>().PropertyOwner = 0;
                        count++;
              
                    }
                   


                }// end of loop
      


            } //end of try block 
            catch (Exception e)
            {
                Debug.Log("Message Error: " + e.Message);
             
            }
            LoadFileInScene = false;
            Debug.Log("Initialised Completed");
       
        }

        //Read the file from the php server and assign it into different list
        IEnumerator FetchFiles(string path1, string path2, string path3) {
         
            UnityWebRequest www1 = UnityWebRequest.Get(path1);
            UnityWebRequest www2 = UnityWebRequest.Get(path2);
            UnityWebRequest www3 = UnityWebRequest.Get(path3);

            yield return www1.SendWebRequest();
            if (!www1.isNetworkError &&  !www1.isHttpError)
            {
                InitialisedList(propeList, www1.downloadHandler.text);
               
            }
            else {

                Debug.Log("Property text file not found");
            }
          
            yield return www2.SendWebRequest();

            if (!www2.isNetworkError && !www2.isHttpError)
            {
                InitialisedList(ChanceList, www2.downloadHandler.text);
            }
            else
            {
                Debug.Log("Chance text file not found");
               
            }
           
            yield return www3.SendWebRequest();
            if (!www3.isNetworkError && !www3.isHttpError)
            {
               InitialisedList(CommunityList, www3.downloadHandler.text); 
            }
            else
            {
               Debug.Log("Community text file not found"); 
            }
            LoadFileInScene = true;
            StartCoroutine(InitialisedProperty());
          }

        //adding line of string into the lists
        void InitialisedList(List<string> list, string fileText) {
            string line;
            StringReader reader = new StringReader(fileText);
            while ((line = reader.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
    }
}
