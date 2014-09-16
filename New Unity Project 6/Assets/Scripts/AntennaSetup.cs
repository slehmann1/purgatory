using UnityEngine;
using System.Collections.Generic;
public class AntennaSetup : MonoBehaviour
{
		void Start ()
		{
				HingeJoint2D [] subObjects = GetComponentsInChildren<HingeJoint2D> ();
				for (int i=0; i<subObjects.Length-1; i++) {
						subObjects [i].transform.position = transform.position;
						if (subObjects [i].transform.name == "AntennaBeginning") {
								subObjects [i].connectedBody = transform.parent.GetComponent<Rigidbody2D> ();
								//subObjects[i].connectedAnchor=subObjects[i].transform.position;
								//subObjects[i].anchor=new Vector2(0,-0f);
						} else if (subObjects [i].transform.name == "AntennaEnd") {
								subObjects [i].GetComponent<DistanceJoint2D> ().connectedBody = transform.parent.GetComponent<Rigidbody2D> ();
						}
				}
		}
}
