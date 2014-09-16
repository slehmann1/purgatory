using UnityEngine;
using System.Collections.Generic;
public class HillSpawner : MonoBehaviour
{
		[SerializeField]
		int
				layer = 10;//10 for background, 9 for foreground
		private List <GameObject> subs;
		public GameObject[] hills;
		public float movSpeed;
		public int buffer;
		public float height, width;
		void Awake ()
		{
				subs = new List<GameObject> ();
				GameObject g = ((GameObject)Instantiate (hills [Random.Range (0, hills.Length)], new Vector3 ((-buffer), transform.position.y, transform.position.z), Quaternion.identity));
				g.transform.localScale = new Vector3 (width, height, 1);
				g.transform.parent = this.transform;
				g.layer = layer;
				NonLoopingScrollScript nls = (NonLoopingScrollScript)g.GetComponent ("NonLoopingScrollScript");
				subs.Add (g);
				nls.Setup (movSpeed, 0.0f, true);
				while (subs[subs.Count-1].transform.position.x<15) {
						Spawn ();
						NonLoopingScrollScript nlss = (NonLoopingScrollScript)subs [subs.Count - 1].GetComponent ("NonLoopingScrollScript");
						nlss.Setup (movSpeed, 0.0f, true);
				}
				nls = (NonLoopingScrollScript)subs [subs.Count - 1].GetComponent ("NonLoopingScrollScript");
				nls.EnableSpawn ();
		}
		public void move (float f)
		{
				for (int i=0; i<subs.Count; i++) {
						NonLoopingScrollScript s = (NonLoopingScrollScript)subs [i].GetComponent ("NonLoopingScrollScript");
						s.move (f);
						//subs [i].transform.position = new Vector3 (subs [i].transform.position.x + (movSpeed * Time.deltaTime), subs [i].transform.position.y, subs [i].transform.position.z);
				}
		}
		/// <summary>
		/// Spawn a hill
		/// </summary>
		public void Spawn ()
		{
				GameObject g = ((GameObject)Instantiate (hills [Random.Range (0, hills.Length)], new Vector3 (0.0f, transform.position.y, transform.position.z), Quaternion.identity));
				g.transform.position = new Vector3 ((subs [subs.Count - 1].renderer.bounds.max.x + (g.renderer.bounds.max.x) * width), transform.position.y, transform.position.z);
				g.transform.parent = this.transform;
				g.transform.localScale = new Vector3 (width, height, 1);
				g.layer = layer;
				NonLoopingScrollScript nls = (NonLoopingScrollScript)g.GetComponent ("NonLoopingScrollScript");
				nls.Setup (movSpeed, 0.0f);
				subs.Add (g);
		}
}
