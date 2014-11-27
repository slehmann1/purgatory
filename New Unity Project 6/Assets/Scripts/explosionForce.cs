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
   
        foreach(Rigidbody2D rigBody in currentlyColliding){
            rigBody.AddForce(getForce(rigBody));
        }
    }
    private Vector2 getForce(Rigidbody2D rigBody)
    {
		float force = (getForce (Vector2.Distance (transform.position, rigBody.transform.position)));
		float angle = Vector2.Angle (transform.position,rigBody.transform.position);
		float xForce =Mathf.Cos (angle) * force;
		float yForce =Mathf.Sin (angle)*force;
		Debug.DrawRay (rigBody.transform.position,new Vector3(xForce,yForce,0),Color.cyan,5f,false);
		Debug.Log (new Vector2(xForce,yForce)+"|"+angle);
		return new Vector2(xForce,yForce).normalized;

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
		//f(x)=(b/(log(-(0-a+1))))(log(-(x-(a+1)))) where a is the x intercept and b is the y intercept. 
		//thus a is the maximum range and b is the maximum force
		//it is 0 in the first section because that is the value of x at the y intercept
		//x is the distance

		return  maxForce / (Mathf.Log10 (-(-range + 1)))*Mathf.Log10 (-(distance-(range)));
        //return distance * slope + maxForce;//f(x) = (-maxforce/maxrange)(x)+maxforce, (-maxforce/maxrange) = slope
    }
}
