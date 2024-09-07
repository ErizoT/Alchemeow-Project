using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarFunctionality : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Crushable>(out Crushable objToCrush))
        {
            objToCrush.inBowl = true;
        }
        else return;
    }
}
