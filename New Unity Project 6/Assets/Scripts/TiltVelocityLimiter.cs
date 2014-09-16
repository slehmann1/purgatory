using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody2D))]
public class TiltVelocityLimiter : MonoBehaviour {
	public float limit,ratio;
	public float smoothing;
	public enum lsn{lerp, slerp, none};
	public lsn choice;
	private float velocity;
	void Update () {
		velocity=transform.position.x-velocity;
		velocity/=Time.deltaTime;
		float degrees= ratio*velocity;
		degrees*=-1;
		degrees=Mathf.Clamp(degrees,-limit,limit);
		if(choice==lsn.lerp){
		degrees=Mathf.LerpAngle(transform.rotation.eulerAngles.z,degrees,smoothing);
			transform.rotation= Quaternion.Euler(0,0,degrees);
		}else if(choice == lsn.slerp){
			Quaternion q= Quaternion.Euler(0,0,degrees);
			transform.rotation=Quaternion.Slerp(transform.rotation,q,smoothing);
		}else{
			transform.rotation= Quaternion.Euler(0,0,degrees);
			}
		velocity=transform.position.x;
	}
}
