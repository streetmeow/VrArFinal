using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    public static HandPosition Instance;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public Transform GetLeftHand()
    {
        return leftHand;
    }

    public Transform GetRightHand()
    {
        return rightHand;
    }
}
