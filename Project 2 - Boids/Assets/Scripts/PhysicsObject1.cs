using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector3 position;
    private Vector3 direction;
    public Vector3 velocity;
    public Vector3 acceleration;


    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    [SerializeField]
    float mass = 1f;

    public float maxSpeed;

    [SerializeField] SpriteRenderer spriteRenderer;

    //things that might go away
    public bool useFriction, useGravity;

    [SerializeField]
    float frictionCoeef = 0;


    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        radius = spriteRenderer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (useFriction)
        {
            ApplyFriction(frictionCoeef);
        }

        if (useGravity)
        {
            ApplyGravity(Vector3.down * 9.8f);
        }
        /*
        else
        {
            velocity = Vector3.zero;
        }
        */


        //Calculate the velocity for this fram 
        velocity += acceleration * Time.deltaTime;

        //limit how much velocity an object can observe each frame
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);


        position += velocity * Time.deltaTime;

        //Grab current direction from velocity
        direction = velocity.normalized;
  

        
        if (velocity != Vector3.zero)
        {
            Quaternion spriteRotation = Quaternion.LookRotation(Vector3.back, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, spriteRotation, 360f * Time.deltaTime);
        }
        


        //Bounce();

        transform.position = position;

        //Zero out acceleration
        acceleration = Vector3.zero;

    }

    //Force Methods
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }


    //coeff must be above 0 or else it will just cause the object to accelerate 
    public void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();

        friction = friction * coeff;

        ApplyForce(friction);
    }

    public void ApplyGravity(Vector3 gravForce)
    {
        acceleration += gravForce;
    }

    void Bounce()
    {
        float totalCamHeight = (Camera.main.orthographicSize * 2f) / 2;
        float totalCamWidth = (totalCamHeight * Camera.main.aspect);

        if (position.y >= totalCamHeight) 
        {
            velocity.y *= -1;
            //acceleration.y *= -1;
        }
        if(position.y <= (totalCamHeight * -1)){
            velocity.y *= -1;
            //acceleration.y *= -1;
        }
        if (position.x >= totalCamWidth)
        {
            velocity.x *= -1;
            //acceleration.x *= -1;
        }
        if (position.x <= (totalCamWidth * -1))
        {
            velocity.x *= -1;
            //acceleration.x *= -1;
            //position.x = 
        }
    }
}
