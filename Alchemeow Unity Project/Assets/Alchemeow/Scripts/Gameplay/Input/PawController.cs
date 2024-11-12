using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using FMODUnity;
using UnityEngine.UI;

public class PawController : MonoBehaviour
{
    public static bool isPaused;

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
    [SerializeField] SkinnedMeshRenderer pawToChange;
    [SerializeField] ParticleSystem meowParticles;

    [Header("Pause Menu Varialbles")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button pauseButton;
    //[SerializeField] Material playerTwoMat;

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
    [SerializeField] private StudioEventEmitter grabsuccessEmitter;
    [SerializeField] private StudioEventEmitter grabfailEmitter;
    [SerializeField] private StudioEventEmitter grabreleaseEmitter;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(visuals.transform.position, nearestObject.transform.position);
    }

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
                   }

    private void Update()
    {
        // Player Movement
        Vector3 playerVel = new Vector3(moveVector.x, raiseValue, moveVector.y) * moveSpeed;
        rb.AddForce(playerVel*Time.deltaTime, ForceMode.Acceleration);

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

        if(Vector3.Distance(transform.position, nearestObject.transform.position) > grabRange)
        {
            LetGo();
        }
    }

    private void FixedUpdate()
    {
        if (grabbed)
        {
            transform.position = nearestObject.transform.position;
            transform.rotation = nearestObject.transform.rotation;
            hinge.connectedBody = nearestObject.GetComponentInParent<Rigidbody>();
            animator.SetBool("Holding", true);
        }

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

                if(grabbed)
                {
                    // Play release sound
                       grabreleaseEmitter.Play();

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
          
            //successful grab sound
            grabsuccessEmitter.Play();

            grabbed = true;
            paw.layer = LayerMask.NameToLayer("GhostPlayer");
            objectHeld = nearestObject;
            objectHeld.GetComponent<GripPoint>().Gripped(this);

            
                
            }
           else
        {
            //unsuccessful grab sound
            grabfailEmitter.Play();
            
        }
    }

    public void LetGo()
    {
        // Happens when you do a successful grab and let go
        
        grabbed = false;
        paw.layer = LayerMask.NameToLayer("Player");
        objectHeld.layer = LayerMask.NameToLayer("Default");
        objectHeld.GetComponent<GripPoint>().Ungripped();
        objectHeld = null;
        
        
    }

    public void RaiseLower(InputAction.CallbackContext input)
    {
        raiseValue = input.ReadValue<float>() * raiseLowerSpeed;
    }

    public void AdvanceText(InputAction.CallbackContext input)
    {
        if(input.performed)
        {
            DialogueArray.Instance.NextLine();
        }

    }

    public void Meow(InputAction.CallbackContext input)
    {
        if(input.started)
        {
            // Play a MEOW SOUND at varying pitches!!
            // Somethin like...
            // audioSource.pitch = RandomRange(0.8, 1.2);
            // audioSource.Play();

            meowParticles.Play();
        }

    }

    public void InputPause(InputAction.CallbackContext input)
    {
        if (input.started)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
        else if (isPaused)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
    }

    public void InitialisePlayerTwo(Material matToChangeTo)
    {
        pawToChange.material = matToChangeTo;
    }

    public void ResetScene()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the scene
        SceneManager.LoadScene(currentScene.name);
    }

    public void ExitScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScreen");
    }
}
