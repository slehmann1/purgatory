using UnityEngine;
using System.Collections;
[RequireComponent (typeof(ParticleEmitter))]
public class CannonBehaviour : MonoBehaviour {
	public float length;
	public void Activate(){
		particleEmitter.emit=true;
		Invoke("deActivate",length);
	}
	public void deActivate(){
		particleEmitter.emit=false;
	}
}
