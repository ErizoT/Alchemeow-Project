using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PawController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float raiseLowerSpeed = 10f;
    [SerializeField] GameObject visuals;
    [SerializeField] Rigidbody dummyRigidbody;
    [Range(.5f, 1f)]
    [SerializeField] float damping = 0.7f;

    private bool isHolding;

    // Movement Input
    private Vector2 moveVector;
    private Vector2 rotateVector;
    private float raiseValue;
    private Rigidbody rb;
    private HingeJoint hinge;
    //private Quaternion defaultRotation;

    private GameObject nearestObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //defaultRotation = visuals.transform.rotation;
        hinge = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        // Player Movement
        Vector3 playerVel = new Vector3(moveVector.x, raiseValue, moveVector.y) * moveSpeed;
        rb.AddForce(playerVel, ForceMode.Acceleration);

        // Player Rotation
        if (!isHolding)
        {
            // Should do a spherecast instead of checking every object in the scene due to performance

            //Find all objects with tag "Object"
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Objects"); // Should make it "Grabbable" instead of "Objects"
            hinge.connectedBody = dummyRigidbody;

            if (objects.Length == 0)
                return;

            // Find the nearest object
            nearestObject = null;
            float minDistance = Mathf.Infinity;
            float lookThreshold = 5f;
            Vector3 currentPosition = transform.position;

            foreach (GameObject obj in objects)
            {
                float distance = Vector3.Distance(currentPosition, obj.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestObject = obj;
                }
            }

            //nearestObject.GetComponentInParent<HingeJoint>().connectedBody = null;

            // If a nearest object is found, make this object look at it
            if (nearestObject != null && minDistance <= lookThreshold)
            {
                visuals.transform.LookAt(nearestObject.transform);
                visuals.transform.Rotate(0, 90, -90);
            }
        }

        if (isHolding)
        {
            transform.position = nearestObject.transform.position;
            hinge.connectedBody = nearestObject.GetComponentInParent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        // Velocity Damping
        Vector3 tempV = rb.velocity;
        tempV = Vector3.Scale(tempV, new Vector3(damping, damping, damping));
        tempV = new Vector3(tempV.x, tempV.y, tempV.z);
        rb.velocity = tempV;
    }
    public void OnMove(InputAction.CallbackContext input)
    {
        moveVector = input.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext input)
    {
        rotateVector = input.ReadValue<Vector2>();
        Debug.Log(rotateVector);
    }

    public void OnGrab(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            if (isHolding)
            {
                isHolding = false;
            }
            else
            {
                isHolding = true;
            }
        }
    }

    public void RaiseLower(InputAction.CallbackContext input)
    {
        raiseValue = input.ReadValue<float>() * raiseLowerSpeed;

        
    }
}
