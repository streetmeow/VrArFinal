using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamRatation : MonoBehaviour
{
    public GameObject cam;
    // https://discussions.unity.com/t/head-rotation-not-working-with-xr-device-simulator/842443
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            cam.transform.eulerAngles.y,
            gameObject.transform.eulerAngles.z
        );;
    }
}
