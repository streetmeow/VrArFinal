using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BeamActivator : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    [SerializeField] private InputActionReference reference;

    private XRRayInteractor xrRayInteractor;
    // private LineRenderer lineRenderer;

    private void Awake()
    {
        reference.action.Enable();
        reference.action.performed += ToggleBeam;
    }

    private void Start()
    {
        xrRayInteractor = controller.GetComponent<XRRayInteractor>();
        // lineRenderer = GetComponent<LineRenderer>();
    }

    public void ToggleBeam(InputAction.CallbackContext context)
    {
        xrRayInteractor.enabled = !xrRayInteractor.enabled;
        // lineRenderer.enabled = xrRayInteractor.enabled;
    }
}
