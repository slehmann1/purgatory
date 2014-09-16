using UnityEngine;
using System.Collections;
public class CloudMovement : MonoBehaviour
{
		public float speed;
		void Update ()
		{
				transform.Translate (speed * Time.deltaTime, 0.0f, 0.0f);
		}
		void OnBecameInvisible ()
		{
				DestroyObject (gameObject);
		}
		public void setSpeed (float f)
		{
				speed = f;
		}
}
