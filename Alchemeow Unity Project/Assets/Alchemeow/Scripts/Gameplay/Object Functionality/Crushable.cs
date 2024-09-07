using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushable : MonoBehaviour
{
    public bool inBowl;

    [SerializeField] GameObject prefabToSpawn;

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
            hasBeenCrushed = true;
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
