using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class audio_dragging : MonoBehaviour
{
    /// <summary>
    /// GENERAL SCRIPT FOR DRAGGIN SOUNDS BETWEEN ONE OBJECT AN ANOTHER USING OBJECT VELOCITY PARAMETER
    /// </summary>
    ///


    // OBJECTS IMPACTED AGAINST
    [SerializeField] private GameObject[] collisionObjects;
    int objectNumber = 0;


    // SOUND BEING MADE
    [SerializeField] FMODUnity.EventReference impactEvent;
    private FMOD.Studio.EventInstance impactInstance;

    //BODY OF IMPACT OBJECT
    [SerializeField] Rigidbody objectBody;
    

    private float objectVelocity;
    private float objectYVelocity;
    private bool currentlyColliding = false;
    
    
    

    private void OnCollisionEnter(Collision collision)
    {
       //CHECKS ARRAY FOR COLLISION 
        for (objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {
            if (collision.gameObject.transform == collisionObjects[objectNumber].transform)
            {
                impactInstance.release();
                currentlyColliding = true;

                impactInstance = FMODUnity.RuntimeManager.CreateInstance(impactEvent);
                impactInstance.start();
            }
        }
        

    }

    private void OnCollisionExit(Collision collision)
    {
        //CHECKS ARRAY FOR COLLISION 
        for (objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {
            if (collision.gameObject.transform == collisionObjects[objectNumber].transform)
            {
                impactInstance.setParameterByName("objectVelocity", 0);
                impactInstance.release();
                currentlyColliding = false;
            }
        }
    }

    private void Update()
    {
        if(currentlyColliding == true)
        {
            //DEBUG
            objectVelocity = Vector3.Magnitude(objectBody.velocity);
            if(objectBody.velocity.y > 1)
            {
                impactInstance.release();
                impactInstance.setParameterByName("objectVelocity", 0);
            }
            
            //LIMITS VALUES BETWEEN 0 AND 1 FOR FMOD
            Mathf.Clamp(objectVelocity, 0, 3);
            objectVelocity = (objectVelocity / 3);
            impactInstance.setParameterByName("objectVelocity", objectVelocity);
            
        } 
        
    }
}

    

