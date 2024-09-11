using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Crushable : MonoBehaviour
{
    public bool inBowl;
    private float impactVelocity;
   [SerializeField] GameObject prefabToSpawn;
   [SerializeField] FMODUnity.EventReference crushSound;
    private bool hasBeenCrushed;
    

    public void Crush()
    {
        if (inBowl && !hasBeenCrushed)
        {
            //Debug.Log("Instantiated 1 Block");
            // Emit a big af particle effect
            // Play a sound
            // Delete the current gameobject
            // Spawn prefabToSpawn in its place
            FMODUnity.RuntimeManager.PlayOneShot(crushSound);
            hasBeenCrushed = true;
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
            Destroy(gameObject);
            
        }
    }
}
