using UnityEngine;
using System.Collections;
using System;
public class GrapplingEnd : MonoBehaviour
{
    private Rigidbody2D rig;
    private GameObject stem, connectedObj;
    private BoxCollider2D coll;
    private bool active;
    private Vector2 position;
    private Quaternion rot, initRot;
    private bool contacted;
    private DistanceJoint2D dist, objConnection;
    private LayerMask layersToGrapple;
    private const float connectedObjectDistance = 0.01f;
    private ParticleSystem parts;

    public void setLayersToGrapple(LayerMask l)
    {
        layersToGrapple = l;
    }
    public void setRange(float range)
    {
        dist.distance = range;
    }
    /// <summary>
    /// spawns without updating the rotation
    /// </summary>
    public void updateLength()
    {
        //coll.enabled=true;
        renderer.enabled = true;
        active = true;
        dist.enabled = true;
        parts.enableEmission = true;
    }

    public void Spawn()
    {
        rig = rigidbody2D;
        rig.isKinematic = false;
        transform.parent = stem.transform.parent.transform.parent;
        coll.enabled = true;
        renderer.enabled = true;
        active = true;
        contacted = false;
        dist.enabled = true;
        parts.enableEmission = true;
    }
    public void deactivate()
    {
        try
        {
            coll.enabled = false;
            renderer.enabled = false;
            active = false;
            dist.enabled = false;
            parts.enableEmission = false;
            objConnection.enabled = false;
        }
        catch
        {

        }
        // rigidbody2D.isKinematic=false;
    }
    public void Setup(GameObject newStem)
    {
        dist = GetComponent<DistanceJoint2D>();
        stem = newStem;
        dist.enabled = true;
        dist.connectedBody = transform.parent.transform.parent.rigidbody2D;
        dist.enabled = false;
        transform.parent = stem.transform.parent.transform.parent;
        parts = GetComponent<ParticleSystem>();
    }
    // Use this for initialization
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
        objConnection = gameObject.AddComponent<DistanceJoint2D>();
        objConnection.enabled = false;
        deactivate();
    }
    void UpdatePos()
    {
        try
        {
            if (!contacted)
            {//ifn the end isnt fixed-> set the pos/rot to the end of the stem
                position = stem.transform.TransformPoint(new Vector2(0.5f, 0f));//set the world position to the local position of 0.5,0.5 in the bases space
            }
        }
        catch { }
    }
    public GameObject getConnectedObject()
    {
        return connectedObj;
    }
    void attachToObj()
    {
        try
        {
            if (connectedObj.rigidbody2D)
            {
                objConnection.connectedBody = connectedObj.rigidbody2D;
                objConnection.connectedAnchor = connectedObj.transform.InverseTransformPoint(transform.position);
                rig.isKinematic = false;
            }
            else
            {
                objConnection.connectedAnchor = transform.position;
            }
        }
        catch
        {
            objConnection.connectedAnchor = transform.position;
        }
        objConnection.distance = connectedObjectDistance;
        objConnection.enabled = true;
        coll.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && !contacted && (((1 << collision.gameObject.layer) & layersToGrapple.value) > 0))
        {//last condition is to check if they are the same layer
            transform.parent = collision.transform;
            rigidbody2D.isKinematic = true;
            contacted = true;
            connectedObj = collision.gameObject;
            attachToObj();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!contacted)
        {
            UpdatePos();
        }
    }
}
