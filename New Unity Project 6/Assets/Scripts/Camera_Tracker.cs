using UnityEngine;
using System.Collections.Generic;
public class Camera_Tracker : MonoBehaviour
{
		public GameObject g ;
		private Transform target;
		public float xthreshold, ythreshold, xtrackSpeed, ytrackSpeed;
		private List <HillSpawner> hillMovement;
		//private GameObject o;
		//private int jumpState=0;
		public void setTarget (Transform t)
		{
				target = t;
		}
		// Set target
		public void Start ()
		{
				target = GameObject.FindGameObjectWithTag ("Player").transform;
				hillMovement = new List<HillSpawner> ();
				//	hillMovement=GameObject.FindGameObjectWithTag("HillSpawners").GetComponent<HillSpawner>();
				GameObject[] hills = GameObject.FindGameObjectsWithTag ("HillSpawners");
				for (int i = 0; i<hills.Length; i++) {
						hillMovement.Add (hills [i].GetComponent<HillSpawner> ());
				}
		}
		int greaterThanZero (float x)
		{
				if (x > 0) {
						return 1;
				}
				if (x == 0) {
						return 0;
				} else {
						return -1;
				}
		}
		// Track target
		void LateUpdate ()
		{
				//Debug.Log(target.name);
				float y = transform.position.y;
				float x = transform.position.x;
				//X
				if (Mathf.Abs (transform.position.x - target.transform.position.x) > xthreshold) {
						if (Mathf.Abs (transform.position.x - target.transform.position.x) < xtrackSpeed) {
								if (transform.position.x - target.transform.position.x > 0) {
										x = transform.position.x - xtrackSpeed;
										for (int i =0; i<hillMovement.Count; i++) {
												hillMovement [i].move (-xtrackSpeed);						
										}
								} else {
										x = transform.position.x + xtrackSpeed;
										for (int i =0; i<hillMovement.Count; i++) {
												hillMovement [i].move (xtrackSpeed);						
										}
								}
						} else {
								if (transform.position.x - target.transform.position.x > 0) {
										x = target.transform.position.x + xthreshold;
										for (int i =0; i<hillMovement.Count; i++) {
												hillMovement [i].move (xtrackSpeed);						
										}
								} else {
										x = target.transform.position.x - xthreshold;
										for (int i =0; i<hillMovement.Count; i++) {
												hillMovement [i].move (-xtrackSpeed);						
										}
								}
						}
				}
				//Y
				if (Mathf.Abs (transform.position.y - target.transform.position.y) > ythreshold) {
						if (Mathf.Abs (transform.position.y - target.transform.position.y) < ytrackSpeed) {
								if (transform.position.y - target.transform.position.y > 0) {
										y = transform.position.y - ytrackSpeed;
								} else {
										y = transform.position.y + ytrackSpeed;
								}
						} else {
								if (transform.position.y - target.transform.position.y > 0) {
										y = target.transform.position.y + ythreshold;
								} else {
										y = target.transform.position.y - ythreshold;
								}
						}
				}
				transform.position = new Vector3 (x, y, transform.position.z);
		}
}
			
