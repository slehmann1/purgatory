using UnityEngine;
using System.Collections;
public class ScrollScript : MonoBehaviour
{
		public float speed;
		// Update is called once per frame
		void Update ()
		{
				renderer.material.mainTextureOffset = new Vector2 (Time.time * speed, 0f);
		}
		public void move (float x)
		{
				renderer.material.mainTextureOffset = new Vector2 (renderer.material.mainTextureOffset.x + (x * speed), 0f);
		}
}
