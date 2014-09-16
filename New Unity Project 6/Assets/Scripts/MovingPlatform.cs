using UnityEngine;
using System.Collections;
public class MovingPlatform : MonoBehaviour
{
		public WaypointScript[] Waypoints ;
		private bool there = false;
		// Use this for initialization
		void Start ()
		{
				StartCoroutine ("move");
		}
		IEnumerator move ()
		{
				int index = 0;
				//float StartTime=Time.time;
				//float travelLength= Vector2.Distance(Waypoints[index].transform.position,transform.position);
				//float time = Speed;
				//Vector3 t =Waypoints[index].transform.position;
				while (true) {
						//float distCovered= (Time.time-StartTime)*Speed;
						//float frac=distCovered/travelLength;
						//Debug.Log("KASJDK");
						transform.position = Vector2.Lerp (transform.position, Waypoints [index].transform.position, Waypoints [index].Speed);
						transform.rotation = Quaternion.Lerp (transform.rotation, Waypoints [index].transform.rotation, Waypoints [index].Speed);
						//time-=Time.deltaTime;
						//Waypoints[index].transform.position=t;
						if (Mathf.Abs (transform.position.x - Waypoints [index].transform.position.x) < Waypoints [index].Margin && Mathf.Abs (transform.position.y - Waypoints [index].transform.position.y) < Waypoints [index].Margin && !there) {
								index++;
								there = true;
								if (index > Waypoints.Length - 1) {
										index = 0;
								}
						} else {
								there = false;
						}
						yield return new WaitForSeconds (0.01f);
				}
		}
		// Update is called once per frame
		void Update ()
		{
		}
}
