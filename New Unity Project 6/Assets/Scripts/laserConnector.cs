using UnityEngine;
using System.Collections;
using System;

public class laserConnector : MonoBehaviour {
    public Transform endPoint, startPoint,player;
    private float originalWidth;
    private bool flipped = false;
    void Start()
    {
        originalWidth = renderer.bounds.extents.x;
    }
    public void flip()
    {
        flipped = !flipped;
    }
    // Update is called once per frame
	void Update () {
        try
        {
           
            //transform.position = startPoint.position;
            transform.position = Vector3.Lerp(startPoint.position,endPoint.position,0.5f);
            if (flipped)
            {
                transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z*-1);
            }
            transform.rotation = Quaternion.Euler(0f, 0f, (getAngle(endPoint.position, startPoint.position) * 180 / Mathf.PI));
            transform.localScale = new Vector3(Vector3.Distance(startPoint.position,endPoint.position)/ player.transform.lossyScale.x, transform.localScale.y);
        
        }
        catch { }


	}
    /// <summary>
    /// returns the angle betwwen two points, accounting for quadrants
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2">This point is defined as the Origin</param>
    /// <returns></returns>
    private float getAngle(Vector2 point1, Vector2 point2)
    {
        float ang = Mathf.Atan(Mathf.Abs((point1.y - point2.y) / (point1.x - point2.x)));
        if (float.IsNaN(ang))
        {
            throw new Exception("The difference between the points to connect is too low. (floating point precision issue)");
        }
        if (point1.x > point2.x)
        {
            if (point1.y > point2.y)
            {
                //quad 1
            }
            else
            {
                //quad 4
                ang = (Mathf.PI) - ang + (Mathf.PI);
            }
        }
        else
        {
            if (point1.y > point2.y)
            {
                //quad 2
                ang = (Mathf.PI / 2) - ang + (Mathf.PI / 2);
            }
            else
            {
                //quad 3
                ang = Mathf.PI + ang;
            }
        }
        return ang;
    }
}
