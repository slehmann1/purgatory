using UnityEngine;
using System.Collections.Generic;
public class BlockSpawner: MonoBehaviour
{
		public GameObject Block, BlockSilhouette, Gravestone,GravestoneSilhouette;
		public string pillSpawn, heartSpawn;
		public float blockYOffset, gravesYOffset;
    [Tooltip ("This shall be the parent of the spawned blocks.")]
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
				camTrack = camera.GetComponent<Camera_Tracker> ();
				blocks = new List<GameObject> ();
				graves = new List<GameObject> ();
				heartsList = GameObject.Find ("LifeCounter").GetComponent<Lives> ();
				pillsList = GameObject.Find ("PillCounter").GetComponent<Lives> ();
		}
        public void getHook()
        {
            hook = GetComponentInChildren<GrapplingHook>();
        }
		private bool possibleToSpawn (GameObject obj, float yoff)
		{
				Debug.DrawRay (transform.position, new Vector3 (0, -(yoff + obj.renderer.bounds.size.y), 0), Color.magenta, 5f);
				if (Physics2D.Raycast (transform.position, Vector3.down, yoff + obj.renderer.bounds.size.y, 1 << 8)) {
						return false;
				}
				return true;
		}
		private GameObject createBlock (GameObject obj,GameObject silhouette, float yoff)
		{
				if (possibleToSpawn (obj, yoff)) {
                    try
                    {
                        hook.removeHook();
                    }
                    catch
                    {
                        getHook();
                        hook.removeHook();
                    }
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
                    GameObject g=(GameObject)Instantiate(silhouette, new Vector3(transform.position.x, transform.position.y-yoff-obj.renderer.bounds.size.y/2,transform.position.z-5), Quaternion.identity);
                    g.transform.parent=parent;
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


                            GameObject g=createBlock(Block,BlockSilhouette, blockYOffset);
                            if (g!=null) {
                                blocks.Add(g);
                                pillsList.suicide();
                            }
                            else {
                                pillsList.flash();
                            }
                        }
                        else {
                            pillsList.flash();
                        }


                    } 
                    if (Input.GetButtonDown(heartSpawn)&&!p.getMovementDisabled()) {
						if (heartsList.getLivesLeft () > 0) {
								
										GameObject g = createBlock (Gravestone,GravestoneSilhouette, gravesYOffset);
										if (g != null) {
												graves.Add (g);
												camTrack.setTarget ((graves [graves.Count - 1]).transform);
												Invoke ("revertTarget", maximumDelay);
												trackingPlayer = false;
						p.setMovementDisabled (true);
                        p.renderer.enabled=false;
												foreach (Transform child in transform) {
														try {
																child.renderer.enabled = false;
														} catch {
														}
												}
												collider2D.enabled = false;
										} else {
						heartsList.flash();
										}
								}
                                else {
                                    heartsList.flash();
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
                renderer.enabled=true;
				foreach (Transform child in transform) {
						try {
								child.renderer.enabled = true;
						} catch {
						}
				}
				collider2D.enabled = true;
				GetComponent<Player_Movement> ().setMovementDisabled (false);
		}
}