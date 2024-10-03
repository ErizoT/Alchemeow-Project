using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audio_playsmokesound : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter smokesoundEmitter;

    private void OnParticleTrigger()
    {
        smokesoundEmitter.Play();
    }
}
