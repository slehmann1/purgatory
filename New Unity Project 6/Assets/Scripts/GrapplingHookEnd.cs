using UnityEngine;
using System.Collections;
public class GrapplingHookEnd : MonoBehaviour
{
		bool grappling = false;
		Vector2 Position;
		public Vector2 initPosition;
		bool attached;
		public Vector3 initRotation;
		Quaternion rotInit;
	Quaternion rot;
		GameObject player;
		DistanceJoint2D dist;
	Vector3 scale;
	private Transform parent;
		void OnCollisionEnter2D (Collision2D collision)
		{
				attached = true;
				Position = transform.position;
		RaycastHit2D hit = Physics2D.Raycast(transform.position,new Vector2(transform.rotation.z,transform.rotation.w),5.0f, 1 << 8);
		if (hit !=null){
			float angle = Vector2.Angle(transform.position,hit.normal);
			angle=90-Mathf.Abs(angle);
			Debug.DrawRay(transform.position,new Vector3(0,0,angle*180/Mathf.PI),Color.red,2.0f,true);
			rot=Quaternion.Euler(0f,0f,angle);
		}
	}
	public void setRange(Vector3 Target){ 
		dist.distance=Vector3.Distance(Target,transform.position); 
	}
	public void Init (GameObject newPlayer )
		{
		parent=transform.parent;
		player = newPlayer;
		dist = GetComponent<DistanceJoint2D> ();
				//GetComponent<HingeJoint2D>().connectedBody=transform.parent.rigidbody2D;
				//transform.rotation=Quaternion.Euler(0f,0f,90f);
				rotInit = Quaternion.Euler (initRotation);
				Debug.Log (initPosition);
		}
		public void Setup (float angle){
		scale=transform.localScale;
				dist.connectedBody = player.rigidbody2D;
				transform.localRotation = rotInit;
				transform.localPosition = initPosition;
				grappling = true;
		}
		public void stop ()
		{
				grappling = false;
				attached = false;
		}
		void Update ()
		{
				if (grappling) {
			if (attached) {
				transform.parent=null;
				transform.rotation = rot;
				transform.parent=parent;
				transform.position = Position;
			}else{
						transform.localPosition = initPosition;
			}
				}
		transform.localRotation = rotInit;
		transform.localScale=scale;
		}
}
