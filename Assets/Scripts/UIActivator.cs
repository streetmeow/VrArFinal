using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIActivator : MonoBehaviour
{
    [SerializeField] private GameObject soundUi;
    [SerializeField] private InputActionReference reference;

    
    // private LineRenderer lineRenderer;

    private void Awake()
    {
        reference.action.Enable();
        reference.action.performed += ToggleUi;
    }

    private void Start()
    {
        // lineRenderer = GetComponent<LineRenderer>();
    }

    public void ToggleUi(InputAction.CallbackContext context)
    {
        soundUi.SetActive(!soundUi.activeSelf);
        // lineRenderer.enabled = xrRayInteractor.enabled;
    }
}
