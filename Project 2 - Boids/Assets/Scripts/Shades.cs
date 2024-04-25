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
    [SerializeField] float avoidTime = 2f;

    [SerializeField] float boundsWeight = 2f;


    protected override Vector3 CalculateSteeringForces()
    {
        Vector3 totalForce = Vector3.zero;

        totalForce += Seperate() * seperateWeight;
        totalForce += StayInBounds() * boundsWeight;
        totalForce += AvoidObstacles(avoidTime) * obstacleWeight;
        totalForce += Wander(wanderTime, wanderRadius, range);

        return totalForce;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 futurePos = CalcFuturePosition(avoidTime);

        float dist = Vector3.Distance(transform.position, futurePos) + physicsObject.radius;


        //Quaternion spriteRotation = Quaternion.LookRotation(Vector3.back, physicsObject.Direction);

        Vector3 boxSize = new Vector3(physicsObject.radius * 2f, dist, physicsObject.radius * 2f);
        Vector3 boxCenter = Vector3.zero;
        boxCenter.y += dist / 2f;

        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(boxCenter, boxSize);
        Gizmos.matrix = Matrix4x4.identity;

        Gizmos.color = Color.white;

        foreach (Vector3 pos in foundObstaclePositions)
        {
            Gizmos.DrawLine(transform.position, pos);
        }
    }
}
