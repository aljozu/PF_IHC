using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    public float followDistance = 10f; // Distance within which the enemy will follow the player
    public Light enemyLight; // Reference to the Light component

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Ensure the enemyLight is assigned
        if (enemyLight == null)
        {
            Debug.LogError("Enemy Light not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Calculate the direction vector from the enemy to the player
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= 4f)
        {
            // Trigger vibration on both controllers
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);

            // Stop the vibration after a short delay
            Invoke("StopVibration", 0.5f);
        }

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

        // Check if the enemy is touched by the light
        if (enemyLight.enabled && distance <= enemyLight.range)
        {
            // Destroy the enemy GameObject
            Destroy(gameObject);
        }
    }

    void StopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}

