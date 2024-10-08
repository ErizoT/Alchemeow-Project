using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audio_slimeballboings : MonoBehaviour
{
    /////               ////
    ///     ATTEMPT 1    ///
    //////             /////


    private float totalDeformation;
    private float normalizedDeformation;
    private SpringJoint[] springJoints;

    [SerializeField] private StudioEventEmitter slimeEmitter;

    private void Start()
    {
        springJoints = this.GetComponents<SpringJoint>();
        slimeEmitter.Play();
    }

    // Update is called once per frame
    void Update()
    {
        totalDeformation = 0f;
        foreach (var springJoint in springJoints)
        {
            totalDeformation += Mathf.Abs(springJoint.currentForce.y);

            normalizedDeformation = Mathf.InverseLerp(0, 1000, totalDeformation);
            slimeEmitter.SetParameter("squash_amount", normalizedDeformation);
        }

       // print("total deformation: " + totalDeformation + "normalized deformation: " + normalizedDeformation);

    }



    /////               ////
    ///     ATTEMPT 2    ///
    //////             /////


    //private Vector3[] initialDistances;
    //private SpringJoint[] springJoints;

    //[SerializeField] private StudioEventEmitter slimeEmitter;

    //void Start()
    //{
    //    //play emitter
    //    slimeEmitter.Play();

    //    // Get all SpringJoints
    //    springJoints = GetComponentsInChildren<SpringJoint>();

    //    // Store the initial distances between each joint and its connected anchor
    //    initialDistances = new Vector3[springJoints.Length];
    //    for (int i = 0; i < springJoints.Length; i++)
    //    {
    //        initialDistances[i] = springJoints[i].transform.position - springJoints[i].connectedAnchor;
    //    }
    //}

    //void Update()
    //{
    //    float totalSquashFactor = 0f;

    //    // Calculate the deformation based on the current distance
    //    for (int i = 0; i < springJoints.Length; i++)
    //    {
    //        Vector3 currentDistance = springJoints[i].transform.position - springJoints[i].connectedAnchor;

    //        // Squash factor based on the change in distance
    //        float squashFactor = (currentDistance.magnitude - initialDistances[i].magnitude) / initialDistances[i].magnitude;

    //        totalSquashFactor += squashFactor;
    //    }

    //    // Normalize total squash factor
    //    float normalizedSquashFactor = totalSquashFactor * -1f;
    //    /// springJoints.Length

    //    // Use normalizedSquashFactor to control FMOD or other properties
    //    Debug.Log("Squash Factor: " + normalizedSquashFactor);

    //    slimeEmitter.SetParameter("squash_amount", normalizedSquashFactor);

    //}


}
