using UnityEngine;
using System.Collections;
public class SpawnEffect : MonoBehaviour {
	private Animator anim;
	void Start(){
		anim=GetComponent<Animator>();
	}
	public void spawn(){
	anim.SetBool("Animate",true);
		Invoke("Reset",0.1f);
	}
	private void Reset(){
		anim.SetBool("Animate",false);
	}
}
