using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillateTarget : MonoBehaviour
{
    private RotOscillator rotO;
    private PosOscillator posO;
    public GameObject childObject;
    public bool updateEveryFrame = true;
    public bool useWorldPos = true;
    public bool unParent = false;

    void Start()
    {
        Rigidbody rb = childObject.GetComponent<Rigidbody>();

        if (unParent)
        {
            childObject.transform.parent = null;
        }

        if (rb)
        {
            rotO = childObject.GetComponent<RotOscillator>();
            posO = childObject.GetComponent<PosOscillator>();

            try
            {
                rotO.localEquilibriumRotation = transform.rotation.eulerAngles;
            }catch{}

            if (useWorldPos)
            {
                posO.localEquilibriumPosition = transform.position;
            }
            else
            {
                posO.localEquilibriumPosition = transform.localPosition;
            }
        }

        posO = childObject.GetComponent<PosOscillator>();
        if (useWorldPos)
            {
                posO.localEquilibriumPosition = transform.position;
            }
            else
            {
                posO.localEquilibriumPosition = transform.localPosition;
            }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (updateEveryFrame)
        {
            if (rotO)
            {
                rotO.localEquilibriumRotation = transform.rotation.eulerAngles;
            }
            if (useWorldPos)
            {
                posO.localEquilibriumPosition = transform.position;
            }
            else
            {
                posO.localEquilibriumPosition = transform.localPosition;
            }
        }
    }
}
