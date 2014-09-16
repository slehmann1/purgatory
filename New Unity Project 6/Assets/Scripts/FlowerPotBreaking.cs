using UnityEngine;
using System;
public class FlowerPotBreaking : MonoBehaviour
{
		public float oldVelocity;
		private int highestIndex = 0;
		public FlowerPotPhase[] phases;
		[System.Serializable]
		public class FlowerPotPhase
		{
				public float requiredVelocity;
				public Sprite flowerPotTexture;
		public GameObject[] spawnables;
		public Vector2[] polygon;
		}
		// Use this for initialization
		void Start ()
		{
				oldVelocity = 0;
				System.Array.Sort (phases,
		                   delegate(FlowerPotPhase x, FlowerPotPhase y) {
						return x.requiredVelocity.CompareTo (y.requiredVelocity);
				});
		}
		// Update is called once per frame
		void OnCollisionEnter2D (Collision2D collision)
		{
				//float velocity =(float) Math.Pow((rigidbody2D.velocity.x*rigidbody2D.velocity.x)+(rigidbody2D.velocity.y*rigidbody2D.velocity.y),0.5);
				//float acceleration = (velocity-oldVelocity)/Time.deltaTime;
				//oldVelocity=velocity;
				int index = 0;
				for (int i=1; i<phases.Length; i++) {
						if (phases [i].requiredVelocity <= Mathf.Abs (collision.relativeVelocity.magnitude)) {
								index = i;
						}
				}
				if (index > highestIndex) {
						highestIndex = index;
						GetComponent<SpriteRenderer> ().sprite = phases [index].flowerPotTexture;
			GetComponent<PolygonCollider2D>().SetPath(0,phases[index].polygon);
						if (phases [index].spawnables.Length > 0) {
				for(int i =0; i<phases[index].spawnables.Length; i++){
					GameObject g =(GameObject)GameObject.Instantiate(phases[index].spawnables[i]);
					g.GetComponent<Collider2D>().enabled=false;
					g.transform.parent=this.transform;
					g.GetComponent<Collider2D>().enabled=true;
					g.transform.localPosition=g.transform.position;
					g.transform.localScale=g.transform.lossyScale;
				}
						}
				}
//Debug.Log("Collision");
		for(int i = 0; i<GetComponent<PolygonCollider2D>().GetPath(0).Length;i++){
			//Debug.Log(GetComponent<PolygonCollider2D>().GetPath(0)[i]);
		}
		}
}
