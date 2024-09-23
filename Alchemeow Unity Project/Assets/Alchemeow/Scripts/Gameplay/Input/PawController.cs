using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using FMODUnity;

public class PawController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject paw;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float raiseLowerSpeed = 10f;
    [SerializeField] float grabRange = 3f;
    [SerializeField] Animator animator;
    [SerializeField] GameObject visuals;
    [SerializeField] Rigidbody dummyRigidbody;
    [Range(.5f, 1f)]
    [SerializeField] float damping = 0.7f;

    public bool isHolding;
    private bool grabbed;
    private Vector2 moveVector;
    private Vector2 rotateVector;
    private float raiseValue;
    private Rigidbody rb;
    private ConfigurableJoint hinge;
    private Quaternion defaultRotation;
    private GameObject nearestObject;
    private GameObject objectHeld;
    private Camera mainCamera;

    //sound for grabbing
    [SerializeField] private StudioEventEmitter grabEmitter;
    private bool playedPop = false;


    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        defaultRotation = transform.rotation;
        hinge = GetComponent<ConfigurableJoint>();

        // Finding main camera and adding this object to it
        mainCamera = Camera.main;
        GameObject mainCamObject = mainCamera.gameObject;
        MultipleTargetCamera camScript = mainCamObject.GetComponent<MultipleTargetCamera>();
        camScript.targets.Add(transform);

      
        grabEmitter.Play();
        grabEmitter.SetParameter("GrabState", 3);



    }

    private void Update()
    {
        // Player Movement
        Vector3 playerVel = new Vector3(moveVector.x, raiseValue, moveVector.y) * moveSpeed;
        rb.AddForce(playerVel * Time.deltaTime, ForceMode.Acceleration);

        // Player Rotation
        if (!grabbed)
        {
            animator.SetBool("Holding", false);
            hinge.connectedBody = dummyRigidbody;

            //Find all objects with tag "Holdable"
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Holdable");

            if (objects.Length == 0)
                return;

            // Find the nearest object
            nearestObject = null;
            float minDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (GameObject obj in objects)
            {
                float distance = Vector3.Distance(currentPosition, obj.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestObject = obj;
                }
            }

            // If a nearest object is found, make this object look at it
            if (nearestObject != null && minDistance <= grabRange)
            {
                transform.LookAt(nearestObject.transform);
                transform.Rotate(0, 90, -90);
            }
            else
            {
                transform.rotation = defaultRotation;
            }
        }

        if (grabbed)
        {
            transform.position = nearestObject.transform.position;
            hinge.connectedBody = nearestObject.GetComponentInParent<Rigidbody>();
            visuals.transform.rotation = nearestObject.transform.rotation;
            visuals.transform.position = nearestObject.transform.position;
            animator.SetBool("Holding", true);
        }
    }

    // Callum request:
    // Play a sound when you let go of an object, not JUST the air.
    // - Detect when it is holding an object
    // - Detect when you let go of said object

    private void FixedUpdate()
    {
        // Velocity Damping
        Vector3 tempV = rb.velocity;
        tempV = Vector3.Scale(tempV, new Vector3(damping, damping, damping));
        tempV = new Vector3(tempV.x, tempV.y, tempV.z);
        rb.velocity = tempV;
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        moveVector = input.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext input)
    {
        rotateVector = input.ReadValue<Vector2>();
        Debug.Log(rotateVector);
    }

    public void OnGrab(InputAction.CallbackContext input)
    {
        // DETECTING A SUCCESSFUL GRAB VS NON-SUCCESSFUL GRAB
        // - isHolding will turn true upon the player pressing the grab button, and false when released
        // - When isHolding = true, it will do a GrabCheck(), which will check if the grip point is under the grab range threshold
        // - If it's in the correct range, grabbed will be set to true. Otherwise, it will be an unsuccessful grab
        // - Upon a successful grab and is released, LetGo() will be called

        if (input.performed)
        {
            if (isHolding)
            {
                isHolding = false;

                if (grabbed)
                {
                    LetGo();
                }

            }
            else
            {
                isHolding = true;
                animator.SetTrigger("Grab");
                GrabCheck();
            }
        }
    }


    void GrabCheck()
    {
        // If the distance between the nearest grip point and the paw is within the range threshold...
        // set grabbed to true
        // Set player physics layer to "Ghost" player layer
        // Assign objectHeld and change its physics layer to GhostObject

        if (Vector3.Distance(transform.position, nearestObject.transform.position) <= grabRange)
        {
            grabbed = true;
            paw.layer = LayerMask.NameToLayer("GhostPlayer");

            objectHeld = nearestObject;

            if (playedPop == true)
            {
                grabEmitter.Play();
                grabEmitter.SetParameter("GrabState", 0);
            }
            else
                playedPop = true;


        }
        else
        {
            // Unsuccessful grab

            if (playedPop == true)
            {
                grabEmitter.Play();
                grabEmitter.SetParameter("GrabState", 1);
            }
            else
                playedPop = true;
        }
    }

    void LetGo()
    {
        // Happens when you do a successful grab and let go

        grabbed = false;
        paw.layer = LayerMask.NameToLayer("Player");
        objectHeld.layer = LayerMask.NameToLayer("Default");
        objectHeld = null;
        // Play a sound

        grabEmitter.Play();
        grabEmitter.SetParameter("GrabState", 2);

    }

    public void RaiseLower(InputAction.CallbackContext input)
    {
        raiseValue = input.ReadValue<float>() * raiseLowerSpeed;
    }

    public void ResetScene(InputAction.CallbackContext input)
    {
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the scene
        SceneManager.LoadScene(currentScene.name);
    }
}
