using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    public GameObject []alienBody;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        alienBody = GameObject.FindGameObjectsWithTag("Alien");        
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (GameObject o in alienBody) {
            o.transform.LookAt(player.transform.position);
        }
        
    }

}
