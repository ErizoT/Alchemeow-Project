using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerJoinHandler : MonoBehaviour
{
    [SerializeField] Transform playerOneSpawn;
    [SerializeField] Transform playerTwoSpawn;
    [SerializeField] GameObject playerOneText;
    [SerializeField] GameObject playerTwoText;
    [SerializeField] CinemachineTargetGroup group;
    [SerializeField] Material playerTwoMat;

    private PlayerInput playerOne;

    // This method is called whenever a player joins
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log(playerInput.ToString() + "has joined");

        
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
