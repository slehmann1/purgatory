using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
[RequireComponent(typeof(CircleCollider2D))]
public class explosionForce : MonoBehaviour
{
	public Vector2 forceToApplyAsAResultOfTheFire;
    [Tooltip("whether or not it will explode when another explsion applies a force on it")]
    public bool explodeWithExplosion=true;
    [Tooltip("the delay before it explodes from another explosion")]
    public float delayOfExplosionTransfer=0.5f;
    [Tooltip("The relative velocity required for it to explode")]
    private bool alreadyExploded = false;
	public ParticleSystem[] particles;
    public bool multipleExplosions;
    public float velocityForExplosion;
    public float range;
    private float slope;
    public float maxForce;
    private List<Rigidbody2D> currentlyColliding;
    public bool hasExploded()
    {
        return alreadyExploded;
    }
	public void Update(){
		if (hasExploded()) {
			rigidbody2D.AddRelativeForce(forceToApplyAsAResultOfTheFire);
				}
		}
    public void Explode()
    {
				if (!(alreadyExploded && !multipleExplosions)) {
						foreach (Rigidbody2D rigBody in currentlyColliding) {
								addForce (rigBody);
						}
						alreadyExploded = true;
						foreach (ParticleSystem particle in particles) {
								particle.enableEmission=true;
						}

				}
		}
    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.relativeVelocity.sqrMagnitude > velocityForExplosion)
        {
            Explode();
        }

    }
    private float getAngle(Vector2 point1, Vector2 point2)
    {
        float ang = Mathf.Atan(Mathf.Abs((point1.y - point2.y) / (point1.x - point2.x)));
        if (float.IsNaN(ang))
        {
            throw new Exception("The difference between the points to connect is too low. (floating point precision issue)");
        }
        if (point1.x > point2.x)
        {
            if (point1.y > point2.y)
            {
                //quad 1
            }
            else
            {
                //quad 4
                ang = (Mathf.PI) - ang + (Mathf.PI);
            }
        }
        else
        {
            if (point1.y > point2.y)
            {
                //quad 2
                ang = (Mathf.PI / 2) - ang + (Mathf.PI / 2);
            }
            else
            {
                //quad 3
                ang = Mathf.PI + ang;
            }
        }
        return ang;
    } 
    IEnumerator exp(explosionForce e)
    {
        yield return new WaitForSeconds(e.delayOfExplosionTransfer);
        if (!e.hasExploded())
        {
            e.Explode();
            Debug.Log("BOOM");
        }
    }
    private void addForce(Rigidbody2D rigBody)
    {
		RaycastHit2D rh = Physics2D.Linecast (transform.position, rigBody.transform.position);
		float force = (addForce (rh.distance));
        float angle = getAngle(transform.position,rigBody.transform.position);
        Vector2 forceVector = new Vector2(-Mathf.Cos(angle),-Mathf.Sin(angle));
        forceVector *= force;
		Debug.DrawRay (rigBody.transform.position,forceVector.normalized,Color.cyan,5f,false);
		rigBody.AddForceAtPosition (forceVector,rh.point);
        if(rigBody.GetComponent<Snappable>()){
            if (rigBody.GetComponent<Snappable>().snapOnExplode)
            {
                rigBody.GetComponent<Snappable>().destroy();
            }
        }
        if (rigBody.GetComponent<explosionForce>())
        {
            explosionForce e = rigBody.GetComponent<explosionForce>();
            if (e.explodeWithExplosion)
            {
                StartCoroutine(exp(e));
                
            }
        }
    }
    void Start()
    {
		forceToApplyAsAResultOfTheFire *= rigidbody2D.mass;
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
    float addForce(float distance)
    {
		//f(x)=(b/(log(-(0-a+1))))(log(-(x-(a+1)))) where a is the x intercept and b is the y intercept. 
		//thus a is the maximum range and b is the maximum force
		//it is 0 in the first section because that is the value of x at the y intercept
		//x is the distance

		return  maxForce / (Mathf.Log10 (-(-range + 1)))*Mathf.Log10 (-(distance-(range)));
        //return distance * slope + maxForce;//f(x) = (-maxforce/maxrange)(x)+maxforce, (-maxforce/maxrange) = slope
    }
}
