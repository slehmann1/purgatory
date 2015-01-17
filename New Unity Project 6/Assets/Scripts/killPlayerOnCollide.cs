using UnityEngine;
using System.Collections;

public class killPlayerOnCollide : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.gameObject.transform.root.BroadcastMessage("death"); 
            
        }
        
    }
}
