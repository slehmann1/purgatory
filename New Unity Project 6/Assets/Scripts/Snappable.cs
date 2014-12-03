using UnityEngine;
using System.Collections;
using System;

public class Snappable : MonoBehaviour {
    public bool snapOnExplode = true;
	public static GameObject defaultParticle;
    public GameObject particleEffect;
    public virtual void destroy()
    {
        showParticles();
    }

		
		

    protected void showParticles(){
        try
        {
            particleEffect = (GameObject)GameObject.Instantiate(particleEffect, transform.position, Quaternion.identity);
            particleEffect.transform.parent = transform;
            particleEffect.layer = gameObject.layer;
        }
        catch
        {
         //   throw new Exception("Particle effect not assigned to gameobject: "+name);
			if(!defaultParticle){
				defaultParticle = Resources.Load("defaultSmokeParticle") as GameObject;
			}
			GameObject g = (GameObject)GameObject.Instantiate(defaultParticle, new Vector3 (transform.position.x,transform.position.y,transform.position.z+1), Quaternion.identity);
			g.transform.parent = transform;
			g.layer = gameObject.layer;
        }
    }
    
}
