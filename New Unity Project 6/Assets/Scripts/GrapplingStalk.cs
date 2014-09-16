using UnityEngine;
using System.Collections;
public class GrapplingStalk : MonoBehaviour {
	DistanceJoint2D joint;
	void Start(){
		joint = GetComponent<DistanceJoint2D>();//0 is to block, 1 is to player
		joint.connectedBody=transform.parent.rigidbody2D;
	}
}
