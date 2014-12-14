using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent (typeof(Collider2D))]
public class groundCheck : MonoBehaviour {
    Collider2D coll;
    private List<GameObject> currentlyColliding;
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
        currentlyColliding.Add(coll.gameObject);
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
