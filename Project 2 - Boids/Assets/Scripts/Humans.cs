using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans : Agent
{
    [SerializeField] float wanderRadius = 1f;
    [SerializeField] float alignWeight = 1.5f;
    [SerializeField] float range = 3f;
    [SerializeField] float seperateWeight = 1f;
    [SerializeField] float cohesionWeight = 1f;
    [SerializeField] float evadeWeight = 1f;
    [SerializeField] float pursueWeight = 1f;

    [SerializeField] float avoidTime = 2f;
    [SerializeField] float obstacleWeight = 1f;
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
                wanderForce += Pursue(shade) * pursueWeight;
            }
        }


        wanderForce += Align(agentManager.humans) * alignWeight;
        wanderForce += Seperate(agentManager.humans) * seperateWeight;

        wanderForce += Cohesion(agentManager.humans) * cohesionWeight;
        wanderForce += AvoidObstacles(avoidTime) * obstacleWeight;
        wanderForce += StayInBounds() * boundsWeight;
        

        return wanderForce;
    }


    private void OnDrawGizmosSelected()
    {
        /*
        Gizmos.color = Color.black;

        Vector3 futurePosition = CalcFuturePosition(wanderTime);
        Gizmos.DrawWireSphere(futurePosition, wanderRadius);

        float randAngle = Random.Range(0f, Mathf.PI * 2f);

        Gizmos.color = Color.red;
        Vector3 wanderPoint = futurePosition;
        wanderPoint.x += Mathf.Cos(randAngle) * wanderRadius;
        wanderPoint.y += Mathf.Sin(randAngle) * wanderRadius;
        Gizmos.DrawLine(transform.position, wanderPoint);
        */

        Vector3 futurePos = CalcFuturePosition(avoidTime);

        float dist = Vector3.Distance(transform.position, futurePos) + physicsObject.radius;


        //Quaternion spriteRotation = Quaternion.LookRotation(Vector3.back, physicsObject.Direction);

        Vector3 boxSize = new Vector3(physicsObject.radius * 2f, dist, physicsObject.radius * 3f);
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


        
        //not working rn
        Gizmos.color = Color.cyan;
        foreach (Agent shade in agentManager.shades)
        {
            if ((Vector3.Distance(transform.position, shade.transform.position) + physicsObject.radius) < 2f)
            {
                
                Gizmos.DrawLine(transform.position, shade.transform.position);
            }
        }
        


    }
}
