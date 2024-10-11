using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class audio_UIHOVER : MonoBehaviour, IPointerEnterHandler
{
    //plays a definable sound on hover

    [SerializeField] private EventReference hoverSoundEvent;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        RuntimeManager.PlayOneShot(hoverSoundEvent);
    }
}
