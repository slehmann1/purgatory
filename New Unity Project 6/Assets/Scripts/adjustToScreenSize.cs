using UnityEngine;
using System.Collections;

public class adjustToScreenSize : MonoBehaviour {
    public Camera cam;
	// Use this for initialization
	void Start () {
        float height = cam.orthographicSize ;
        float width = height * Screen.width / Screen.height;
        transform.localScale = new Vector3(width/renderer.bounds.extents.x, height/renderer.bounds.extents.y, 0.1f);
	}

	
}
