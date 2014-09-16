using UnityEngine;
using System.Collections;
public class HingeSetup : MonoBehaviour {
	public GameObject connected;
	// Use this for initialization
	public void setConnected(GameObject connected){
		this.connected=connected;
		HingeJoint2D hinge= GetComponent<HingeJoint2D>();
		hinge.connectedBody = connected.GetComponent<Rigidbody2D>();
	}
}
