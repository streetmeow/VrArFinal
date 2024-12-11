using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private LineRenderer line;
    private bool isLineActivated = false;
    
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
        aButton.action.performed += ChangeArm;
        bButton.action.performed += Quit;
        // yButton.action.started += GuideTrigger;
    }

    private void RotatePlayer(InputAction.CallbackContext context)
    {
        if (!playerRigidBody.useGravity) return;
        Vector2 value = context.ReadValue<Vector2>();
        playerTransform.Rotate(Vector3.up * value.x * Time.deltaTime * 80f);
        modelTransform.Rotate(Vector3.up * value.x * Time.deltaTime * 80f);
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        // Debug.Log(_isGrounded);
        if (!playerRigidBody.useGravity) return;
        Vector2 value = context.ReadValue<Vector2>();
        float speed = 2f * Time.deltaTime;
        // playerTransform.position += new Vector3(value.x, 0, value.y) * speed;
        playerTransform.position += playerTransform.TransformDirection(new Vector3(value.x, 0, value.y)) * speed;
    }

    private void GuideTrigger(InputAction.CallbackContext context)
    {
        // guideImage.SetActive(!guideImage.activeSelf);
    }

    public void SetGrounded(bool value)
    {
        _isGrounded = value;
    }

    public void ChangeArm(InputAction.CallbackContext context)
    {
        isLineActivated = !isLineActivated;
    }

    public void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
        Debug.Log("애플리케이션 종료 요청 (에디터 모드에서는 종료되지 않습니다)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
