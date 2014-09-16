using UnityEngine;
public class CarSpawner : MonoBehaviour
{
		public float minTime, maxTime, startingSpeed, startingForce;
		public Vehicle[] vehicles;
		[System.Serializable]
		public class Vehicle
		{
				public GameObject vehicle;
				public float probability;
		}
		// Use this for initialization
		void Start ()
		{
				System.Array.Sort (vehicles,
		              delegate(Vehicle x, Vehicle y) {
						return x.probability.CompareTo (y.probability);
				});
				float prob = 0;
				for (int i = 0; i< vehicles.Length; i++) {
						prob += vehicles [i].probability;
				}
				if (prob == 100) {
						Invoke ("Spawn", UnityEngine.Random.Range (minTime, maxTime));
				} else {
						Debug.LogError ("Probabilites do not add up to one hundred!");
				}
		}
		// Update is called once per frame
		void Spawn ()
		{
				CancelInvoke ();
				GameObject g = vehicles [0].vehicle;
				float f = UnityEngine.Random.Range (0, 100);
				float amount = 0;
				for (int index =0; index<vehicles.Length; index++) {
						amount += vehicles [index].probability;
						if (amount >= f) {
								g = vehicles [index].vehicle;
								break;
						}
				}
				g = (GameObject)Instantiate (g, transform.position, Quaternion.identity);
		g.GetComponent<Collider2D>().enabled=false;
				g.transform.parent = transform;
		g.GetComponent<Collider2D>().enabled=true;
				g.GetComponent<CarBehaviour> ().setForce (startingForce);
				g.GetComponent<CarBehaviour> ().setSpeed (startingSpeed);
				Invoke ("Spawn", UnityEngine.Random.Range (minTime, maxTime));
		}
}
