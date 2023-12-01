using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Platform;

public class ExitDoorTrigger : MonoBehaviour
{
    private OVRPlayerController playerController;
    public GameObject WinUI;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");

            this.GetComponent<BoxCollider>().enabled = false;
            playerController = FindObjectOfType<OVRPlayerController>();

            if (playerController != null)
            {
                CharacterController characterController = playerController.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    characterController.enabled = false;
                    Debug.Log("CharacterController disabled!");
                }
            }
            else
            {
                Debug.LogError("OVRPlayerController not found in the scene!");
            }

            WinUI.SetActive(true);
        }
    }

}
