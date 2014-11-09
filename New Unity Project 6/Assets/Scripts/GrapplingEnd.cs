using UnityEngine;
using System.Collections;
public class GrapplingEnd : MonoBehaviour {
    private Rigidbody2D rig;
    private GameObject stem, connectedObj;
    private Transform connectedTransform;
    private BoxCollider2D coll;
    private bool active;
    private Vector3 position, oldConnectedPos,oldPosition;
    private Quaternion rot, initRot;
    private bool contacted;
    private DistanceJoint2D dist, objConnection;
    private const float connectedObjectDistance=0.01f;
    private ParticleSystem parts;
    public bool rotationEnabled;
    private Vector2 offset;
    private int oldConnectedObjLayer;
    private int connectedObjLayer=LayerMask.NameToLayer("ConnectedObject"); // the layer that objects will be changed to
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
        parts.enableEmission=true;
    }

    public void Spawn() {
        rig=rigidbody2D;
        rig.isKinematic=false;
        transform.parent=stem.transform.parent.transform.parent;
        coll.enabled=true;
        renderer.enabled=true;
        active=true;
        contacted=false;
        initRot=transform.rotation;
        dist.enabled=true;
        transform.rotation=initRot;
        parts.enableEmission=true;
    }
    public void deactivate() {
        try {
            coll.enabled=false;
            renderer.enabled=false;
            active=false;
            dist.enabled=false;
            parts.enableEmission=false;
            objConnection.enabled=false;
            connectedObj.layer=oldConnectedObjLayer;
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
    }
    // Use this for initialization
    void Start() {
        coll=GetComponent<BoxCollider2D>();
        coll.enabled=false;
        objConnection=gameObject.AddComponent<DistanceJoint2D>();
        objConnection.enabled=false;
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
    public GameObject getConnectedObject() {
        return connectedObj;
    }
    void attachToObj() {
        contacted=true;
        try {
            objConnection.connectedBody=connectedObj.rigidbody2D;
            objConnection.connectedAnchor=connectedObj.transform.InverseTransformPoint(transform.position);
            rig.isKinematic=false;
        }
        catch {
            objConnection.connectedAnchor=transform.position;
        }
        objConnection.distance=connectedObjectDistance;
       // objConnection.enabled=true;
        offset=transform.position-connectedObj.transform.position;
        connectedTransform=connectedObj.transform;
        oldConnectedPos=connectedTransform.position;
        oldPosition=transform.position;
        oldConnectedObjLayer=connectedObj.layer;
        connectedObj.layer=connectedObjLayer;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (active&&!contacted) {
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
            connectedObj=collision.gameObject;
            attachToObj();
        }
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
                
                oldConnectedPos=connectedTransform.position;
                transform.position=oldConnectedPos;
                oldPosition=transform.position;
        }
    }
}
