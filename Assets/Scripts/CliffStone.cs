using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class CliffStone : MonoBehaviour
{
    private HandPosition handTransform;
    private Transform headTransform;
    private InputActionReference reference1;
    private InputActionReference reference2;
    private Rigidbody rb;
    
    private void Start()
    {
        handTransform = HandPosition.Instance;
        headTransform = CustomInputs.instance.playerTransform;
        rb = CustomInputs.instance.playerRigidBody;
        reference1 = CustomInputs.instance.leftHandSelectButton;
        reference2 = CustomInputs.instance.rightHandSelectButton;
        // reference1.action.Enable();
        reference1.action.performed += GrabCliffVoid;
        reference2.action.performed += GrabCliffVoid;
    }

    private void Update()
    {
        
    }

    private void GrabCliffVoid(InputAction.CallbackContext context)
    {
        if (Vector3.Distance(handTransform.GetLeftHand().position, gameObject.transform.position) < 1.3f || 
                Vector3.Distance(handTransform.GetRightHand().position, gameObject.transform.position) < 1.3f)
        {
            rb.useGravity = false;
            headTransform.position = Vector3.MoveTowards(headTransform.position, 
                gameObject.transform.position + headTransform.TransformDirection(new Vector3(0, 1f, -0.5f)), 0.3f);
        }
        if (headTransform.position.y > -5.2f)
        {
            headTransform.position = Vector3.MoveTowards(headTransform.position,
                headTransform.position + headTransform.TransformDirection(new Vector3(0, 5f, 1.5f)), 0.3f);
            rb.useGravity = true;
        }
    }
}
