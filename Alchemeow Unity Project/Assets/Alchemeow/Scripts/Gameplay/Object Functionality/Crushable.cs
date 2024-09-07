using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushable : MonoBehaviour
{
    public bool inBowl;

    [SerializeField] GameObject[] prefabToSpawn;

    private void OnCollisionEnter(Collision collision)
    {
        if (!inBowl) return;

        else if (inBowl) // AND that the pestel is moving at the correct speed // Will have to check collision layers
        {
            // Emit a big af particle effect
            // Delete the current gameobject
            // Spawn prefabToSpawn in its place
        }
    }
}
