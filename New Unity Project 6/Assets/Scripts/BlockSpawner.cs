using UnityEngine;
using System.Collections.Generic;
public class BlockSpawner: MonoBehaviour
{
		public GameObject Block, Gravestone;
		public string pillSpawn, heartSpawn;
		public float blockYOffset, gravesYOffset;
		public Transform parent;
        private GrapplingHook hook;
		private List <GameObject> blocks, graves;
		private Lives heartsList, pillsList;
		public Camera camera;
		private Camera_Tracker camTrack;
		public float maximumDelay, minimumDelay, minimumSpeed;
		private bool trackingPlayer = true;
		private bool reverting = false;
		public GameObject smokeEffect;
        private Rigidbody2D rig;
	private Player_Movement p;
		public void Start ()
		{

		p=GetComponent<Player_Movement>();
            rig=GetComponent<Rigidbody2D>();
        hook=GetComponentInChildren<GrapplingHook>();
				camTrack = camera.GetComponent<Camera_Tracker> ();
				blocks = new List<GameObject> ();
				graves = new List<GameObject> ();
				heartsList = GameObject.Find ("LifeCounter").GetComponent<Lives> ();
				pillsList = GameObject.Find ("PillCounter").GetComponent<Lives> ();
		}
		private bool possibleToSpawn (GameObject obj, float yoff)
		{
				Debug.DrawRay (transform.position, new Vector3 (0, -(yoff + obj.renderer.bounds.size.y), 0), Color.magenta, 5f);
				if (Physics2D.Raycast (transform.position, Vector3.down, yoff + obj.renderer.bounds.size.y, 1 << 8)) {
						return false;
				}
				return true;
		}
		private GameObject createBlock (GameObject obj, float yoff)
		{
				if (possibleToSpawn (obj, yoff)) {
                    hook.removeHook();
						GameObject g = (GameObject)Instantiate (obj, new Vector2 (transform.position.x, transform.position.y - yoff - obj.renderer.bounds.size.y / 2), Quaternion.identity);
			g.GetComponent<Collider2D>().enabled=false;
						g.transform.parent = parent;
                        if (g.GetComponent<Rigidbody2D>()) {
                            g.GetComponent<Rigidbody2D>().velocity=rig.velocity;
                        }
			g.GetComponent<Collider2D>().enabled=true;
						g.layer = 8;
						GameObject f = (GameObject)Instantiate (smokeEffect, new Vector3 (transform.position.x, transform.position.y - yoff - obj.renderer.bounds.size.y / 2, transform.position.z - 1), Quaternion.identity);
						f.transform.parent = parent;
						return g;
				} else {
						return null;
				}
		}
		public void Update ()
		{
				if (!trackingPlayer) {
						if (!reverting) {
								if (Mathf.Abs ((graves [graves.Count - 1]).GetComponent<Rigidbody2D> ().velocity.x + graves [graves.Count - 1].GetComponent<Rigidbody2D> ().velocity.y) <= minimumSpeed) {
										Invoke ("revertTarget", minimumDelay);
										reverting = true;
								}
                                if (Input.anyKeyDown) {
                                    revertTarget();
                                }
						} else {
								if (Mathf.Abs ((graves [graves.Count - 1]).GetComponent<Rigidbody2D> ().velocity.x + graves [graves.Count - 1].GetComponent<Rigidbody2D> ().velocity.y) >= minimumSpeed) {
										reverting = false;
										CancelInvoke ();
										Invoke ("revertTarget", maximumDelay);
								}
						}
				} else {
                    if (Input.GetButtonDown(pillSpawn)) {
                        if (pillsList.getLivesLeft()>0) {


                            GameObject g=createBlock(Block, blockYOffset);
                            if (g!=null) {
                                blocks.Add(g);
                            }
                            else {
                                pillsList.flash();
                            }
                        }

                        
                    }
						if (heartsList.getLivesLeft () > 0) {
								if (Input.GetButtonDown (heartSpawn)&&!p.getMovementDisabled()) {
										GameObject g = createBlock (Gravestone, gravesYOffset);
										if (g != null) {
												graves.Add (g);
												camTrack.setTarget ((graves [graves.Count - 1]).transform);
												Invoke ("revertTarget", maximumDelay);
												trackingPlayer = false;
						p.setMovementDisabled (true);
												foreach (Transform child in transform) {
														try {
																child.renderer.enabled = false;
														} catch {
														}
												}
												GetComponent<BoxCollider2D> ().enabled = false;
										} else {
						heartsList.flash();
												//heartsList.addLives(1);
										}
								}
						}
				}
		}
		private void revertTarget ()
		{
		GetComponent<Kill> ().death ();
				CancelInvoke ();
				camTrack.setTarget (this.transform);
				reverting = false;
				trackingPlayer = true;
				foreach (Transform child in transform) {
						try {
								child.renderer.enabled = true;
						} catch {
						}
				}
				GetComponent<BoxCollider2D> ().enabled = true;
				GetComponent<Player_Movement> ().setMovementDisabled (false);
		}
}