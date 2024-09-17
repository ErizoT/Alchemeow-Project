using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarBreakHandler : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform spawnPoint;

    private ConfigurableJoint joint;
    private float breakForce;
    private bool hasBroken = false;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();

        // Ensure the joint is assigned and set up the break force
        if (joint == null)
        {
            Debug.LogError("ConfigurableJoint is not assigned.");
            enabled = false;
            return;
        }

        breakForce = joint.breakForce;
        spawnPoint = transform;
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
                }
                hasBroken = true;
            }
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
