using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODPlayPop : MonoBehaviour
{

    private ParticleSystem parentParticleSystem;

    private int currentNumberOfParticles = 0;

    private float bubbleLifespan;
    

    void Start()
    {
        parentParticleSystem = this.GetComponent<ParticleSystem>();
        if (parentParticleSystem == null)
        {
            Debug.LogError("No particle system found!");
        }
        
    }


    void Update()
    {

        bubbleLifespan = Random.Range(2f, 4f);
        parentParticleSystem.startLifetime = bubbleLifespan;

       
        var amount = Mathf.Abs(currentNumberOfParticles - parentParticleSystem.particleCount);

        if (parentParticleSystem.particleCount < currentNumberOfParticles)
        {
                                  GetComponent<FMODUnity.StudioEventEmitter>().Play();
           
        }

        if(parentParticleSystem.particleCount > currentNumberOfParticles)
        {
           // place audio for new bubble here if wanted
        }

        currentNumberOfParticles = parentParticleSystem.particleCount;
    }

    }