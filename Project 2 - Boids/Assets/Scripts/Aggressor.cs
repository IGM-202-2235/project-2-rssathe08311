using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggressor : Agent
{
    [SerializeField] float seekWeight = 1f;
    [SerializeField] float wanderTime = 1f;
    [SerializeField] float wanderRadius = 1f;
    [SerializeField] float boundsWeight = 2f;


    protected override Vector3 CalculateSteeringForces()
    {
        Vector3 wanderForce = Vector3.zero;


        wanderForce += Seek(agentManager.humans[0]) * seekWeight;
        wanderForce += StayInBounds() * boundsWeight;

        return wanderForce;
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;

        Vector3 futurePosition = CalcFuturePosition(wanderTime);
        Gizmos.DrawWireSphere(futurePosition, wanderRadius);

        float randAngle = Random.Range(0f, Mathf.PI * 2f);

        Gizmos.color = Color.red;
        Vector3 wanderPoint = futurePosition;
        wanderPoint.x += Mathf.Cos(randAngle) * wanderRadius;
        wanderPoint.y += Mathf.Sin(randAngle) * wanderRadius;
        Gizmos.DrawLine(transform.position, wanderPoint);

    }
}
