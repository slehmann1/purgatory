using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
[CustomEditor (typeof (explosionForce))]
[CanEditMultipleObjects]

public class explosionEditor : Editor {
	private explosionForce obj;
	void Start(){
		Debug.Log ("RUN");
		obj = (explosionForce)target;
		}
	void OnSceneGUI() {
		obj = (explosionForce)target; 
		if (obj.range < 0) {
						obj.range = 0;
				}
		Handles.DrawSolidDisc (obj.transform.position,  new Vector3(0, 0, 1), obj.range); 	

}
}	