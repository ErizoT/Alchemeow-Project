using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {get; private set;}

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("ERROR: Duplicate Audio Manager");
            }
        instance = this;
    }

    public void PlayOneShot(EventReference testSound, Vector3 worldPosition)
        {
            RuntimeManager.PlayOneShot(testSound, worldPosition);
        }

}
