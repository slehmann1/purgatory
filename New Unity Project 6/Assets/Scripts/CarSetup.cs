using UnityEngine;
using System.Collections.Generic;
public class CarSetup : MonoBehaviour
{
		public GameObject frontWheel, rearWheel, body;
		public List<Sprite> wheels;
		public List<Sprite> Bodies;
		public List<GameObject> possibles;
		public List<int>probabilities;
		// Use this for initialization
		void Start ()
		{
				body.GetComponent<SpriteRenderer> ().sprite = Bodies [Random.Range (0, Bodies.Count)];
				frontWheel.GetComponent<SpriteRenderer> ().sprite = wheels [Random.Range (0, wheels.Count)];
				rearWheel.GetComponent<SpriteRenderer> ().sprite = frontWheel.GetComponent<SpriteRenderer> ().sprite;
				for (int i = 0; i<=possibles.Count-1; i++) {
						if (Random.Range (0, 100) < probabilities [i]) {
								SpawnObject (possibles [i]);
						}
				}
		}
		// Update is called once per frame
		void SpawnObject (GameObject g)
		{
		g= (GameObject)Instantiate(g);
		g.transform.parent=this.transform;
		g.transform.localPosition=g.transform.position;
	}
}
