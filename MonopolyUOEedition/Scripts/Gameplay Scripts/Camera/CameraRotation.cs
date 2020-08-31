using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
 
    public float speed;
    //public Transform PlayerTransform;
    //private Vector3 _cameraOffet;

    [Range(0.01f,1.0f)]
    public float SmoothFactor = 0.5f;

    private void Start()
    {
       // _cameraOffet = transform.position - PlayerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
    public void FollowPlayer() {
        //Vector3 newPos = PlayerTransform.position + _cameraOffet;
        //transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    }

}
