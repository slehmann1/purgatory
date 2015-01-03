using UnityEngine;
using System.Collections;
public class LifeBehaviour : MonoBehaviour
{
		private Camera cam;
		public Texture alive, dead;
		private GameObject smoke;
		public void setSmoke (GameObject g)
		{
				smoke = g;
		}
		public void setCam (Camera Cam)
		{
				cam = Cam;
		}
		void Start ()
		{
				GetComponent<GUITexture> ().texture = alive;
		}
	public bool isAlive(){
		if(GetComponent<GUITexture> ().texture==alive)
			return true;
		else
			return false;
	}
	public void life(){
		GetComponent<GUITexture> ().texture = alive;
		Vector3 worldPos = cam.ViewportToWorldPoint (transform.localPosition + Vector3.forward); 
		worldPos.z = 0;
		GameObject smokeObj = (GameObject)Instantiate (smoke, worldPos, Quaternion.identity);
		//Debug.Log (worldPos);
		//smokeObj.layer = 5;
		smokeObj.transform.parent = this.transform;
	}		public void death ()
		{
				GetComponent<GUITexture> ().texture = dead;
				Vector3 worldPos = cam.ViewportToWorldPoint (transform.localPosition + Vector3.forward); 
				worldPos.z = 0;
				GameObject smokeObj = (GameObject)Instantiate (smoke, worldPos, Quaternion.identity);
				//Debug.Log (worldPos);
				//smokeObj.layer = 5;
				smokeObj.transform.parent = this.transform;
		}
}
