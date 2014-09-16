using UnityEditor;
using UnityEngine;
using System.Collections;



[CustomEditor (typeof(GrapplingHook))]
[CanEditMultipleObjects]
public class GrapplingHookEditor : Editor {


	public  void OnSceneGUI ()
	{	
		Color c =Color.green;
		c.a=0.1f;
		Handles.color=c;
		Handles.DrawSolidDisc(((GrapplingHook)target).transform.position,Vector3.forward,((GrapplingHook)target).range);
		/*if(((GrapplingHook)target).passAJoint().distance!=((GrapplingHook)target).range){
			 c =Color.red;
			c.a=0.1f;
			Handles.color=c;
			//Handles.DrawSolidDisc(((GrapplingHook)target).transform.position,Vector3.forward,((GrapplingHook)target).passAJoint().distance);
		}*/




	
	}
}
