using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarFunctionality : MonoBehaviour
{
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Animator animator;

    [HideInInspector] public MortarMovement mortarMovement;
    private GripHandler gP;
    private bool isGrounded;
    private bool objInBowl;
    private bool animTriggered;
    private bool isFrozen;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, .1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (-transform.up * groundCheckDistance));
    }

    private void Start()
    {
        gP = GetComponentInParent<GripHandler>();
        mortarMovement = GetComponentInParent<MortarMovement>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, layerMask);
        
        if(isGrounded && objInBowl)
        {
            //mortarMovement.canMove = true;

            if(!animTriggered && !isFrozen)
            {
                animTriggered = true;
                animator.SetTrigger("Shocked");
                animator.SetBool("Walking", true);
                StartCoroutine(StartWalking());
            }
        }
        else
        {
            mortarMovement.canMove = false;

            if (animTriggered && !isFrozen)
            {
                animTriggered = false;
                StartCoroutine(StopWalking());
            }
        }

        
        if (gP.isHeld && !isFrozen)
        {
            isFrozen = true;
            animator.SetTrigger("Shocked");
            animator.SetBool("Grabbed", true);
        }
        else if (!gP.isHeld && isFrozen)
        {
            isFrozen = false;
            animator.SetTrigger("Shocked");
            animator.SetBool("Grabbed", false);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Crushable>(out Crushable objToCrush))
        {
            objToCrush.inBowl = true;
            objInBowl = true;
        }
        else
        {
            objInBowl = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Crushable>(out Crushable objToCrush))
        {
            objToCrush.inBowl = false;
            objInBowl = false;
        }
    }
    
    IEnumerator StartWalking()
    {
        yield return new WaitForSeconds(.8f);
        mortarMovement.canMove = true;
    }

    IEnumerator StopWalking()
    {
        animator.SetTrigger("Shocked");
        animator.SetBool("Walking", false);
        return null;
    }
}
