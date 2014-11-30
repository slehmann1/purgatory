using UnityEngine;
using System.Collections;
using System;

public class Snappable : MonoBehaviour {
    public bool snapOnExplode = true;
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
            Debug.Log("RUN");
        }
        catch
        {
            throw new Exception("Particle effect not assigned to gameobject: "+name);
        }
    }
    
}
