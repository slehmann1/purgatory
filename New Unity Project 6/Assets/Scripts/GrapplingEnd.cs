using UnityEngine;
using System.Collections;
public class GrapplingEnd : MonoBehaviour {
    private GameObject stem;
    private BoxCollider2D coll;
    private bool active;
    private Vector2 position;
    private Quaternion rot, initRot;
    private bool contacted;
    private DistanceJoint2D dist;
    private ParticleSystem parts;
    public bool rotationEnabled;
    public void setRange(float range) {
        dist.distance=range;
    }
    /// <summary>
    /// spawns without updating the rotation
    /// </summary>
    public void updateLength() {
        coll.enabled=true;
        renderer.enabled=true;
        active=true;
        dist.enabled=true;
		parts.active=true;
		Debug.Log ("run");
    }

    public void Spawn() {
        rigidbody2D.isKinematic=false;
        transform.parent=stem.transform.parent.transform.parent;
        coll.enabled=true;
        renderer.enabled=true;
        active=true;
        contacted=false;
        initRot=transform.rotation;
        dist.enabled=true;
        transform.rotation=initRot;
        parts.active=true;
		Debug.Log ("run");
    }
    public void deactivate() {
        try {
            coll.enabled=false;
            renderer.enabled=false;
            active=false;
            dist.enabled=false;
            parts.active=false;
			Debug.Log ("run");
        }
        catch {
            
        }
       // rigidbody2D.isKinematic=false;
    }
    public void Setup(GameObject newStem) {
        initRot=transform.rotation;
        dist=GetComponent<DistanceJoint2D>();
        stem=newStem;
        dist.enabled=true;
        dist.connectedBody=transform.parent.transform.parent.rigidbody2D;
        dist.enabled=false;
        transform.parent=stem.transform.parent.transform.parent;
        parts=GetComponent<ParticleSystem>();
		Debug.Log ("run");
    }
    // Use this for initialization
    void Start() {
        coll=GetComponent<BoxCollider2D>();
        coll.enabled=false;
        deactivate();
    }
    void UpdatePos() {
        try {
            if (!contacted) {//ifn the end isnt fixed-> set the pos/rot to the end of the stem
                position=stem.transform.TransformPoint(new Vector2(0.5f, 0f));//set the world position to the local position of 0.5,0.5 in the bases space
                if (rotationEnabled) { 
                rot=stem.transform.rotation;
                    }
            }
        }
        catch { }
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (rotationEnabled) {
            float rotation=Mathf.Atan2(collision.contacts [0].normal.y, (collision.contacts [0].normal.x));//not sure on this,  should calculate the angle in relation to the collider7
            rotation*=180;
            rotation/=Mathf.PI;
            //Debug.Log(rotation);
            rot=Quaternion.Euler(new Vector3(0f, 0f, rotation));
        }
        transform.parent=null;
        rigidbody2D.isKinematic=true;
        transform.localRotation=Quaternion.identity;
        if (!contacted&&rotationEnabled) {//if it has simply changed length, will not update the rotation
            transform.rotation=rot*initRot;
        }
        
        contacted=true;
    }
    // Update is called once per frame
    void Update() {
        if (!contacted) {
            UpdatePos();
            if (rotationEnabled) {
                transform.rotation=rot*initRot;
            }
        }
        else {
        }
    }
}
