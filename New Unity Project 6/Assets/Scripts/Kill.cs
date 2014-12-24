using UnityEngine;
using System.Collections;
public class Kill : MonoBehaviour
{
		private Player_Movement p;
		private Lives pills, hearts;
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
    /// <summary>
    /// This is the one that takes away a life
    /// </summary>
		public void temporaryDeath ()
		{
				//pills.Reset();
				//hearts.Reset();
				hearts.suicide ();
				p.respawn ();
		}
        public void death()
        {
            hearts.Reset();
            pills.Reset();
            p.respawn();
        }
}
