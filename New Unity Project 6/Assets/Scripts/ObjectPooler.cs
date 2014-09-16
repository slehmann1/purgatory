using UnityEngine;
using System.Collections.Generic;
public class ObjectPooler : MonoBehaviour
{
		public bool willGrow;
		private List<GameObject> objs;
		public int initialLength;
		public int maxLength;
		public GameObject obj;
		// Use this for initialization
		void Start ()
		{
			//System.Diagnostics.Stopwatch sw=new System.Diagnostics.Stopwatch();
			//sw.Start();
				objs = new List<GameObject> ();
		for (int i=0; i< initialLength; i++) {
						objs.Add ((GameObject)GameObject.Instantiate (obj));
						if (objs [i].collider != null) {
								objs [i].collider.enabled = false;
								objs [i].transform.parent = transform;
								objs [i].collider.enabled = true;
						} else {
								objs [i].transform.parent = this.transform;
						}
						objs [i].SetActive (false);
				}
		//	sw.Stop();
		//	Debug.Log("Time: "+sw.ElapsedMilliseconds+" ms");
		}
	void setPoolSize(int size){
		if(size< objs.Count){		 
			while(size<objs.Count&&objs.Count-1<maxLength){
				objs.Add ((GameObject)GameObject.Instantiate (obj));
				if (objs [objs.Count-1].collider != null) {
					objs [objs.Count-1].collider.enabled = false;
					objs [objs.Count-1].transform.parent = transform;
					objs [objs.Count-1].collider.enabled = true;
				} else {
					objs [objs.Count-1].transform.parent = this.transform;
				}
				objs [objs.Count-1].SetActive (false);
				}
		}else if(size>objs.Count){
				objs.RemoveRange(size,(objs.Count-1-size));
		}
	}
		// Update is called once per frame
		public GameObject fetch ()
		{
				for (int i=0; i< objs.Count; i++) {
						if (!objs [i].activeSelf) {
								objs [i].SetActive (true);
								return objs [i];
						}
				}
				if (willGrow && objs.Count < maxLength) {
						objs.Add ((GameObject)GameObject.Instantiate (obj));
						if (objs [objs.Count - 1].collider != null) {
								objs [objs.Count - 1].collider.enabled = false;
								objs [objs.Count - 1].transform.parent = transform;
								objs [objs.Count - 1].collider.enabled = true;
						} else {
								objs [objs.Count - 1].transform.parent = transform;
						}
						return objs [objs.Count - 1];
				} else {
						return null;
				}
		}
}
