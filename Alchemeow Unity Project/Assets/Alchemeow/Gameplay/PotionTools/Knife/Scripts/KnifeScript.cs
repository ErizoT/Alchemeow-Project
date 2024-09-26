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

    private Rigidbody rb;
    private bool canCut;
    private ParticleSystem systemToPlay;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        systemToPlay.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (rb != null)
        {
            // Get the velocity vector
            Vector3 velocity = rb.velocity;

            // Check if the velocity in the y direction is negative
            if (velocity.y < cuttingSpeed)
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
