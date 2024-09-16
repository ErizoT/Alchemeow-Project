using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * -1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * -0.5f);
    }
}
