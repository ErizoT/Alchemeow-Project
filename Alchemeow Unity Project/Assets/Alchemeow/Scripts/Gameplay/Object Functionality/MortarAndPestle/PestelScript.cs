using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PestelScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool canCrush;

    public ParticleSystem systemToPlay;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        systemToPlay.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (rb != null)
        {
            // Get the velocity vector
            Vector3 velocity = rb.velocity;

            // Check if the velocity in the y direction is negative
            if (velocity.y < -3f)
            {
                canCrush = true;
            }
            else
            {
                canCrush = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Crushable>(out Crushable objToCrush) && canCrush)
        {
            //Debug.Log("Crushing...");
            objToCrush.Crush();
            systemToPlay.Play();
        }
    }
}
