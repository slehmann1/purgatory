using UnityEngine;
using System.Collections;
public class SnapWithPlayer : ProbabilitySnappable
{
		public float delay;
		
		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "Player") {
						//collided with player
						Invoke ("destroy", delay);
				}
		}
		void OnCollisionExit (Collision collision)
		{
				destroy ();
		}
		
}
