using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans : Agent
{
    [SerializeField] float wanderTime = 1f;
    [SerializeField] float wanderRadius = 1f;
    [SerializeField] float alignWeight = 1.5f;
    [SerializeField] float range = 3f;
    [SerializeField] float seperateWeight = 1f;
    [SerializeField] float cohesionWeight = 1f;
    [SerializeField] float evadeWeight = 1f;
    [SerializeField] float pursueWeight = 1f;

    [SerializeField] float boundsWeight = 2f;


    protected override Vector3 CalculateSteeringForces()
    {
        Vector3 wanderForce = Vector3.zero;


        wanderForce += Wander(wanderTime, wanderRadius, range);
        wanderForce += Evade(agentManager.aggressor) * evadeWeight;


        foreach(Agent shade in agentManager.shades)
        {
            if((Vector3.Distance(transform.position, shade.transform.position) + physicsObject.radius) < 5f)
            {
                wanderForce += Pursue(shade);
            }
        }

        wanderForce += Align(agentManager.humans) * alignWeight;
        wanderForce += Seperate() * seperateWeight;
        wanderForce += Cohesion(agentManager.humans) * cohesionWeight;
        wanderForce += StayInBounds() * boundsWeight;

        return wanderForce;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Vector3 futurePosition = CalcFuturePosition(wanderTime);
        Gizmos.DrawWireSphere(futurePosition, wanderRadius);

        float randAngle = Random.Range(0f, Mathf.PI * 2f);

        Gizmos.color = Color.red;
        Vector3 wanderPoint = futurePosition;
        wanderPoint.x += Mathf.Cos(randAngle) * wanderRadius;
        wanderPoint.y += Mathf.Sin(randAngle) * wanderRadius;
        Gizmos.DrawLine(transform.position, wanderPoint);

        

        //not working rn
        Gizmos.color = Color.cyan;
        Vector3 pursueObject = Vector3.zero;
        foreach (Agent shade in agentManager.shades)
        {
            if ((Vector3.Distance(transform.position, shade.transform.position) + physicsObject.radius) < 5f)
            {
                pursueObject = Pursue(shade) * pursueWeight;
                Gizmos.DrawLine(transform.position, pursueObject);
            }
        }

        
    }
}
