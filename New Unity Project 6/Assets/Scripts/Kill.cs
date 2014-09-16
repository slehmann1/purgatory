using UnityEngine;
using System.Collections;
public class Kill : MonoBehaviour
{
		private Player_Movement p;
		private Lives pills;
		private Lives hearts;
		void Start ()
		{
				hearts = GameObject.Find ("LifeCounter").GetComponent<Lives> ();
				pills = GameObject.Find ("PillCounter").GetComponent<Lives> ();
				p = GetComponent<Player_Movement> ();
		}
		void onTriggerEnter2D (Collider2D other)
		{
				Debug.Log ("Collision");
				if (other.tag == "Player") {
						Debug.Log ("Kill");
				}
		}
		public void death ()
		{
				//pills.Reset();
				//hearts.Reset();
				hearts.suicide ();
				p.respawn ();
		}
}
