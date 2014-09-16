using UnityEngine;
using System.Collections;
public class Update : MonoBehaviour
{
		public ScrollScript[] layers;
		public void run (float x)
		{
				Debug.Log ("RUN");
				for (int i = 0; i<layers.Length; i++) {
						layers [i].move (x);
				}
		}
}
