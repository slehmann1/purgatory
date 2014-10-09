using UnityEngine;
using System.Collections.Generic;
public class CarBehaviour : MonoBehaviour
{
		public float maxSpeed, force;
		public GameObject frontWheel, rearWheel, body;
		private bool speedChanging = false;
		private float amountOfTime, amountOfTimeUsed, targetSpeed;
		private WheelJoint2D WjFrontWheel, WjRearWheel;
		private Rigidbody2D RigidFrontWheel, RigidRearWheel;
		private Vector2 startPoint, endPoint;
		// Use this for initialization
		void Start ()
		{
				RigidFrontWheel = frontWheel.GetComponent<Rigidbody2D> ();
				RigidRearWheel = rearWheel.GetComponent<Rigidbody2D> ();
				WjFrontWheel = RigidFrontWheel.GetComponent<WheelJoint2D> ();
				WjRearWheel = RigidRearWheel.GetComponent<WheelJoint2D> ();
		}
		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "carSpeedChanger") {
                    Debug.Log("SPEED CHANGE");
						if (!speedChanging) {
								targetSpeed = other.GetComponent<CarDecelerator> ().getEndSpeed ();
                                Debug.Log("SPEED CHANGE");
								if (other.GetComponent<CarDecelerator> ().getEndPoint () != null) {
										GameObject endingPoint = other.GetComponent<CarDecelerator> ().getEndPoint ();
										//d=(vf+vi/2)t
										//t=2d/(vf+vi)
										float distance = Mathf.Abs ((endingPoint.transform.position.x) - (transform.position.x)) - renderer.bounds.size.x / 2;
										amountOfTime = (distance) / (Mathf.Abs (targetSpeed) + Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x));
										Debug.Log (amountOfTime);
								} else {
										amountOfTime = other.GetComponent<CarDecelerator> ().getAmountOfTime ();
								}
								//a=v/t
								//f=ma
								//f=mv/t
								//	changingForce = GetComponent<Rigidbody2D>().mass*(GetComponent<Rigidbody2D>().velocity.x-finalSpeed)/amountOfTime;
								speedChanging = true;
								amountOfTimeUsed = 0;
								startPoint = GetComponent<Rigidbody2D> ().velocity;
								endPoint = new Vector2 (targetSpeed, GetComponent<Rigidbody2D> ().velocity.y);
								//	Debug.Log(changingForce);
						}
				}else if(other.tag== "carDestroyer"){
			Destroy(gameObject);
		}
		}
		public void setSpeed (float f)
		{
				maxSpeed = f;
		}
		public void setForce (float f)
		{
				force = f;
		}
		// Update is called once per frame
		void LateUpdate ()
		{
				if (speedChanging) {
						if (amountOfTimeUsed < amountOfTime) {
								float fracJouney = amountOfTimeUsed / amountOfTime;
								Vector2.Lerp (startPoint, endPoint, fracJouney);
								//Debug.Log("fracJourney"+fracJouney+"time"+amountOfTime+"used"+amountOfTimeUsed);
								amountOfTimeUsed += Time.deltaTime;
						} else {
								maxSpeed = targetSpeed;
								speedChanging = false;
						}
				} else {
						if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) >= Mathf.Abs (maxSpeed)) {
								GetComponent<Rigidbody2D> ().velocity = new Vector2 (maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
						} else {
								GetComponent<Rigidbody2D> ().AddForce (new Vector2 (force, 0));
						}
				}
				JointSuspension2D Suspension;
				Suspension = WjFrontWheel.suspension;
				Suspension .angle = 90F - RigidFrontWheel .transform .eulerAngles .z + body .transform .eulerAngles .z;
				WjFrontWheel .suspension = Suspension;
				Suspension = WjRearWheel .suspension;
				Suspension .angle = 90F - RigidRearWheel .transform .eulerAngles .z + body .transform .eulerAngles .z;
				WjRearWheel .suspension = Suspension;
		}
}
