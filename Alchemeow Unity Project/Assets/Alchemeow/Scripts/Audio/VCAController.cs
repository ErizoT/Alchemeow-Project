using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class VCAController : MonoBehaviour

{

    //public float masterVolume;
    //public float musicVolume;
    //public float sfxVolume;
    //public float uiVolume;
      

    public void ChangeMasterVolume(float masterVolume)
    {
        RuntimeManager.StudioSystem.setParameterByName("MasterVolume", masterVolume);
        print(masterVolume);

    }
    public void ChangeMusicVolume(float musicVolume)
    {
        RuntimeManager.StudioSystem.setParameterByName("MusicVolume", musicVolume);
    }
    public void ChangeSFXVolume(float sfxVolume)
    {
        RuntimeManager.StudioSystem.setParameterByName("SFXVolume", sfxVolume);
    }
    public void ChangeUIVolume(float uiVolume)
    {
        RuntimeManager.StudioSystem.setParameterByName("UIVolume", uiVolume);
    }


}
