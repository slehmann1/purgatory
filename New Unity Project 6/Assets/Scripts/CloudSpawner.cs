using UnityEngine;
using System.Collections;
public class CloudSpawner : MonoBehaviour
{
		[SerializeField]
		int layer = 10;
		public float percentChance;
		public float minTime;
		public float minSpeed;
		public float maxSpeed;
		private float timeElapsed = 0.0f;
		public float posRandomness, yRandom, xRandom;
		public GameObject[] cloud;
		public  int initX, initY;
		// Update is called once per frame
		void Update ()
		{
				timeElapsed += Time.deltaTime;
				if (timeElapsed > minTime) {
						if (Random.Range (0.0f, 100.0f) >= percentChance) {
								GameObject c = (GameObject)Instantiate (cloud [Random.Range (0, cloud.Length)], new Vector3 (transform.position.x, transform.position.y + Random.Range (-posRandomness, posRandomness), transform.position.z), Quaternion.identity);
								c.transform.parent = this.transform;
								c.transform.localScale = new Vector3 ((initX + Random.Range (-xRandom, xRandom)), (initY + Random.Range (-yRandom, yRandom)), 1);
								c.GetComponent<CloudMovement> ().setSpeed (Random.Range (minSpeed, maxSpeed));
								c.layer = layer;
								timeElapsed = 0.0f;
						}
				}
		}
}
