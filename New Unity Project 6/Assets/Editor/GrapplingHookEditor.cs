using UnityEditor;
using UnityEngine;
using System.Collections;



[CustomEditor (typeof(GrapplingHook))]
[CanEditMultipleObjects]
public class GrapplingHookEditor : Editor {
    GrapplingHook myTarget;
    float oldTimeToChange,oldSpeed;
    public void Start()
    {
        myTarget = (GrapplingHook)target;
    }
	public  void OnSceneGUI ()
	{
        if (myTarget == null)
        {
            myTarget = (GrapplingHook)target;
            oldTimeToChange = myTarget.timeToChangeLength;
            oldSpeed = myTarget.changeSpeed;
        }
        
		Color c =Color.green;
		c.a=0.1f;
		Handles.color=c;
		Handles.DrawSolidDisc(((GrapplingHook)target).transform.position,Vector3.forward,((GrapplingHook)target).range);

        if (oldTimeToChange != myTarget.timeToChangeLength)
        {
            float oldChangeSpeed = myTarget.changeSpeed;
            myTarget.changeSpeed *= (myTarget.timeToChangeLength / oldTimeToChange);
            if (float.IsNaN(myTarget.changeSpeed))
            {
                myTarget.changeSpeed = oldChangeSpeed;
            }
        }
        if(oldSpeed!=myTarget.changeSpeed&&myTarget.changeSpeed<0){
            myTarget.changeSpeed = oldSpeed;
        }
        if (oldTimeToChange != myTarget.timeToChangeLength && myTarget.timeToChangeLength < 0)
        {
            myTarget.timeToChangeLength = oldTimeToChange;
        }
        oldSpeed = myTarget.changeSpeed;
            oldTimeToChange = myTarget.timeToChangeLength;
        



	
	}
}
