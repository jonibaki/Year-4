using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public static GameObject[] SpawnSpot;
    public static GameObject[] BuildingSpots;
    // Start is called before the first frame update
    void Awake()
    {
        SpawnSpot = GameObject.FindGameObjectsWithTag("Spot");
        BuildingSpots = GameObject.FindGameObjectsWithTag("Buildings");

    }
}
