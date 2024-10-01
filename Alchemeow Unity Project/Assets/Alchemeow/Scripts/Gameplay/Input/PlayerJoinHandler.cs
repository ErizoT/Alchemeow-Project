using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerJoinHandler : MonoBehaviour
{
    [SerializeField] Transform playerOneSpawn;
    [SerializeField] Transform playerTwoSpawn;
    [SerializeField] CinemachineTargetGroup group;

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
            DialogueArray.Instance.StartNextDialogue();

        } else
        {
            playerInput.transform.position = playerTwoSpawn.position;
        }
    }
}
