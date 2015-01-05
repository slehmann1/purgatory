using UnityEngine;
using System.Collections;

public class killPlayerOnCollide : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.gameObject.SendMessageUpwards("death");//send to parent
            coll.gameObject.BroadcastMessage("death");//send to children
        }
        
    }
}
