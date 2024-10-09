using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audio_splashtrigger : MonoBehaviour
{
    /// <summary>
    /// SCRIPT FOR CREATING A SPLASH SOUND WHEN OBJECTS ENTER THE CAULDRON
    /// </summary>

    // OBJECT IMPACTED AGAINST
    [SerializeField] private GameObject[] splashObjects;
    int objectNumber = 0;
    private int iCollisionPoints;
    private int currentcollisionpoints = 0;

    // FMOD emitter attached to the object, exposed to Inspector
    [SerializeField] private StudioEventEmitter splashEmitter;

    private float impactSpeed;

    private void Start()
    {
        // Check if the emitter is assigned, if not, log an error
        if (splashEmitter == null)
        {
            Debug.LogError("No StudioEventEmitter assigned in the Inspector.");
        }





    }

    private void OnTriggerEnter(Collider other)
    {
        for (objectNumber = 0; objectNumber < splashObjects.Length; objectNumber++)
        {
            if (other.gameObject.transform == splashObjects[objectNumber].transform && splashEmitter != null)
            {
                // Calculate impact speed based on relative velocity (if Rigidbody exists)
                Rigidbody rb = other.attachedRigidbody;
                if (rb != null)
                {
                    impactSpeed = rb.velocity.magnitude;

                    // SETS VELOCITY BETWEEN VALUE 0-1
                    impactSpeed = Mathf.Clamp(impactSpeed, 0, 3) / 3;

                    // Set the parameter for impact velocity
                   
                    splashEmitter.SetParameter("splash_velocity", 1);

                    print(impactSpeed);
                    // Start the emitter event
                    splashEmitter.Play();
                }

                break;
            }
        }
    }
}




