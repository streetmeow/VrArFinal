using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputs : MonoBehaviour
{
    public static CustomInputs instance;
    public InputActionReference leftThumb;
    public InputActionReference rightThumb;
    public InputActionReference yButton;
    public InputActionReference aButton;
    public InputActionReference bButton;
    public InputActionReference leftHandSelectButton;
    public InputActionReference xButton;
    public InputActionReference leftHandTriggerButton;
    public InputActionReference rightHandSelectButton;

    public Transform playerTransform;
    // public GameObject guideImage;
    public Rigidbody playerRigidBody;
    public Transform modelTransform;
    
    private bool _isGrounded = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        leftThumb.action.performed += RotatePlayer;
        rightThumb.action.performed += MovePlayer;
        // yButton.action.started += GuideTrigger;
    }

    private void RotatePlayer(InputAction.CallbackContext context)
    {
        if (!playerRigidBody.useGravity) return;
        Vector2 value = context.ReadValue<Vector2>();
        playerTransform.Rotate(Vector3.up * value.x * Time.deltaTime * 40f);
        modelTransform.Rotate(Vector3.up * value.x * Time.deltaTime * 40f);
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        // Debug.Log(_isGrounded);
        if (!playerRigidBody.useGravity) return;
        Vector2 value = context.ReadValue<Vector2>();
        float speed = 1.5f * Time.deltaTime;
        playerTransform.position += new Vector3(value.x, 0, value.y) * speed;
    }

    private void GuideTrigger(InputAction.CallbackContext context)
    {
        // guideImage.SetActive(!guideImage.activeSelf);
    }

    public void SetGrounded(bool value)
    {
        _isGrounded = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
