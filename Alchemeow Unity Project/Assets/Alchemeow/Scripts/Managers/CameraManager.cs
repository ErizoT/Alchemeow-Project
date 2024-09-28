using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    private static CameraManager _instance;
    public static CameraManager Instance => _instance;

   [SerializeField] private  CinemachineStateDrivenCamera cinemachineStateCamera;

    public string currentCameraState;
    public Animator stateAnimator;

    private void Awake()
    {
        // Singleton to make sure this is the only camera manager in the scene
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Call upon this function from other scripts
    // Pass in a string to get to the certain camera angle
    public void ChangeCameraState(string CameraStateName)
    {
        stateAnimator.Play(CameraStateName);
        currentCameraState = CameraStateName;
    }

    public IEnumerator WrongIngredient()
    {
        Debug.Log("Called the thing");
        ChangeCameraState("Cauldron");
        yield return new WaitForSeconds(2);  // Example delay of 2 seconds
        ChangeCameraState("Player");
    }

    public IEnumerator CorrectIngredient()
    {
        Debug.Log("Called the thing");
        ChangeCameraState("Cauldron");
        yield return new WaitForSeconds(2);  // Example delay of 2 seconds
        ChangeCameraState("CrystalBall");
        DialogueArray.Instance.StartNextDialogue();// Call dialogue to play
        yield return new WaitForSeconds(1);

        //ChangeCameraState("Player");
    }

    public IEnumerator BackToPlayer()
    {
        ChangeCameraState("Player");
        yield return null;
    }
}
