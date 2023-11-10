using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    public float followDistance = 10f; // Distance within which the enemy will follow the player

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Calculate the direction vector from the enemy to the player
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // If the player is within the follow distance, the enemy will follow the player
        if (distance < followDistance)
        {
            // Set the NavMeshAgent's destination to the player's position
            navMeshAgent.SetDestination(player.position);
            // Resume the NavMeshAgent
            navMeshAgent.isStopped = false;
        }
        else
        {
            // Stop the NavMeshAgent
            navMeshAgent.isStopped = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.transform == player)
        {
            // Trigger vibration on both controllers
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);

            // Stop the vibration after a short delay
            Invoke("StopVibration", 0.5f);
        }
    }

    void StopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
