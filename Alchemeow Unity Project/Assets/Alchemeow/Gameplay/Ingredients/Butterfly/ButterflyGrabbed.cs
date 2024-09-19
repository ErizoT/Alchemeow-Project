using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyGrabbed : MonoBehaviour
{
    [SerializeField] public PawController script;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (script.isHolding == true)
        {
            GetComponent<Animator>().enabled = false;
            Debug.Log("Help Holy fuck");
        }
    }
}
