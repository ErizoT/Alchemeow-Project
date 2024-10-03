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
    [SerializeField] StudioEventEmitter draggingEmitter;

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

                draggingEmitter.Play();
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
                draggingEmitter.SetParameter("objectVelocity", objectVelocity);
                currentlyColliding = false;
               
            }
        }
    }

    private void Update()
    {
       if (currentlyColliding == false || objectVelocity < 0.199f || objectBody.velocity.y > 0.3f)
        {
                    
            draggingEmitter.SetParameter("objectVelocity", 0);
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
            draggingEmitter.SetParameter("objectVelocity", objectVelocity);
        }
    }

}