using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] string cameraToSwitch;

    private BoxCollider boxCollider;
    private int playerCount;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCount++;
        }

        if (playerCount == 2)
        {
            CameraManager.Instance.ChangeCameraState(cameraToSwitch);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerCount--;
        }

        if (playerCount == 0)
        {
            CameraManager.Instance.ChangeCameraState("Player");
        }
    }
}
