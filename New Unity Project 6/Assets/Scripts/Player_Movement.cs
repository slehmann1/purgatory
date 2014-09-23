using UnityEngine;
using System.Collections.Generic;
public class Player_Movement : MonoBehaviour
{
		//on death a snarky comment like- the players heart has stopped beating, this kills the player
		public GameObject doubleJumpPref;
		public bool facingRight = true;
		public float wallJumpForce, wallJumpAngle;
		public float moveForce = 365f;
        public float grapplingMovement,airMovement;// Amount of force added to move the player left and right.
		public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
		public float jumpForce = 1000f;
		public float clingForce = 400f;
		public float dubJumpForce = 100f;//force added on jump
		private Transform groundCheck;
		public Transform[] leftWallCheck, rightWallCheck;	// A position marking where to check if the player is grounded.
		private bool grounded = false;
		private bool grappling=false;
		private bool jump = false;
		public float speed;
		private bool dubjump = false;
		private bool  WallJumping, leftWallJump, clinging, clingJumping, onWall;//prevents continuous wall jumping
		public PhysicsMaterial2D jumping, normal, stopping, clingingJump;
		private BoxCollider2D coll;
		private Vector3 old;
		private string text = "Running";
		private DoubleJumpParticle doubleJumpParticle;
		private Animator animator;
		private bool movementDisabled;
		private Vector3 movementStop;
		public Vector3 spawn;
		private SpawnEffect spawnEffect;
        private GrapplingHook hook;
		void Start ()
		{
            hook=GetComponentInChildren<GrapplingHook>();
				animator = GetComponent<Animator> ();
		spawnEffect=gameObject.GetComponentInChildren<SpawnEffect>();
				//doubleJumpParticle = GameObject.Find ("doubleJump").GetComponent<DoubleJumpParticle> ();
				spawn = transform.position;
		}
        public bool isGrounded() {
            return grounded;
        }
		public void respawn ()
		{
				transform.position = spawn;
				rigidbody2D.velocity = Vector2.zero;
				rigidbody2D.angularVelocity = 0;
		spawnEffect.spawn();
		}
		void Awake ()
		{
				// Setting up references.
		groundCheck = transform.Find ("groundCheck");
				coll = (BoxCollider2D)GetComponent ("BoxCollider2D");
		}
		// Update is called once per frame
		void Clinging ()
		{
				text += "CLINGING!";
		}
		void Update ()
		{
				if (!movementDisabled) {
						onWall = false;
						clinging = false;
						text = "";
						// Check if grounded
						bool wallJump = false;
						grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("level"));  
						if (Physics2D.Linecast (transform.position, leftWallCheck [0].position, 1 << LayerMask.NameToLayer ("level"))) {
								leftWallJump = true;
								//Debug.DrawLine (transform.position, leftWallCheck [1].position, Color.red, 0.01f, true);
							//	Debug.DrawLine (transform.position, leftWallCheck [0].position, Color.green, 0.01f, true);
								wallJump = true;
								dubjump = false;
								onWall = true;
						} else 
			if (Physics2D.Linecast (transform.position, leftWallCheck [1].position, 1 << LayerMask.NameToLayer ("level"))) {
								clinging = true;
								onWall = true;
								dubjump = false;
								Clinging ();
						} else		if (Physics2D.Linecast (transform.position, rightWallCheck [0].position, 1 << LayerMask.NameToLayer ("level")) || Physics2D.Linecast (transform.position, rightWallCheck [1].position, 1 << LayerMask.NameToLayer ("level"))) {
								wallJump = true;
								onWall = true;
								dubjump = false;
								leftWallJump = false;
						} 
			if(!grappling){
						if (wallJump && (Input.GetButtonDown ("Jump") && !grounded)) {
								WallJumping = true;
						} else if (Input.GetButtonDown ("Jump") && !dubjump) {
								if (!grounded && !clinging) {
										dubjump = true;
								}
								jump = true;
						}
			}
						movement ();
				} else {
						transform.position = movementStop;
						rigidbody2D.velocity = Vector3.zero;
				}
		}
		public void setMovementDisabled (bool disabled)
		{
				movementDisabled = disabled;
				if (movementDisabled) {
						movementStop = transform.position;
				}
		}
		public bool getMovementDisabled ()
		{
				return movementDisabled;
		}
		void FixedUpdate ()
		{
				if (!movementDisabled) {
						if (WallJumping) {
								if (leftWallJump) {
										wallJump (wallJumpAngle, true);
								} else {
										wallJump (wallJumpAngle, false);
								}
								WallJumping = false;
						}
						if (jump) {//first jump
								if (dubjump) {
										// Add a vertical force to the player.
										jumpUp (dubJumpForce);
										doubleJump ();
								} else if (clinging) {
										clingJumping = true;
										jumpUp (clingForce);
								} else {
										// Add a vertical force to the player.
										jumpUp (jumpForce);
								}
								// Make sure the player can't jump again until the jump conditions from Update are satisfied.
								jump = false;
						}
						if (grounded) {
								clingJumping = false;
								dubjump = false;
						}
						movement ();
				}
		}
		void movement ()
		{
				text += rigidbody2D.velocity.x;
				animator.SetFloat ("Speed", 0.0f);
				float h = Input.GetAxis ("Horizontal");
				coll.enabled = false;//there is a bug in unity that requires collider to be disabled and then enabled
				//when changing 2d physics materials, check if still there every time unity updates
				if (clingJumping) {
						coll.collider2D.sharedMaterial = clingingJump;
				} else if (h == 0.0 && grounded || clinging) {
						coll.collider2D.sharedMaterial = stopping;
				} else {
						if (grounded) {
								coll.collider2D.sharedMaterial = normal;
						} else {
								coll.collider2D.sharedMaterial = jumping;
						}
				}
				coll.enabled = true;//there is a bug in unity that requires collider to be disabled and then enabled
				//when changing 2d physics materials, check if still there every time unity updates
				if (h * rigidbody2D.velocity.x < maxSpeed) {
						// ... add a force to the player.
                    if (grounded) {
                        rigidbody2D.AddForce(Vector2.right*h*moveForce);
                        animator.SetBool("Jump", false);
                    }
                    else if(!grappling){//jumping
                        animator.SetBool("Jump", true);
						rigidbody2D.AddForce (Vector2.right * h * airMovement);
                    }
                    else {//grappling
                        rigidbody2D.AddForce(Vector2.right*h*grapplingMovement);
                    }
				}
				// If the player's horizontal velocity is greater than the maxSpeed...
				if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed) {
						// ... set the player's velocity to the maxSpeed in the x axis.
						rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
						//			backgroundUpdate.run(1.0f);
				}
						animator.SetFloat ("Speed", Mathf.Abs (h));
				// If the input is moving the player right and the player is facing left...
				if (h > 0 && !facingRight) {
						// ... flip the player.
                    flip();
                  //  transform.localScale=new Vector2(transform.localScale.x*-1, transform.localScale.y);
						facingRight = true;
				}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight) {
						// ... flip the player.
            flip();
				}
				// If the player should jump...
				speed = Input.GetAxis ("Horizontal");
				Vector3 knew = transform.position;
				if (old != knew) {
					//	Debug.DrawRay (old, knew, Color.green, 1);
						old = knew;
				}
		}
    /// <summary>
    /// flips the player horizontally (over the Y axis)
    /// </summary>
        private void flip() {
            transform.Rotate(new Vector3(0, 180, 0));
            hook.flip();
           // transform.localScale=new Vector3(transform.localScale.x,transform.localScale.y);
            
            facingRight=!facingRight;
        }
    /// <summary>
    /// sets whether or not the player is grappling
    /// </summary>
    /// <param name="grapple"></param>
	public void setGrappling(bool grapple){
		if(grapple){
			dubjump=false;
            animator.SetBool("Grappling", true);
        }
        else {
            animator.SetBool("Grappling", false);
        }
		grappling=grapple;
	}
		private void jumpUp (float i)
		{
            animator.SetBool("Jump", true);
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0f);//this sets y velocity to zero, should this be done
				rigidbody2D.AddForce (new Vector2 (0f, i));
		}
		private void doubleJump ()
		{
            animator.SetBool("Jump", true);
				//doubleJumpParticle.Run ();
				Instantiate (doubleJumpPref, transform.position, Quaternion.identity);
		}
		private void wallJump (float angle, bool left)
		{
            animator.SetBool("Jump", true);
				old = transform.position;
				//rigidbody2D.velocity=new Vector2(rigidbody2D.velocity.x,0f);//this sets y velocity to zero, should this be done
				//rigidbody2D.AddForce(new Vector2(1,Mathf.Cos(angle))*wallJumpForce);
				old = transform.position;
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0f);//this sets y velocity to zero, should this be done
				Vector3 dir = Quaternion.AngleAxis (angle, Vector3.forward) * Vector3.right;
				if (!left) {
						dir.x *= -1;
				}
				rigidbody2D.AddForce (dir * wallJumpForce);
				dubjump = false;
				//rigidbody2D.AddForce(new Vector2(1,Mathf.Cos(angle))*wallJumpForce);
				//Debug.DrawRay(transform.position, new Vector3(1,Mathf.Sin(-angle),0)*10,Color.red,5);
		}
		void OnGUI ()
		{
				GUI.Label (new Rect (0, 0, Screen.width, Screen.height), text);
		}
}
