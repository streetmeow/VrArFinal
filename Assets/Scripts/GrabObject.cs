using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObject : MonoBehaviour
{
    private HandPosition handPosition;
    private InputActionReference reference;
    private InputActionReference releaseTrigger;
    private InputActionReference xTrigger;
    private bool grabbed = false;
    private Queue<Vector3> posRecord;
    private Vector3 lastPos;
    
    private void Awake()
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
    }

    private void Update()
    {
        if (grabbed)
        {
            gameObject.transform.position = handPosition.GetLeftHand().position + new Vector3(0.1f, 0.1f, 0.1f);
            if (posRecord.Count > 15) posRecord.Dequeue();
            posRecord.Enqueue(gameObject.transform.position);
            
        }
    }

    private void GrabObjectVoid(InputAction.CallbackContext context)
    {
        if (Vector3.Distance(handPosition.GetLeftHand().position, gameObject.transform.position) < 0.2f && grabbed == false)
        {
            grabbed = true;
        }
    }

    private void ReleaseObject(InputAction.CallbackContext context)
    {
        if (grabbed)
        {
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
}
