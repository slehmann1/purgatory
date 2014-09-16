using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


[CustomEditor (typeof (ConnectObjects))]
[CanEditMultipleObjects]
public class ConnectObjectsEditor : Editor {
	private Handles startPoint, endPoint;

	 void OnSceneGUI () {
		ConnectObjects myTarget= (ConnectObjects)target;
		Handles.color=Color.blue;
		myTarget.startPoint=Handles.PositionHandle(myTarget.startPoint,Quaternion.identity);
		Handles.Disc(Quaternion.identity,myTarget.startPoint,new Vector3(0,0,1),1,false,1);
		Handles.Label(myTarget.startPoint,"Start Point");
		myTarget.endPoint=Handles.PositionHandle(myTarget.endPoint,Quaternion.identity);
		Handles.Disc(Quaternion.identity,myTarget.endPoint,new Vector3(0,0,1),1,false,1);
		Handles.Label(myTarget.endPoint,"EndPoint");


		//this is where the math starts
		//setting the position to halfway between the beginning and the end
			myTarget.connector.transform.position=Vector3.Lerp(myTarget.startPoint,myTarget.endPoint,0.5f);
		//getting the scale
		float scale = Vector3.Distance(myTarget.startPoint,myTarget.endPoint)/myTarget.transform.lossyScale.x;
		myTarget.scale=scale;
		myTarget.connector.transform.localScale=new Vector3(scale,0.2f);

		//trig
		float degrees = Mathf.Atan((myTarget.endPoint.y-myTarget.startPoint.y)/(myTarget.endPoint.x-myTarget.startPoint.x));
		degrees*=180;
		degrees/=Mathf.PI;
		myTarget.connector.transform.rotation= Quaternion.Euler(0,0,degrees);
		}
	

}
