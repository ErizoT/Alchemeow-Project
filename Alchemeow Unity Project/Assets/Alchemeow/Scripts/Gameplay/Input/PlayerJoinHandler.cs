using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using FMODUnity;

public class PlayerJoinHandler : MonoBehaviour
{
    [SerializeField] Transform playerOneSpawn;
    [SerializeField] Transform playerTwoSpawn;
    [SerializeField] GameObject playerOneText;
    [SerializeField] GameObject playerTwoText;
    [SerializeField] CinemachineTargetGroup group;
    [SerializeField] Material playerTwoMat;

    //joining sounds and snapshot changes (mutes and unmutes SFX)
    [SerializeField] private EventReference joiningSound;
    [SerializeField] private EventReference waitingforplayerone;
    [SerializeField] private EventReference waitingforplayertwo;
    [SerializeField] private FMOD.Studio.EventInstance waitingforplayeroneINSTANCE;
    [SerializeField] private FMOD.Studio.EventInstance waitingforplayertwoINSTANCE;
    [SerializeField] private EventReference startingSnapshot;
    [SerializeField] private EventReference maingameSnapshot;
    private FMOD.Studio.EventInstance startingsnapshotInstance;
    private FMOD.Studio.EventInstance maingamesnapshotInstance;


    private PlayerInput playerOne;

    private void Start()
    {
        //loads audio snapshots
        startingsnapshotInstance = FMODUnity.RuntimeManager.CreateInstance(startingSnapshot);
        maingamesnapshotInstance = FMODUnity.RuntimeManager.CreateInstance(maingameSnapshot);
        waitingforplayeroneINSTANCE = FMODUnity.RuntimeManager.CreateInstance(waitingforplayerone);
        waitingforplayertwoINSTANCE = FMODUnity.RuntimeManager.CreateInstance(waitingforplayertwo);
        startingsnapshotInstance.start();
        
        StartCoroutine(PlayDelayedSounds());

    }

    private IEnumerator PlayDelayedSounds()
    {
        // Wait 
        yield return new WaitForSeconds(0.8f);

        // Start the remaining instances
        waitingforplayeroneINSTANCE.start();
        waitingforplayertwoINSTANCE.start();
    }


    // This method is called whenever a player joins
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log(playerInput.ToString() + "has joined");

        //play sound
        FMODUnity.RuntimeManager.PlayOneShot(joiningSound);
        waitingforplayeroneINSTANCE.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);


        // Get the transform of the new player
        Transform playerTransform = playerInput.transform;

        group.AddMember(playerTransform, 1, 6);

        if (PlayerInput.all.Count == 1)
        {
            playerInput.transform.position = playerOneSpawn.position;
            playerInput.DeactivateInput();
            playerOne = playerInput;
            playerOneText.SetActive(false);
        } else
        {
            //audio
            waitingforplayertwoINSTANCE.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            maingamesnapshotInstance.start();

            playerInput.transform.position = playerTwoSpawn.position;
            playerInput.gameObject.GetComponent<PawController>().InitialisePlayerTwo(playerTwoMat);
            DialogueArray.Instance.StartNextDialogue();
            playerOne.ActivateInput();
            CameraManager.Instance.ChangeCameraState("CrystalBall");
            playerTwoText.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(playerOneSpawn.position, .5f);
        Gizmos.DrawSphere(playerTwoSpawn.position, .5f);
    }
}
