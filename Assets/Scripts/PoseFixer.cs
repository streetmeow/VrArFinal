using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseFixer : MonoBehaviour
{
    private float time = 0;
    private void Update()
    {
        if (!IsSmall(transform.rotation.eulerAngles.x) || !IsSmall(transform.localEulerAngles.z))
        {
            if (time < 2f) return;
            time = 0;
            transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y + 10.3f, transform.position.z);
        }
        time += Time.deltaTime;
    }

    private bool IsSmall(float value)
    {
        if (Math.Abs(value % 360) < 45) return true;
        if (Math.Abs(value % 360 - 360) < 45) return true;
        return false;
    }
}
