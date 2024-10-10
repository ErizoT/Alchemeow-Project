using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    //[Tooltip("Box collider that needs to touch the cuttable object")]
    //[SerializeField] BoxCollider cuttingEdge;
    [Tooltip("Downwards velocity required to cut an object (Default: 3)")]
    [SerializeField] float cuttingSpeed = 3f;
    public ParticleSystem systemToPlay;
    [SerializeField] GripHandler gripHandler; // Super unfortunate i had to do it this way, but it just wouldn't find the GripHandler for some reason
    [SerializeField] Rigidbody rb;

    private bool canCut;

    // Dev
    public Vector3 velocity;

    private void Start()
    {
        //rb = GetComponentInParent<Rigidbody>();
        systemToPlay.GetComponent<ParticleSystem>();
        //gripHandler = GetComponentInParent<GripHandler>();
    }

    private void Update()
    {
        velocity = rb.velocity;

        if (rb != null)
        {
            // Get the velocity vector
            Vector3 velocity = rb.velocity;

            // Check if the velocity in the y direction is negative
            if (velocity.y < cuttingSpeed * -1 && gripHandler.cooperativeHold)
            {
                canCut = true;
            }
            else
            {
                canCut = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Cuttable>(out Cuttable objToCut) && canCut)
        {
            //Debug.Log("Crushing...");
            objToCut.Cut();
            //systemToPlay.Play();
        }
    }
}
