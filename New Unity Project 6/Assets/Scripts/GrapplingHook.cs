using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
[RequireComponent(typeof(ObjectPooler))]
public class GrapplingHook : MonoBehaviour {
    private bool ignoreLimits;
    public float range;
    private float angle, gap;
    public float changeSpeed;
	public float limit;
    Vector2 target, oldTarget;
    public GameObject obj;
    private GameObject player;
    private RaycastHit2D targetCast;
    private bool hasHitObj;
    bool forwards, grappling;
    public GameObject end;
    private GrapplingEnd endScript;
    private GrapplingStalk objMid;
    private Player_Movement pMov;
    public float minimumDistance;
    public bool invertedScroll;
    private TrailRenderer trail;
    private HingeJoint2D endJoint;
    void Start() {
        obj=(GameObject)GameObject.Instantiate(obj);
        //		trailSub = transform.GetChild (0).gameObject;
        trail=obj.GetComponent<TrailRenderer>();
        player=transform.parent.transform.parent.gameObject;
        pMov=player.GetComponent<Player_Movement>();
        obj.collider2D.enabled=false;
        obj.transform.parent=transform;
        obj.GetComponent<HingeJoint2D>().anchor=new Vector2(-0.5f, 0);
        obj.GetComponent<HingeJoint2D>().connectedBody=player.rigidbody2D;
        obj.collider2D.enabled=true;
        objMid=obj.GetComponentInChildren<GrapplingStalk>();
        obj.SetActive(false);
        end=(GameObject)GameObject.Instantiate(end);
        end.transform.parent=transform.parent;
        endScript=end.GetComponent<GrapplingEnd>();
        endScript.Setup(obj);
        endJoint=obj.AddComponent<HingeJoint2D>();
        endJoint.anchor=new Vector2(0.5f, 0);
        endJoint.connectedBody=end.rigidbody2D;
    }
    /// <summary>
    /// Resets the trail renderer.
    /// </summary>
    IEnumerator resetTrailRenderer() {
        float time=trail.time;
        trail.time=0;
        yield return null;
        trail.time=time;
    }
    /// <summary>
    /// removes a grappling hook
    /// </summary>
    void removeLine() {
        //		objEnd.stop ();
        obj.SetActive(false);
        endScript.deactivate();
    }
    /// <summary>
    /// creates a grappling hook
    /// </summary>
    void grapple() {
        
        StartCoroutine(resetTrailRenderer());

        grappling=true;
        angle=Mathf.Atan(Mathf.Abs((target.y-transform.position.y)/(target.x-transform.position.x)));
        angle=getAngle(target,transform.position);

        connect(obj.transform, transform.position, target);
        obj.SetActive(true);
        endScript.Spawn();
        endScript.setRange(Vector3.Distance(target, transform.position));
        end.transform.position=target;
        
        //if the player hooks on to something above him, do not need to check limits
        if (target.y>transform.position.y){
            ignoreLimits=true;
        }
        else {
            if (hasHitObj) {
                GameObject blockTouched=Physics2D.OverlapCircle(target, 1f, 1<<LayerMask.NameToLayer("level")).gameObject;
                if (blockTouched) {//it is a possibility that there is no block within the circle
                    ignoreLimits=false;
                    //if the player is beyond the horizontal edge of the block
                    if (Physics2D.Raycast(transform.position, Vector3.down, range, 1<<LayerMask.NameToLayer("level")).transform.gameObject!=blockTouched) {
                        ignoreLimits=true;
                    }
                }
            }
            
            
                
        }

        //if this is not done, sometimes the hinge joint does not do anything
        endJoint.enabled=false;
        endJoint.enabled=true;
    }
    public void flip() {
        if (!hasHitObj) {
            float newX=-1*(end.transform.position.x-transform.position.x)+transform.position.x;
            transform.Rotate(new Vector3(0, 180, 0));
            end.transform.position=new Vector3(newX, end.transform.position.y, end.transform.position.z);//prevents the hook from shifting closer and closer to 90 degrees up
        }
        else {
            transform.Rotate(new Vector3(0, 180, 0));
        }
            //set position to behind the player
        transform.position=new Vector3(transform.position.x, transform.position.y, -transform.position.z);

    }
    /// <summary>
    /// removes thegrappling hook if it exists
    /// </summary>
    public void removeHook() {
        if (grappling) {
            pMov.setGrappling(false);
            removeLine();
            grappling=false;
        }
    }
    /// <summary>
    /// Connects two objects
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    void connect(Transform obj, Vector3 start, Vector3 end) {
        //this is where the math starts
        //setting the position to halfway between the beginning and the end
        obj.position=Vector3.Lerp(start, end, 0.5f);
        Debug.DrawLine(start, obj.position, Color.red, 2f, false);
        //getting the scale
        float scale=Vector3.Distance(start, end);
        // scale*=2f;


        obj.transform.localScale=new Vector3(scale/player.transform.lossyScale.x, obj.transform.localScale.y);

        if (scale<0) {
            Debug.LogWarning("The spacing is too high!");
        }
        //trig
        float degrees=Mathf.Atan((end.y-start.y)/(end.x-start.x));
        obj.localRotation=Quaternion.Euler(0, 0, angle*180/Mathf.PI);
    }
    void Update() {
        if (Input.GetButtonDown("Create_Grappling_Hook")&&!pMov.getMovementDisabled()) {
            if (!grappling) {
                Vector3 newVec=transform.position-Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                newVec*=-1;
                newVec.Normalize();
                removeLine();
                if (Physics2D.Raycast(transform.position, newVec, range, 1<<8)) {
                    targetCast=Physics2D.Raycast(transform.position, newVec, range, 1<<8);
                    oldTarget=targetCast.point;
                    hasHitObj=true;
                    pMov.setGrappling(true);
                }
                else {
                    hasHitObj=false;
                    oldTarget=transform.position+(newVec*range);
                }
                target=oldTarget;
                Debug.DrawLine(transform.position, (target), Color.green, 1.0f, true);
                grapple();
            }
        }
        if (Input.GetButtonDown("Release_Grappling_Hook")) {
            pMov.setGrappling(false);
            removeLine();
            grappling=false;
        }
        if (grappling) {
            if (!hasHitObj) {
                Vector3 newVec=transform.position-end.transform.position;
                newVec*=-1;
                newVec.Normalize();
                if (Physics2D.Raycast(transform.position, newVec, range, 1<<8)) {
                    targetCast=Physics2D.Raycast(transform.position, newVec, range, 1<<8);
                    oldTarget=targetCast.point;
                    hasHitObj=true;
                    target=oldTarget;
                    removeLine();
                    grapple();
                }
            }
        }
        if (Input.GetAxis ("Change_Grappling_Hook_Length") != 0 && grappling) {

                if (checkLimits()) {


                    
                    Debug.DrawLine(transform.position, end.transform.position, Color.red, 5f, false);
                    if (hasHitObj) {
                        if (Input.GetAxis("Change_Grappling_Hook_Length")>0) {

                            if (Vector3.Magnitude(player.transform.position-end.transform.position)>minimumDistance) {
                                changeLength(true);
                            }
                        }
                        else {
                            if (Vector3.Magnitude(player.transform.position-end.transform.position)<range) {
                                changeLength(false);
                            }
                        }
                    }
                    else {
                        if (Input.GetAxis("Change_Grappling_Hook_Length")>0) {

                            if (Vector3.Magnitude(player.transform.position-end.transform.position)>minimumDistance) {
                             //   changeLengthWithoutPlayerMovement(true);
                            }
                        }
                        else {
                            if (Vector3.Magnitude(player.transform.position-end.transform.position)<range) {
                              //  changeLengthWithoutPlayerMovement(false);
                            }
                        }
                    }
                }
            
				}
    }
    private bool checkLimits() {
        if (ignoreLimits) {
            return true;
        }
        //if above limit
        float currAngle=getAngle(end.transform.position, transform.position)*180/Mathf.PI;
        currAngle+=180;
        if (currAngle>360) {
            currAngle-=360;
        }
        if (currAngle>limit) {
            return true;
        }
        else {
            return false;
        }
    }
    /// <summary>
    /// returns the angle betwwen two points, accounting for quadrants
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2">This point is defined as the Origin</param>
    /// <returns></returns>
    private float getAngle(Vector2 point1, Vector2 point2) {
        float ang=Mathf.Atan(Mathf.Abs((point1.y-point2.y)/(point1.x-point2.x)));
        if (float.IsNaN(ang)) {
            throw new Exception("The difference between the points to connect is too low. (floating point precision issue)");
        }
        if (point1.x>point2.x) {
            if (point1.y>point2.y) {
                //quad 1
            }
            else {
                //quad 4
                ang=(Mathf.PI)-ang+(Mathf.PI);
            }
        }
        else {
            if (point1.y>point2.y) {
                //quad 2
                ang=(Mathf.PI/2)-ang+(Mathf.PI/2);
            }
            else {
                //quad 3
                ang=Mathf.PI+ang;
            }
        }
        return ang;
    }
    /// <summary>
    /// changes the length of the grappling hook without moving the player
    /// </summary>
    void changeLengthWithoutPlayerMovement(bool towardsCenter) {
        StartCoroutine(resetTrailRenderer());
            if (invertedScroll) {
                towardsCenter=!towardsCenter;
            }
            Vector3 newVec=player.transform.position-end.transform.position;
            newVec*=-1;
            newVec.Normalize();
            
            if (towardsCenter) {
                //towards center
                newVec*=changeSpeed;
                if (Vector2.Distance(player.transform.position+newVec, end.transform.position)>=minimumDistance) {
                    target-=(Vector2)(newVec);
                }
            }else {
                //away from hook
                if (Vector2.Distance(player.transform.position-newVec, end.transform.position)>=minimumDistance) {
                    if (Vector2.Distance((target+(Vector2)newVec), transform.position)<=range) {
                        newVec*=changeSpeed;
                        target+=(Vector2)(newVec);
                    }
                    else {
                        //set it to as far away as possible
                        target=transform.position+(newVec*range);
                        Debug.Log(Vector2.Distance(target, transform.position));
                    }
                }
            }
            removeLine();
            try {
                angle=getAngle(target, transform.position);
                connect(obj.transform, transform.position, target);
                obj.SetActive(true);
                endScript.updateLength();
                endScript.setRange(Vector3.Distance(target, transform.position));
                end.transform.position=target;
            }
            catch {
            }
        
        //if this is not done, sometimes the hinge joint does not do anything
        endJoint.enabled=false;
        endJoint.enabled=true;

    }
    /// <summary>
    /// Changes the length of the grapplingHook by moving the player
    /// </summary>
    /// <param name="towardsCenter"></param>
    void changeLength(bool towardsCenter) {
	
        StartCoroutine(resetTrailRenderer());
        //this prevents clipping through ground
        if (!(!towardsCenter&&pMov.isGrounded())) {
            if (invertedScroll) {
                towardsCenter=!towardsCenter;
            }
            //progress += (Input.GetAxis ("Change_Grappling_Hook_Length") * Time.deltaTime * changeSpeed);
            Vector3 newVec=player.transform.position-end.transform.position;
            newVec*=-1;
            newVec.Normalize();
            newVec*=changeSpeed;
            if (towardsCenter) {
                //towards center
                if (Vector2.Distance(player.transform.position+newVec, end.transform.position)>=minimumDistance)
                    player.transform.position+=(newVec);
            }
            else {
                //away from hook
                if (Vector2.Distance(player.transform.position-newVec, end.transform.position)>=minimumDistance)
                    player.transform.position-=(newVec);
            }
            removeLine();
            try {
                angle=getAngle(target,transform.position);
                connect(obj.transform, transform.position, target);
                obj.SetActive(true);
                endScript.updateLength();
                endScript.setRange(Vector3.Distance(target, transform.position));
                end.transform.position=target;
            }
            catch {
            }
        }
		//if this is not done, sometimes the hinge joint does not do anything
		endJoint.enabled=false;
		endJoint.enabled=true;
    }
}
