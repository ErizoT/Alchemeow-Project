using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripPoint : MonoBehaviour
{
    public GripHandler gripHandler;
    public PawController pC;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * -1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * -0.5f);
    }

    public void Gripped(PawController controller)
    {
        // Keep track of the current player holding the thing
        pC = controller;

        // Set the tag of the grip point to default
        gameObject.tag = "Untagged";
        gripHandler.holds += 1;
    }

    public void Ungripped()
    {
        pC = null;

        // Set the tag of the grip point to Holdable
        gameObject.tag = "Holdable";
        gripHandler.holds -= 1;
    }
}
