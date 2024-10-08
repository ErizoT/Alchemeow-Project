using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngData : MonoBehaviour
{
    [Tooltip("Name that the cauldron will check when placed inside.")]
    [SerializeField] string ingredientID;
    [Tooltip("Whether the object will respawn or not")]
    public bool canRespawn;

    [HideInInspector] public Ingredient ingClass;
    [HideInInspector] public bool hasRespawned = false;
    
    private Vector3 respawnPoint;
    private Quaternion respawnRotation;
    private GameObject prefabToRespawn;
    private Rigidbody rb;

    private void Start()
    {
        ingClass = new Ingredient(ingredientID);
        rb = GetComponent<Rigidbody>();

        respawnPoint = transform.position;
        respawnRotation = transform.rotation;
        prefabToRespawn = gameObject;
    }

    public void Respawn()
    {
        if (canRespawn)
        {
            //Instantiate(prefabToRespawn, respawnPoint, respawnRotation);
            transform.position = respawnPoint;
            transform.rotation = respawnRotation;

            // Reset the velocity of this Rigidbody
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero; // Reset angular velocity if needed
            }

            // Reset the velocities of all child Rigidbodies
            Rigidbody[] childRigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody childRb in childRigidbodies)
            {
                childRb.velocity = Vector3.zero;
                childRb.angularVelocity = Vector3.zero; // Reset angular velocity for children as well
            }

        }

    }
}
