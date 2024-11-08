using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarFunctionality : MonoBehaviour
{
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask layerMask;

    [HideInInspector] public MortarMovement mortarMovement;
    private GripHandler gP;
    private bool isGrounded;
    private bool objInBowl;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, .1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (-transform.up * groundCheckDistance));
    }

    private void Start()
    {
        gP = GetComponent<GripHandler>();
        mortarMovement = GetComponentInParent<MortarMovement>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, layerMask);
        
        if(isGrounded && objInBowl)
        {
            mortarMovement.canMove = true;
        }
        else
        {
            mortarMovement.canMove = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Crushable>(out Crushable objToCrush))
        {
            objToCrush.inBowl = true;
            objInBowl = true;
        }
        else return;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Crushable>(out Crushable objToCrush))
        {
            objToCrush.inBowl = false;
            objInBowl = false;
        }
    }
}
