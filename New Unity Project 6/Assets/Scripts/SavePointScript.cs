﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class SavePointScript : MonoBehaviour {
	private Player_Movement player;
	public GameObject flagPole;
	private bool rotating;
	public float Speed;
    float progress;
	public CannonBehaviour[] Cannons;
	public AudioClip[] audios;
	void Start(){
		audio.clip=audios[Random.Range(0,audios.Length)];
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
		if(player==null){Debug.LogError("Savepoint Cannot Find Player");}
	}
	void rotate(){
		if(rotating){
		if(flagPole.transform.rotation.z/Mathf.PI<1){
				progress+=Speed*Time.deltaTime;
			flagPole.transform.rotation= Quaternion.Euler(0,0,Mathf.LerpAngle(flagPole.transform.rotation.z,180,progress));
		}else{
			rotating=false;
			CancelInvoke();
		}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			activate();
		}
	}
	void activate(){
		if(flagPole.transform.rotation.z/Mathf.PI<1&&!rotating){
            player.spawn = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
			audio.PlayDelayed(12000);
			rotating=true;
			InvokeRepeating("rotate",0.01f,0.01f);
			for(int i=0; i<Cannons.Length;i++){
				Cannons[i].Activate();
			}
		}
	}
}
