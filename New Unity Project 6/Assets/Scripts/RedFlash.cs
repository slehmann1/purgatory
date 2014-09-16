using UnityEngine;
using System.Collections;
public class RedFlash : MonoBehaviour
{
		public float maxAlpha, timeTo, timeEnd, timeBetween;//remember max alpha is in decimal, not percent
		public int flashes;
		private int flashNum;
		private bool up;
		private float addition, subtraction;
		private const float timeDiff = 0.01f;
		// Use this for initialization
		void Start ()
		{
				Color texColor = GetComponent<GUITexture> ().color;
				texColor.a = 0;
				GetComponent<GUITexture> ().color = texColor;
				addition = (maxAlpha) / timeTo * timeDiff;
				subtraction = (maxAlpha) / timeEnd * timeDiff;
				//InvokeRepeating("run",timeDiff,timeDiff);
				up = true;
		}
		public void flash (bool soundEnabled)
		{
				up = true;
				Color texColor = GetComponent<GUITexture> ().color;
				texColor.a = 0;
				GetComponent<GUITexture> ().color = texColor;
				flashNum = 0;
				CancelInvoke ();
				InvokeRepeating ("run", timeDiff, timeDiff);
		if(soundEnabled){
			audio.Play();
		}	
		}
		private void run ()
		{
				Color texColor = GetComponent<GUITexture> ().color;
				if (up) {
						texColor.a += addition;
				} else {
						texColor.a -= subtraction;
				}
				GetComponent<GUITexture> ().color = texColor;
				if (texColor.a <= 0) {		
						CancelInvoke ();
						flashNum += 1;
						if (flashNum == flashes) {
								//	Destroy(gameObject);
						} else {
								up = true;
								InvokeRepeating ("run", timeBetween, timeDiff);
						}
				} else if (texColor.a >= maxAlpha) {
						up = false;
				}
				//	Debug.Log(addition+","+texColor.a);
		}
}
