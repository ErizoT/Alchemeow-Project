using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Billboard : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        transform.forward = -transform.forward;
    }
}
