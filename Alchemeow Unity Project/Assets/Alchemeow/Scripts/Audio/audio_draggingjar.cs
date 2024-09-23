using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class audio_draggingjar : MonoBehaviour
{
    /// <summary>
    /// SCRIPT FOR DRAGGING JARS 
    /// </summary>
    ///


    // OBJECTS IMPACTED AGAINST
    [SerializeField] private GameObject[] collisionObjects;
    int objectNumber = 0;


    // SOUND BEING MADE
    [SerializeField] private StudioEventEmitter dragEmitter;

    //BODY OF IMPACT OBJECT
    [SerializeField] private Rigidbody objectBody;


    private float objectVelocity;
    private float objectYVelocity;
    private bool currentlyColliding;
    private int iCollisionPoints;

    private void Start()
    {
        dragEmitter.Play();
    }


    private void OnCollisionEnter(Collision collision)
    {

        //CHECKS ARRAY FOR COLLISION 
        for (objectNumber = 0; objectNumber < collisionObjects.Length; objectNumber++)
        {

            if (collision.gameObject.transform == collisionObjects[objectNumber].transform)
            {

                
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
                dragEmitter.SetParameter("objectVelocity", 0);
                currentlyColliding = false;

            }
        }
    }

    private void Update()
    {
        if (currentlyColliding == false || objectVelocity < 0.199f || objectBody.velocity.y > 0.3f)
        {

            //dragEmitter.SetParameter("objectVelocity", 0);

        }

    }

    private void OnCollisionStay(Collision collision)
    {

        //iCollisionPoints = collision.contactCount;
        objectVelocity = Vector3.Magnitude(objectBody.velocity);
        //iCollisionPoints = Mathf.Clamp(iCollisionPoints, 0, 4);

        if (currentlyColliding == true)
        {

            objectVelocity = Mathf.Clamp(objectVelocity, 0, 3);
            //objectVelocity = ((objectVelocity * iCollisionPoints) / 12);
            objectVelocity = (objectVelocity / 3);
            dragEmitter.SetParameter("objectVelocity", objectVelocity);
            print(objectVelocity);
        }

       
    }

}