using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestelScript : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb != null)
        {
            // Get the velocity vector
            Vector3 velocity = rb.velocity;

            // Check if the velocity in the y direction is negative
            if (velocity.y < -3f)
            {
                Debug.Log("The object is moving downwards.");
            }
            else
            {
                Debug.Log("The object is not moving downwards.");
            }
        }
    }
}
