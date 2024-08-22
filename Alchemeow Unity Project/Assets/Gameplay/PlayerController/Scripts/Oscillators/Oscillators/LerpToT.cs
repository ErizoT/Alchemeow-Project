using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToT : MonoBehaviour
{
    public Transform toLerp;
    public float lerpSpeed;
    public float rotSpeed;
    public float scaleSpeed;
    public bool lerpPosition;
    public bool setPosition;
    public bool lerpRotation;
    public bool lerpScale;
    public bool unChild;
    
    void Start() 
    {
        if (unChild)
        {
            transform.parent = null;
        }
    }

    void Update()
    {
        if (lerpPosition)
        {
            transform.position = Vector3.Lerp(transform.position, toLerp.position, lerpSpeed * Time.deltaTime);
        }
        else if (setPosition)
        {
            transform.position = toLerp.position;
        }

        if (lerpRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, toLerp.rotation, rotSpeed * Time.deltaTime);
        }

        if (lerpScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, toLerp.localScale, scaleSpeed * Time.deltaTime);
        }
    }
}
