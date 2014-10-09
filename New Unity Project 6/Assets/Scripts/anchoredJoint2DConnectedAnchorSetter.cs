using UnityEngine;
using System.Collections;

public class anchoredJoint2DConnectedAnchorSetter : MonoBehaviour {
    AnchoredJoint2D [] joints;
	void Start () {
        joints=GetComponents<AnchoredJoint2D>();//gets all joints on prefab
        foreach (AnchoredJoint2D joint in joints) {
            joint.connectedAnchor=(Vector2)transform.position+joint.anchor;//sets location of joints
        }
	}
	

}
