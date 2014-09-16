using UnityEngine;
using System.Collections;
public class DoubleJumpParticle : MonoBehaviour
{
		public float firstRate, secondRate, timeOfApparition, maxAlpha;
		private float Location;
		public void Start ()
		{
				Color color = transform.renderer.material.color;
				color.a = 0.0f;
				transform.renderer.material.color = color;
				Run ();
		}
		public void Run ()
		{
				Location = 0.0f;
				//transform.renderer.material.color.a=1.0f;
				InvokeRepeating ("Appear", 0.1f, 0.1f);
		}
		/// <summary>
		/// generate the particle
		/// </summary>
		private void Appear ()
		{
				//-(x-1)^2+1
				Location += 0.1f;
				Color color = transform.renderer.material.color;
				color.a = firstRate * (-1 * ((Location - 1) * (Location - 1)) + 1.0f);
				transform.renderer.material.color = color;
				if (color.a >= maxAlpha) {
						CancelInvoke ();
						Invoke ("FirstOver", timeOfApparition);
				}
		}
		private void FirstOver ()
		{
				Color color = transform.renderer.material.color;
				color.a = 1.0f;
				transform.renderer.material.color = color;
				Location = 0.0f;
				InvokeRepeating ("Disappear", 0.1f, 0.1f);
		}
		/// <summary>
		/// remove the particle
		/// </summary>
		private void Disappear ()
		{
				//-x^2+1
				Location += 0.1f;
				Color color = transform.renderer.material.color;
				color.a = -secondRate * (Location * Location) + 1.0f;
				transform.renderer.material.color = color;
				if (color.a <= 0) {
						CancelInvoke ();
						Destroy (gameObject);
				}
		}
}
