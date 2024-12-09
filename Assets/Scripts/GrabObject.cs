using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class GrabObject : MonoBehaviour
{
    private HandPosition handPosition;
    private InputActionReference reference;
    private InputActionReference releaseTrigger;
    private InputActionReference xTrigger;
    private bool grabbed = false;
    private Queue<Vector3> posRecord;
    private Vector3 lastPos;
    private Transform headTransform;

    public GameObject noteUI;
    private bool bookOpen = false;

    private void Start()
    {
        handPosition = HandPosition.Instance;
        reference = CustomInputs.instance.leftHandSelectButton;
        releaseTrigger = CustomInputs.instance.leftHandTriggerButton;
        xTrigger = CustomInputs.instance.xButton;
        reference.action.Enable();
        reference.action.performed += GrabObjectVoid;
        releaseTrigger.action.performed += ReleaseObject;
        xTrigger.action.performed += DropObject;
        posRecord = new Queue<Vector3>();
        headTransform = CustomInputs.instance.playerTransform;

    }

    private void Update()
    {
        if (grabbed)
        {
            
            if (!bookOpen && gameObject.CompareTag("Book"))
            {
                OpenBook();
            }
            if (bookOpen && noteUI != null)
            {
                noteUI.transform.position = headTransform.position + headTransform.forward * 1.5f + new Vector3(0, 0.3f, 0);
                noteUI.transform.rotation = Quaternion.LookRotation(headTransform.position - noteUI.transform.position) * Quaternion.Euler(-10f, 0f, 0f);
                noteUI.transform.Rotate(0f, 180f, 0f);
            }
            gameObject.transform.position = handPosition.GetLeftHand().position + new Vector3(0.1f, 0.1f, 0.1f);
            if (posRecord.Count > 15) posRecord.Dequeue();
            posRecord.Enqueue(gameObject.transform.position);

        }
    }

    private void GrabObjectVoid(InputAction.CallbackContext context)
    {
        Debug.Log("VAR");
        if ((Vector3.Distance(handPosition.GetLeftHand().position, gameObject.transform.position) < 1f
                || Vector3.Distance(new Vector3(handPosition.GetLeftHand().position.x, 0, handPosition.GetLeftHand().position.z), 
                        new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z)) < 0.25f) && grabbed == false)
        {
            grabbed = true;
        }
    }

    private void ReleaseObject(InputAction.CallbackContext context)
    {
        if (grabbed)
        {
            if (bookOpen && gameObject.CompareTag("Book"))
            {
                CloseBook();
            }
            if (gameObject.CompareTag("Torch"))
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            grabbed = false;
            Invoke("Throw", 0.01f);
            lastPos = gameObject.transform.position;
        }
    }

    private void Throw()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.zero;
        Vector3 v = lastPos - posRecord.Dequeue();
        // Debug.Log(v);
        posRecord = new Queue<Vector3>();
        rb.AddForce(v * 100, ForceMode.Impulse);
    }

    private void DropObject(InputAction.CallbackContext context)
    {
        if (grabbed)
        {
            if (bookOpen && gameObject.CompareTag("Book"))
            {
                CloseBook();
            }
            if (gameObject.CompareTag("Torch"))
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            grabbed = false;
            posRecord = new Queue<Vector3>();
        }
    }

    private void Drop()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(-Vector3.up * 0.2f, ForceMode.Impulse);
    }

    private void OpenBook()
    {
        noteUI.SetActive(true);
        bookOpen = true;
    }

    private void CloseBook()
    {
        noteUI.SetActive(false);
        bookOpen = false;
    }

}
