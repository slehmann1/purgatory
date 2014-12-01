using UnityEngine;
using System.Collections;

public class laserConnector : MonoBehaviour {
    public Transform endPoint, startPoint;
	// Update is called once per frame
	void Update () {
        try
        {
            endPoint = GameObject.Find("LaserEnd").transform;
            startPoint = transform.parent;
            Debug.Log("DONE");
            transform.position = startPoint.position + ((endPoint.position - startPoint.position) / 2);
        }
        catch { }
	}
}
