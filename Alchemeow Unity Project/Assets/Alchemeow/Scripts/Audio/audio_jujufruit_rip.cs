using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audio_jujufruit_rip : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter ripEmitter;
    [SerializeField] private GameObject jujuPot;
    private bool hasPlayed = false;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform == jujuPot.transform && ripEmitter != null && hasPlayed == false)
        {
            hasPlayed = true;

            ripEmitter.Play();

            Debug.Log("sigma");

        }
    }
}