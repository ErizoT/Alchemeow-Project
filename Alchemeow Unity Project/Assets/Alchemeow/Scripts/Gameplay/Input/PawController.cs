using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using FMODUnity;

public class PawController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotateSpeed = 10f;
    [Range(.5f, 1f)]
    [SerializeField] float damping = 0.7f;
    
    //CALLUM FMOD TEST
    [SerializeField] private EventReference goodjobyougrabbedsound;
    [SerializeField] private EventReference badjobyounograbbedsound;
    

    private bool isHolding;
    

    // Movement Input
    private Vector2 moveVector;
    private Vector2 rotateVector;
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Player Movement
        Vector3 playerVel = new Vector3(moveVector.x, 0, moveVector.y) * moveSpeed;
        rb.AddForce(playerVel, ForceMode.Acceleration);

        // Player Rotation
        //Vector3 playerRotation = new Vector3(rotateVector.x, 0, rotateVector.y) * rotateSpeed;
        //rb.AddTorque(playerRotation, ForceMode.Acceleration);
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
    }

    public void OnGrab(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            if (isHolding)
            {
                isHolding = false;
                Debug.Log("Not Grabbing");
                AudioManager.instance.PlayOneShot(badjobyounograbbedsound, this.transform.position);
            }
            else
            {
                isHolding = true;
                Debug.Log("Grabbed!");
                AudioManager.instance.PlayOneShot(goodjobyougrabbedsound, this.transform.position);
                
            }
        }
    }
    
    
    
}
