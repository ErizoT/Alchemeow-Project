using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audio_dragging : MonoBehaviour
{
    /// <summary>
    /// GENERAL SCRIPT FOR DRAGGING SOUNDS BETWEEN ONE OBJECT AND ANOTHER USING OBJECT VELOCITY PARAMETER
    /// </summary>

    // OBJECTS IMPACTED AGAINST
    [SerializeField] private GameObject[] collisionObjects;
    int objectNumber = 0;

    // SOUND BEING MADE
    [SerializeField] private StudioEventEmitter dragEmitter;

    // BODY OF IMPACT OBJECT
    [SerializeField] private Rigidbody objectBody;

    private float objectVelocity;
    private bool currentlyColliding;
    private int iCollisionPoints;

    private void Start()
    {
        // Initialization if needed
    }

    private bool IsCollisionWithTrackedObject(Collision collision)
    {
        // Loop through the collisionObjects array and check if the collided object is in the array
        foreach (GameObject obj in collisionObjects)
        {
            if (collision.gameObject == obj)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsCollisionWithTrackedObject(collision))
        {
            dragEmitter.Play();
            currentlyColliding = true;
            Debug.Log("audio started");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (IsCollisionWithTrackedObject(collision))
        {
            dragEmitter.SetParameter("objectVelocity", objectVelocity);
            currentlyColliding = false;
            Debug.Log("audio stopped");
        }
    }

    private void Update()
    {
        // Ensure parameters are within bounds before applying sound adjustments
        if (!currentlyColliding || objectVelocity < 0.199f || objectBody.velocity.y > 0.3f)
        {
            Debug.Log("high parameters not met");
            dragEmitter.SetParameter("objectVelocity", 0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsCollisionWithTrackedObject(collision))
        {
            iCollisionPoints = collision.contactCount;
            iCollisionPoints = Mathf.Clamp(iCollisionPoints, 0, 4); // Limit collision points to a max of 4

            objectVelocity = Vector3.Magnitude(objectBody.velocity);
            objectVelocity = Mathf.Clamp(objectVelocity, 0, 3); // Clamp velocity to a max of 3
            objectVelocity = (objectVelocity * iCollisionPoints) / 12;

            dragEmitter.SetParameter("objectVelocity", objectVelocity);
            Debug.Log("currently playing audio at " + objectVelocity);
        }
    }
}
