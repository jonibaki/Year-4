using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMode : MonoBehaviour
{
    private enum CameraState
    {
        Fixed, Relative, Follow
    }

    private CameraState state = CameraState.Follow;

    private GameObject player;
    private Transform playerHead;
    private Vector3 position;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHead = GameObject.Find("Head").transform;
        position = transform.localPosition;
        offset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            state = CameraState.Fixed;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            state = CameraState.Relative;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            state = CameraState.Follow;
        
        switch (state)
        {
            case CameraState.Fixed:
                transform.position = new Vector3(10, 25, 0);
                transform.LookAt(player.transform);
                break;
            case CameraState.Relative:
                transform.position = player.transform.position - offset;
                transform.LookAt(playerHead);
                break;
            case CameraState.Follow:
                transform.localPosition = position;
                transform.LookAt(playerHead);
                break;
        }
    }
}
