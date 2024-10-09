using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class JarBreakHandler : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform spawnPoint;
    public ParticleSystem systemToPlay;

    private ConfigurableJoint joint;
    private float breakForce;
    private bool hasBroken = false;
    private GripHandler gP;

    

    [SerializeField] private StudioEventEmitter jarEmitter;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        systemToPlay.GetComponent<ParticleSystem>();
        gP = GetComponent<GripHandler>();

        // Ensure the joint is assigned and set up the break force
        if (joint == null)
        {
            Debug.LogError("ConfigurableJoint is not assigned.");
            enabled = false;
            return;
        }

        breakForce = joint.breakForce;
        //spawnPoint = transform;
    }

    private void Update()
    {
        if (joint == null)
        {
            // Check if the joint was broken and handle the prefab spawning
            if (!hasBroken)
            {

                if (prefabToSpawn != null && spawnPoint != null)
                {
                    Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
                    prefabToSpawn.SetActive(true);
                    jarEmitter.Play();

                }
                hasBroken = true;
                systemToPlay.Play();
            }
        }

        if (gP.cooperativeHold)
        {
            joint.breakForce = breakForce;
            Debug.Log(joint.currentForce);
        }
        else
        {
            joint.breakForce = Mathf.Infinity;
        }
    }

    /* Optional OnJointBreak if you want to handle it more explicitly
     * 
    private void OnJointBreak(float breakForce)
    {
        if (!hasBroken)
        {
            // Triggered when the joint breaks
            if (prefabToSpawn != null && spawnPoint != null)
            {
                Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            }
            hasBroken = true;
        }
    }*/
}
