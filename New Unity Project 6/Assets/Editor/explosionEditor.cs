using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
[CustomEditor(typeof(explosionForce))]
[CanEditMultipleObjects]

public class explosionEditor : Editor
{
    private explosionForce obj;
    private float oldRange = 0;
    private Color c;
    void Start()
    {
        obj = (explosionForce)target;
        c = Color.red;
        c.a = 0.05f;
    }
    void OnSceneGUI()
    {
        if (obj == null)
        {
            Start();
        }
        if (obj.range < 0)
        {
            obj.range = 0;
        }

        if (obj.range != oldRange)
        {
			obj.GetComponent<CircleCollider2D>().radius = obj.range;
            obj.GetComponent<CircleCollider2D>().isTrigger = true;
        }

        Handles.color = c;

        Handles.DrawSolidDisc(obj.transform.position, new Vector3(0, 0, 1), obj.range*2);
        oldRange = obj.range;


    }
}