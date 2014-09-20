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
	private int index=0,previndex=0;
	private float step =0;
	private bool cont = true;
    public bool drawLines=false;
	// Use this for initialization
	void Start () {
        previndex=waypoints.Length-1;
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
                previndex++;
			index++;
			if(index>=waypoints.Length){
				index=0;
			}
            if (previndex>=waypoints.Length) {
                previndex=0;
            }
			calculateStep();
				cont = false;
		}
		}
        if (drawLines) {
            Debug.DrawLine(waypoints [0].waypoint.position, waypoints [waypoints.Length-1].waypoint.position, Color.black, 0f, false);
            for (int i=1; i<waypoints.Length;i++ ) {
                Debug.DrawLine(waypoints[i].waypoint.position,waypoints[i-1].waypoint.position,Color.black,0f,false);
            }
            Debug.DrawLine(waypoints [index].waypoint.position, waypoints [previndex].waypoint.position, Color.green, 0f, false);
        }
    
    }
	void contin(){
		cont = true;
	}
	void calculateStep(){
		Vector2 dist=transform.position-waypoints[index].waypoint.position;
		float distance= Mathf.Abs(dist.x)+Mathf.Abs(dist.y);
		step= distance/waypoints[index].amountOfTime;
	}
}
