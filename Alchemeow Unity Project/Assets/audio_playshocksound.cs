using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audio_playshocksound : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter shockEmitter;
   void playShockSound()
    {
        shockEmitter.Play();
    }
}
