using UnityEngine;
using System.Collections;
public class ChildHingesConfigurator : MonoBehaviour {
	public Rigidbody2D connector;
	// Use this for initialization
	public void act(){
	foreach(Transform child in transform){
				if(child.GetComponent<HingeJoint2D>()){
				child.GetComponent<HingeJoint2D>().connectedBody=connector;
				}
	}
	}
}
