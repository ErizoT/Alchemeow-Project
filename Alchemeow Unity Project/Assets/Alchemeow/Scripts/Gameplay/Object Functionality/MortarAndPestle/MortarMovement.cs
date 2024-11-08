using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float rotationSpeed = 50f;

    [HideInInspector] public bool canMove;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * movementSpeed);

            // Rotate the object around the Y-axis (horizontal rotation)
            Quaternion rotation = Quaternion.Euler(0, rotationSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * rotation);
        }
        
    }
}
