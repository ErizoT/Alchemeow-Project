using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Cuttable : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] StudioEventEmitter cutEmitter;

    private bool hasBeenCut;

    public void Cut()
    {
        if (!hasBeenCut)
        {
            hasBeenCut = true;
            cutEmitter.Play();
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
