using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Agent : MonoBehaviour
{

    [SerializeField] public PhysicsObject physicsObject;
    float randAngle;

    protected List<Vector3> foundObstaclePositions = new List<Vector3>();

    protected Vector3 totalForce = Vector3.zero;
    [SerializeField] float maxForce = 10f;

    public AgentManager agentManager;

    // Start is called before the first frame update
    void Start()
    {
        randAngle = Random.Range(0f, Mathf.PI * 2f);
    }

    // Update is called once per frame
    void Update()
    {
        totalForce += CalculateSteeringForces();

        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);

        physicsObject.ApplyForce(totalForce);

        totalForce = Vector3.zero;

    }

    protected abstract Vector3 CalculateSteeringForces();

    public Vector3 Seek(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - transform.position;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * physicsObject.maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = desiredVelocity - physicsObject.velocity;

        // Return seek steering force
        return seekingForce;

    }

    public Vector3 Seek(Agent target)
    {
        return Seek(target.physicsObject.position);
    }
    public Vector3 Pursue(Agent target)
    {
        return Seek(target.CalcFuturePosition(2f));
    }



    public Vector3 Flee(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = physicsObject.position - targetPos;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * physicsObject.maxSpeed;

        // Calculate seek steering force
        Vector3 fleeingForce = desiredVelocity - physicsObject.velocity;

        // Return seek steering force
        return fleeingForce;
    }

    public Vector3 Flee(Agent target)
    {
        return Flee(target.transform.position);
    }

    //the only difference between evade and flee is target.calcFuturePosition * time
    public Vector3 Evade(Agent target)
    {
        return Flee(target.CalcFuturePosition(2f));
    }

    public Vector3 CalcFuturePosition(float futureTime)
    {
        return physicsObject.velocity * futureTime + transform.position;
        //return transform.position + (physicsObject.Direction * futureTime);
    }

    public Vector3 Wander(float futureTime, float circleRadius, float range)
    {
        Vector3 futurePosition = CalcFuturePosition(futureTime);

        float rad = range * Mathf.Deg2Rad;

        randAngle += Random.Range((rad * -1), rad);

        Vector3 wanderPoint = futurePosition;

        wanderPoint.x += Mathf.Cos(randAngle) * circleRadius;
        wanderPoint.y += Mathf.Sin(randAngle) * circleRadius;
        


        return Seek(wanderPoint);
    }

    public Vector3 StayInBounds()
    {
        Vector3 steeringForce = Vector3.zero;

        Debug.Log((CheckIfInBounds(transform.position)));
        //Do stuff
        if(!CheckIfInBounds(transform.position))
        {
            steeringForce = Seek(Vector3.zero);
        }

        return steeringForce;
    }

    public Vector3 AvoidObstacles(float timeAhead)
    {
        physicsObject.position.z = 0;
        Vector3 steeringForce = Vector3.zero;

        foundObstaclePositions.Clear();

        //vector To obstacle 
        Vector3 vToO = Vector3.zero;

        float forwardDot, rightDot;

        foreach(Obstacle ob in agentManager.obstacles)
        {
            vToO = ob.transform.position - transform.position;
            

            forwardDot = Vector3.Dot(physicsObject.Direction, vToO);
            Vector3 futurePos = CalcFuturePosition(timeAhead);

            float dist = Vector3.Distance(transform.position, futurePos) + physicsObject.radius;

            //Debug.Log(forwardDot);
            Vector3 right = Vector3.Cross(physicsObject.Direction, Vector3.back);
            Debug.Log(right);

            //Vector3 right = Vector3.Cross(physicsObject.Direction, Vector3.back);

            //if the obstacle is in front of an agent 
            if (forwardDot > 0f - ob.radius )
            {
                //if it is within the detection box
                if(forwardDot <= dist + ob.radius)
                {
                    //how far the obstacle is to the right or left of an object
                    rightDot = Vector3.Dot(right, vToO);
                    Debug.Log("rightDot " + rightDot);

                    steeringForce = transform.right * (1 - forwardDot / dist) * physicsObject.maxSpeed;

                    //if the obstacle is within the box of detection
                    if(Mathf.Abs(rightDot) <= physicsObject.radius + ob.radius)
                    {
                        foundObstaclePositions.Add(ob.transform.position);

                        //steer left
                        if(rightDot > 0)
                        {
                            steeringForce = steeringForce * -1;
                        }
                    }
                }

                
            }


            
        }

        return steeringForce;
    }

    protected bool CheckIfInBounds(Vector3 position)
    {
        float totalCamHeight = (Camera.main.orthographicSize * 2f) / 2;
        float totalCamWidth = (totalCamHeight * Camera.main.aspect);
        Vector3 futurePosition = CalcFuturePosition(1f);

        if (futurePosition.y > totalCamHeight || (futurePosition.y < (totalCamHeight * -1)))
        {
            return false;
        }
        if (futurePosition.x > totalCamWidth || (futurePosition.x < (totalCamWidth * -1)))
        {
            return false;
        }
        
        return true;
    }

    public Vector3 Seperate()
    {
        Vector3 separateForce = Vector3.zero;

        foreach(Agent agent in agentManager.shades)
        {
            float dist = Vector3.Distance(transform.position, agent.transform.position);
            if(Mathf.Epsilon < dist)
            {
                separateForce += Flee(agent) * (1f/ dist);
            }
        }
        return separateForce;
    }

    //alignment - seek the average location of the flock
    public Vector3 Align(List<Agent> agents)
    {
        Vector3 alignForce = Vector3.zero;
        Vector3 sum = Vector3.zero;

        foreach(Agent agent in agents)
        {
            sum += agent.physicsObject.velocity;
        }

        sum /= agents.Count;

        sum.Normalize();

        alignForce = Seek(sum);

        return alignForce;
    }

    public Vector3 Cohesion(List<Agent> agents)
    {
        physicsObject.position.z = 0;
        Vector3 cohesionForce = Vector3.zero;
        float neighborDist = 50;
        Vector3 sum = Vector3.zero;
        int count = 0;
        foreach(Agent agent in agents)
        {
            float dist = Vector3.Distance(transform.position, agent.transform.position);
            if((physicsObject != agent.physicsObject) && (dist < neighborDist))
            {
                sum += agent.transform.position;
                count++;
            }

        }

        if(count > 0)
        {
            sum /= count;
            cohesionForce = sum;
        }

        return cohesionForce;
    }
}
