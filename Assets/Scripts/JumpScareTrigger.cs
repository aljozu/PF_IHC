using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareTrigger : MonoBehaviour
{
    public Animation JumpScareAnimation;
    public AudioSource JumpScareAudio;
    private OVRCameraRig cameraRig;
    public float maxRaycastDistance = 10f;

    void Start()
    {
        // Encuentra el OVRCameraRig en la escena
        cameraRig = FindObjectOfType<OVRCameraRig>(); 
    }

    void Update()
    {
        // Lanza un rayo desde el centro de la cámara hacia adelante
        Ray ray = new Ray(cameraRig.centerEyeAnchor.position, cameraRig.centerEyeAnchor.forward);
        RaycastHit hit;

        // Verifica si el rayo intersecta con el prefab
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.CompareTag("JumpScare001")) // Reemplaza "PrefabTag" con el tag del prefab que quieres detectar
            {
                JumpScareAnimation.Play();
                if(JumpScareAudio != null)
                {
                    JumpScareAudio.Play();
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}
