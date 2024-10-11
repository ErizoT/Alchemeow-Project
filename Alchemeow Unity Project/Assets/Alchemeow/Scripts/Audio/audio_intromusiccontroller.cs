using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audio_intromusiccontroller : MonoBehaviour
{

    [SerializeField] private StudioEventEmitter musicEmitter;
  
    public void EndMusicLoop()
    {

        musicEmitter.SetParameter("startGame", 1);

    }


}
