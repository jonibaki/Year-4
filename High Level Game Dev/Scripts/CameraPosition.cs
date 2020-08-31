using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Camera playerCamera;
    //public Rigidbody player;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
      //  player = GameObject.FindObjectOfType<Rigidbody>();
        playerCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempPos = new Vector3(player.transform.position.x, player.transform.position.y+5, player.transform.position.z-12);
        playerCamera.transform.position = tempPos;


    }
}
