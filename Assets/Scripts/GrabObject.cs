using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObject : MonoBehaviour
{
    public HandPosition handPosition;
    public InputActionReference reference;
    public Rigidbody rb;
    private bool grabbed = false;
    private Vector3 currentPos;
    private Vector3 movePos;
    
    private void Awake()
    {
        reference.action.Enable();
        reference.action.performed += GrabObjectVoid;
    }

    private void Update()
    {
        if (grabbed)
        {
            gameObject.transform.position = handPosition.GetLeftHand().position + new Vector3(0.1f, 0.1f, 0.1f);
            movePos = gameObject.transform.position - currentPos;
            currentPos = gameObject.transform.position;
        }
    }

    private void GrabObjectVoid(InputAction.CallbackContext context)
    {
        if (Vector3.Distance(handPosition.GetLeftHand().position, gameObject.transform.position) < 0.2f && grabbed == false)
        {
            grabbed = true;
            currentPos = gameObject.transform.position;
        } else if (grabbed)
        {
            grabbed = false;
            Invoke("Throw", 0.01f);
        }
    }

    private void Throw()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(movePos) * 10, ForceMode.Impulse);
    }
}
