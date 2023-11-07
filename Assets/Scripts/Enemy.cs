using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private OVRCameraRig cameraRig;
    private NavMeshAgent nav;
    public float maxRaycastDistance = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        cameraRig = FindObjectOfType<OVRCameraRig>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cameraRig.centerEyeAnchor.position, cameraRig.centerEyeAnchor.forward);
        RaycastHit hit;

        // Verifica si el rayo intersecta con el prefab
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (hit.collider.gameObject.CompareTag("AnkleGrabber")) // Reemplaza "PrefabTag" con el tag del prefab que quieres detectar
            {
                nav.destination = cameraRig.centerEyeAnchor.position;
            }
        }
    }
}
