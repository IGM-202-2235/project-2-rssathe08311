using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shades : Agent
{
    [SerializeField] float seperateWeight = 1f;
    [SerializeField] float wanderTime = 1f;
    [SerializeField] float wanderRadius = 1f;
    [SerializeField] float range = 3f;
    [SerializeField] float obstacleWeight = 1f;

    [SerializeField] float boundsWeight = 2f;



    protected override Vector3 CalculateSteeringForces()
    {
        Vector3 totalForce = Vector3.zero;

        totalForce += Seperate() * seperateWeight;
        totalForce += StayInBounds() * boundsWeight;
        totalForce += AvoidObstacles() * obstacleWeight;
        totalForce += Wander(wanderTime, wanderRadius, range);


        return totalForce;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        foreach(Vector3 obPos in foundObstaclePositions)
        {
            Gizmos.DrawLine(transform.position, obPos);
        }
    }
}
