using UnityEngine;
using System.Collections;

public class killPlayerOnCollide : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        try
        {
            coll.gameObject.SendMessageUpwards("death");
        }
        catch
        {
            //there is no kill component on the object, probably not the player that collided
        }
    }
}
