using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audio_impactsound : MonoBehaviour
{
    /// <summary>
    /// GENERAL SCRIPT FOR IMPACT SOUNDS BETWEEN ONE OBJECT AND ANOTHER USING IMPACT VELOCITY PARAMETER
    /// </summary>

    // OBJECT IMPACTED AGAINST
    [SerializeField] private GameObject[] collisionObjects;
    int objectNumber = 0;
    private int iCollisionPoints;
    private int currentcollisionpoints = 0;

    // FMOD emitter attached to the object
    private StudioEventEmitter emitter;

    private float impactSpeed;

    private void Start()
    {
        // Get the StudioEventEmitter component attached to the object
        emitter = GetComponent<StudioEventEmitter>();

        if (emitter == null)
        {
            Debug.LogError("No StudioEventEmitter found on this GameObject.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {
            if (collision.gameObject.transform == collisionObjects[objectNumber].transform && emitter != null)
            {
                // Calculate impact speed based on collision velocity
                impactSpeed = collision.relativeVelocity.magnitude;

                // SETS VELOCITY BETWEEN VALUE 0-1
                impactSpeed = Mathf.Clamp(impactSpeed, 0, 3) / 3;

                // Start the emitter event
                emitter.Play();

                // Set the parameter for impact velocity
                emitter.SetParameter("impact_velocity", impactSpeed);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        for (objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {
            if (collision.gameObject.transform == collisionObjects[objectNumber].transform && emitter != null)
            {
                iCollisionPoints = collision.contactCount;
                print(iCollisionPoints);

                if (iCollisionPoints > currentcollisionpoints)
                {
                    print("deez");

                    // Calculate impact speed based on collision velocity
                    impactSpeed = collision.relativeVelocity.magnitude;

                    // SETS VELOCITY BETWEEN VALUE 0-1
                    impactSpeed = Mathf.Clamp(impactSpeed, 0, 3) / 3;

                    // Start the emitter event if it's not already playing
                    if (!emitter.IsPlaying())
                    {
                        emitter.Play();
                    }

                    // Set the parameter for impact velocity
                    emitter.SetParameter("impact_velocity", impactSpeed);
                }

                currentcollisionpoints = iCollisionPoints;
            }
        }
    }
}
