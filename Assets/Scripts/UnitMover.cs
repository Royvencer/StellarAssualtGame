using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMover : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private bool move = false;

    public float defaultSpeed = 3;
    private float speed;
    public float rotationSpeed = 400;
    public bool closeToTarget = false;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {
        if (move)
        {
            if (targetPosition != Vector3.zero)
            {
                // Calculate the direction to the target
                Vector3 direction = (targetPosition - transform.position).normalized;

                // Create the rotation we need to be in to look at the target
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                // Calculate the angle between current rotation and target rotation
                float angle = Quaternion.Angle(transform.rotation, lookRotation);

                // Calculate time to complete the rotation
                float timeToComplete = angle / rotationSpeed;

                // Calculate percentage of rotation completed in this frame
                float percentageDone = Mathf.Min(1F, Time.deltaTime / timeToComplete);

                // Rotate towards the target direction
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, percentageDone);

                // Move towards the target position using NavMeshAgent
                agent.SetDestination(targetPosition);

                // Check if the unit is close to the target position
                if (Vector3.Distance(transform.position, targetPosition) < agent.stoppingDistance)
                {
                    // Stop moving once the unit reaches close to the target
                    move = false;
                }
            }
        }
    }

    public void MoveTo(Vector3 newPos)
    {
        targetPosition = newPos;
        move = true;
    }
}
