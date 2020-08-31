using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectives : MonoBehaviour
{
    public GameObject [] objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("Objectives");
    }


    void Update()
    {
        foreach (GameObject g in objects) {
            
            g.transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
        

    }
}
