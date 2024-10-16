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
    public static GameObject[] collisionObjects;
    int objectNumber = 0;
    private int iCollisionPoints;
    private int currentcollisionpoints = 0;

    // FMOD emitter attached to the object, exposed to Inspector
    [SerializeField] private StudioEventEmitter impactEmitter;

    private float impactSpeed;

    private void Start()
    {
        // Check if the emitter is assigned, if not, log an error
        if (impactEmitter == null)
        {
            Debug.LogError("No StudioEventEmitter assigned in the Inspector.");
        }


        if (collisionObjects == null || collisionObjects.Length == 0)
        {
            collisionObjects = new GameObject[8]; // Define the size of the array (change as needed)
            collisionObjects[0] = GameObject.Find("TableBase");
            collisionObjects[1] = GameObject.Find("TableBack");
            collisionObjects[2] = GameObject.Find("MortarParent");
            collisionObjects[3] = GameObject.Find("SM_Bookcase_Left");
            collisionObjects[4] = GameObject.Find("SM_Bookcase_Right");
            collisionObjects[5] = GameObject.Find("envirnoment walls only");
            collisionObjects[6] = GameObject.Find("CauldronLowerPoly");
            collisionObjects[7] = GameObject.Find("SM_CrystalBall_Base");
           

        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        for (objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {
            if (collision.gameObject.transform == collisionObjects[objectNumber].transform && impactEmitter != null && gameObject != null)
            {
                // Calculate impact speed based on collision velocity
                impactSpeed = collision.relativeVelocity.magnitude;

                // SETS VELOCITY BETWEEN VALUE 0-1
                impactSpeed = Mathf.Clamp(impactSpeed, 0, 3) / 3;

                // Start the emitter event
                impactEmitter.Play();

                // Set the parameter for impact velocity
                impactEmitter.SetParameter("impact_velocity", impactSpeed);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        for (objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {
            if (collision.gameObject.transform == collisionObjects[objectNumber].transform && impactEmitter != null && gameObject != null)
            {
                iCollisionPoints = collision.contactCount;

                if (iCollisionPoints > currentcollisionpoints)
                {
                    // Calculate impact speed based on collision velocity
                    impactSpeed = collision.relativeVelocity.magnitude;
                   
                    // SETS VELOCITY BETWEEN VALUE 0-1
                    impactSpeed = Mathf.Clamp(impactSpeed, 0, 3) / 3;

                    // Start the emitter event if it's not already playing

                    impactEmitter.Play();

                    // Set the parameter for impact velocity
                    impactEmitter.SetParameter("impact_velocity", impactSpeed);

                   
                }

                currentcollisionpoints = iCollisionPoints;
            }
        }
    }
}
