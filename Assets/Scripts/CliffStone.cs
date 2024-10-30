using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CliffStone : MonoBehaviour
{
    public Transform handTransform;
    public Transform headTransform;
    public InputActionReference reference;
    public Rigidbody rb;
    
    private void Awake()
    {
        reference.action.Enable();
        reference.action.performed += GrabCliffVoid;
    }

    private void Update()
    {
        
    }

    private void GrabCliffVoid(InputAction.CallbackContext context)
    {
        if (Vector3.Distance(handTransform.position, gameObject.transform.position) < 0.2f)
        {
            rb.useGravity = false;
            headTransform.position = Vector3.MoveTowards(headTransform.position, 
                gameObject.transform.position + new Vector3(0, 0.7f, -0.5f), 0.3f);
        } 
    }
}
