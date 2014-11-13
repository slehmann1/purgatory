using UnityEngine;
using System.Collections;

public class rotator : MonoBehaviour {
    public float speed;
	
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0.9f,0.5f,0.5f),speed*Time.deltaTime);
	}
}
