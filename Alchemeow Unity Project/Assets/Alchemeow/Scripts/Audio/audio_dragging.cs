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
    [SerializeField]  private Rigidbody objectBody;
    

    private float objectVelocity;
    private float objectYVelocity;
    private bool currentlyColliding;
    private int iCollisionPoints;


     private void OnCollisionEnter(Collision collision)
    {
            
        //CHECKS ARRAY FOR COLLISION 
        for (objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {
            if (collision.gameObject.transform == collisionObjects[objectNumber].transform)
            {
                print("instance created");
                impactInstance = FMODUnity.RuntimeManager.CreateInstance(impactEvent);
                impactInstance.start();
                currentlyColliding = true;
                                               
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
                impactInstance.setParameterByName("objectVelocity", objectVelocity);
                currentlyColliding = false;
                print("false");
            }
        }
    }

    private void Update()
    {
       if (currentlyColliding == false || objectVelocity < 0.1 || objectBody.velocity.y > 0.3)
        {
                    
            impactInstance.setParameterByName("objectVelocity", 0);
        }

       }

    private void OnCollisionStay(Collision collision)
    {
        iCollisionPoints = collision.contactCount;
        objectVelocity = Vector3.Magnitude(objectBody.velocity);
        iCollisionPoints = Mathf.Clamp(iCollisionPoints, 0, 4);

        if (currentlyColliding == true)
        {
            
            objectVelocity = Mathf.Clamp(objectVelocity, 0, 3);
                        objectVelocity = ((objectVelocity * iCollisionPoints) / 12);
                          impactInstance.setParameterByName("objectVelocity", objectVelocity);
        }
    }

}