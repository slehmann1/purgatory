using UnityEngine;
using System.Collections;
public class NonLoopingScrollScript : MonoBehaviour
{
		private float xspeed, yspeed;
		private HillSpawner h;
		public bool prevSpawned = false;
		// Use this for initialization
		public void Setup (float xspeed, float yspeed)
		{
				this.xspeed = xspeed;
				this.yspeed = yspeed;
				prevSpawned = false;
		}
		public void Setup (float xspeed, float yspeed, bool spawn)
		{
				this.xspeed = xspeed;
				this.yspeed = yspeed;
				prevSpawned = spawn;
		}
		public void DisableSpawn ()
		{
				prevSpawned = true;
		}
		public void OnBecameInvisible ()
		{
		}
		public void EnableSpawn ()
		{
				prevSpawned = false;
		}
		public void OnBecameVisible ()
		{
				if (!prevSpawned) {
						prevSpawned = true;
						h = transform.parent.gameObject.GetComponent<HillSpawner> ();
						h.Spawn ();
				}
		}
		public void move ()
		{
				transform.position = new Vector3 (transform.position.x + (xspeed * Time.deltaTime), transform.position.y + (yspeed * Time.deltaTime), transform.position.z);
		}
		public void move (float amount)
		{
				transform.position = new Vector3 (transform.position.x + (amount * xspeed), transform.position.y + (amount * yspeed * Time.deltaTime), transform.position.z);
		}
		void setX (float x)
		{
				transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		}
		void setY (float y)
		{
				transform.position = new Vector3 (transform.position.x, y, transform.position.z);
		}
}
