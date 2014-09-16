using UnityEngine;
using System.Collections.Generic;
public class Lives : MonoBehaviour
{
		public GameObject RedFlash;
		public int number;
		public GameObject live;
		public GameObject smokeEffect;
		public float spacing;
		private List <GUITexture> lives;
		private int livesLeft;
		public float xStart, yStart;
		private Camera cam;
		public string KeyCode;
		public bool removal;
		public void addLives (int amount)
		{
				livesLeft += amount;
				bool cont = true;
				int i = 0;
				try {
						while (cont) {
								if (!lives [i].GetComponent<LifeBehaviour> ().isAlive ()) {
										i++;
								} else {
										cont = false;
										i--;
								}
						}
				} catch {
				}
				while (amount>0&&i>=0) {
						lives [i].GetComponent<LifeBehaviour> ().life ();
						i--;
						amount--;
				}
				while (amount>0) {
						amount--;
						addLife ();
				}
		}
		private void addLife ()
		{
				GameObject g = (GameObject)GameObject.Instantiate (live, new Vector3 (xStart + spacing * (lives.Count - 1), yStart), Quaternion.identity);
				g.transform.parent = this.transform;
				g.layer = 5;
				g.GetComponent<LifeBehaviour> ().setSmoke (smokeEffect);
				g.GetComponent<LifeBehaviour> ().setCam (cam);
				GUITexture gt = g.GetComponent<GUITexture> ();
				lives.Add (gt);
		}
		private void setupLives ()
		{
				for (int i = 0; i<number; i++) {
						addLife ();
				}
		}
		void Start ()
		{
				cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
				lives = new List<GUITexture> ();
				setupLives ();
				livesLeft = number;
		}
		public int getLivesLeft ()
		{
				return livesLeft;
		}
		public void Reset ()
		{
				livesLeft = number;
				lives.Clear ();
				setupLives ();
		}
		public void flash(){
		RedFlash.GetComponent<RedFlash> ().flash (true);
	}
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetButtonDown (KeyCode)) {
						if (livesLeft > 0) {
								if (removal) {
										suicide ();//this means it is pills
								} else {//needs to change the cameras target
								}
						} else {
								flash();
						}
				}
		}
		public void suicide ()
		{
				livesLeft--;
				LifeBehaviour b = lives [(lives.Count - livesLeft) - 1].GetComponent<LifeBehaviour> ();
				b.death ();
		}
}
