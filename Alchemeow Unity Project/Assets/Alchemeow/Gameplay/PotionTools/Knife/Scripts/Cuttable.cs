using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;

    private bool hasBeenCut;

    public void Cut()
    {
        if (!hasBeenCut)
        {
            hasBeenCut = true;
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
