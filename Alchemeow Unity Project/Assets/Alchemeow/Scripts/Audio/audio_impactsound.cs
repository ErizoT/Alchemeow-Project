using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class audio_impactsound : MonoBehaviour
{
    /// <summary>
    /// GENERAL SCRIPT FOR IMPACT SOUNDS BETWEEN ONE OBJECT AN ANOTHER USING IMPACT VELOCITY PARAMETER
    /// </summary>
    ///


    // OBJECT IMPACTED AGAINST
    [SerializeField] private GameObject[] collisionObjects;
    int objectNumber = 0;


    // SOUND BEING MADE
    [SerializeField] FMODUnity.EventReference impactEvent;
    private FMOD.Studio.EventInstance impactInstance;

    private float impactSpeed;

    void OnCollisionEnter(Collision collision)
    {
        for(objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {
            if (collision.gameObject.transform == collisionObjects[objectNumber].transform)
            {

                impactInstance = FMODUnity.RuntimeManager.CreateInstance(impactEvent);

                impactSpeed = collision.relativeVelocity.magnitude;

                // SETS VELOCITY BETWEEN VALUE 0-1
                Mathf.Clamp(impactSpeed, 0, 3);
                impactSpeed = (impactSpeed / 3);

                impactInstance.start();
                impactInstance.setParameterByName("impact_velocity", impactSpeed);

            }
        }
        

    }
}

    

