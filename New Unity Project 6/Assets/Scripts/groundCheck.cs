using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[RequireComponent (typeof(Collider2D))]
public class groundCheck : MonoBehaviour {
    Collider2D coll;
    private List<GameObject> currentlyColliding;
    public LayerMask layersToCollideWith;
    public void Start()
    {
        currentlyColliding = new List<GameObject>();
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D coll)
    {
            currentlyColliding.Remove(coll.gameObject);      
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

        if(((1<<coll.gameObject.layer)&layersToCollideWith.value)>0){//check that it collides with the layermask
        currentlyColliding.Add(coll.gameObject);
        }
        else
        {
            Debug.Log(coll.name+" | "+coll.gameObject.layer);
        }
    }
    public bool isGrounded()
    {
        if(currentlyColliding.Count>0){
            return true;
        }
        else
        {
            return false;
        }
    }
	
}
