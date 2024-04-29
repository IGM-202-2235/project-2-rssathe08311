using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shades : Agent
{
    [SerializeField] float seperateWeight = 1f;
    [SerializeField] float wanderRadius = 1f;
    [SerializeField] float range = 3f;
    [SerializeField] float obstacleWeight = 1f;
    [SerializeField] float avoidTime = 2f;
    [SerializeField] float alignWeight = 1.5f;
    [SerializeField] float cohesionWeight = 1f;
    [SerializeField] float seekWeight = 1f;
    [SerializeField] float fleeWeight = 1f;

    [SerializeField] float boundsWeight = 2f;


    protected override Vector3 CalculateSteeringForces()
    {
        Vector3 totalForce = Vector3.zero;

        totalForce += Seperate(agentManager.shades) * seperateWeight;
        totalForce += Align(agentManager.shades) * alignWeight; 
        totalForce += Cohesion(agentManager.shades) * cohesionWeight;

        foreach (Agent human in agentManager.humans)
        {
            if ((Vector3.Distance(transform.position, human.transform.position) + physicsObject.radius) < 5f)
            {
                totalForce += Flee(human) * fleeWeight;
            }
        }

        totalForce += Seek(agentManager.aggressor) * seekWeight;

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
