using UnityEngine;
using System.Collections;
public class ChildDistanceJointSetup : MonoBehaviour {
	public Rigidbody2D connector;
public void act(){
		foreach(Transform child in transform){
			if(child.GetComponent<DistanceJoint2D>()){
				child.GetComponent<DistanceJoint2D>().connectedBody=connector;
			}
		}
	}
}
