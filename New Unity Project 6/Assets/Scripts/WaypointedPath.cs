using UnityEngine;
using System.Collections;
public class WaypointedPath : MonoBehaviour {
	[System.Serializable]
	public class Waypoint{
		public float amountOfTime;//in seconds
		public Transform waypoint;
		public float delay;
	}
	public Waypoint[] waypoints;
	private int index=0;
	private float step =0;
	private bool cont = true;
	// Use this for initialization
	void Start () {
		if(waypoints.Length<2){
			Debug.LogError("Not Enough Waypoints");
		}
		calculateStep();	
	}
	void Update(){
		if(cont){
		Vector3 oldPos=transform.position;
		transform.position=Vector2.MoveTowards(transform.position,waypoints[index].waypoint.position,step*Time.deltaTime);
		if(oldPos==transform.position){
				Invoke("contin", waypoints[index].delay);
			index++;
			if(index>=waypoints.Length){
				index=0;
			}
			calculateStep();
				cont = false;
		}
		}}
	void contin(){
		cont = true;
	}
	void calculateStep(){
		Vector2 dist=transform.position-waypoints[index].waypoint.position;
		float distance= Mathf.Abs(dist.x)+Mathf.Abs(dist.y);
		step= distance/waypoints[index].amountOfTime;
	}
}
