using UnityEngine;
public class CarSpawner : MonoBehaviour
{
		public float minTime, maxTime, startingSpeed, startingForce,wheelMargin;
        public bool updateCarSpawnerHeight, automaticallySetSpawnHeight;
        private float spawnHeight;
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
            
                spawnHeight=Physics2D.Raycast(transform.position,Vector3.down).point.y;
            
		}
		// Update is called once per frame
		void Spawn ()
		{
            if (updateCarSpawnerHeight&&automaticallySetSpawnHeight) {
                spawnHeight=Physics2D.Raycast(transform.position, Vector3.down).point.y;
            }
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
                if (automaticallySetSpawnHeight) {
                    g=(GameObject)Instantiate(g, new Vector2(transform.position.x, spawnHeight-g.transform.FindChild("RearWheel").transform.localPosition.y+wheelMargin), Quaternion.identity);
                    Debug.Log(g.transform.FindChild("RearWheel").collider2D.bounds.extents);
                }
                else {
                    g=(GameObject)Instantiate(g, transform.position, Quaternion.identity);
                }
		g.GetComponent<Collider2D>().enabled=false;
				g.transform.parent = transform;
		g.GetComponent<Collider2D>().enabled=true;
				g.GetComponent<CarBehaviour> ().setForce (startingForce);
				g.GetComponent<CarBehaviour> ().setSpeed (startingSpeed);
				Invoke ("Spawn", UnityEngine.Random.Range (minTime, maxTime));
		}
}
