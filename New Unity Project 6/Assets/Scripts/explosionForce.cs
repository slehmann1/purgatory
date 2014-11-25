using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CircleCollider2D))]
public class explosionForce : MonoBehaviour {

	public float range;
	private float slope;
	public float maxForce;
	// Use this for initialization
	void Start () {
	
	}
	void getSlope(){
		//f(x) = (-maxforce/maxrange)(x)+maxforce
		//where f(x) is the force at a certain distance or x value
		//this is  a linear function where the maxforce is at x = 0, and at x = max range f(x)=0
		//thus maxforce is the yintercept and maxrange is the x intercept
		slope = -maxForce / range;//slope = rise over run
		}
	float getForce(float distance){
		return distance*slope+maxForce;//f(x) = (-maxforce/maxrange)(x)+maxforce, (-maxforce/maxrange) = slope
		}
}
