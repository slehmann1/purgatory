using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(CircleCollider2D))]
public class explosionForce : MonoBehaviour
{

    public float range;
    private float slope;
    public float maxForce;
    private List<Rigidbody2D> currentlyColliding;

    public void Explode()
    {
        Debug.Log("RUN");
        foreach(Rigidbody2D rigBody in currentlyColliding){
            rigBody.AddForce(getForce(rigBody));
        }
    }
    private Vector2 getForce(Rigidbody2D rigBody)
    {
        float xForce = getForce(rigBody.transform.position.x- transform.position.x);
        float divisor = (rigBody.transform.position.x- transform.position.x)/xForce;//uses similar triangles, could alternatively use trig, but this is likely faster
        return new Vector2(xForce,(getForce(rigBody.transform.position.y- transform.position.y)/divisor));
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Explode();
        }
    }
    void Start()
    {
        currentlyColliding = new List<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.rigidbody2D){
            currentlyColliding.Add(coll.rigidbody2D);
        }
    else
        {
            Debug.LogWarning("Game object: "+coll.name+" does not contain a rigidbody2d to apply an explosive force to.");
            currentlyColliding.Remove(coll.rigidbody2D);
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.rigidbody2D)
        {
            currentlyColliding.Remove(coll.rigidbody2D);
        }
    }
    void getSlope()
    {
        //f(x) = (-maxforce/maxrange)(x)+maxforce
        //where f(x) is the force at a certain distance or x value
        //this is  a linear function where the maxforce is at x = 0, and at x = max range f(x)=0
        //thus maxforce is the yintercept and maxrange is the x intercept
        slope = -maxForce / range;//slope = rise over run
    }
    float getForce(float distance)
    {
        return distance * slope + maxForce;//f(x) = (-maxforce/maxrange)(x)+maxforce, (-maxforce/maxrange) = slope
    }
}
