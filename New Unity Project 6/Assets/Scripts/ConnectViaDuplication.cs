using UnityEngine;
using System.Collections.Generic;
public class ConnectViaDuplication : MonoBehaviour {
	public Vector3 startPoint,endPoint;
	public GameObject connector;
	public float scale,spacing;
	public int number;
	public bool delChildren;
    public float distanceJointDist;
	public bool hingeJointSetup,collideWithConnected, chooseNumber,fixedEnds;
	public enum mode{chooseNumber, chooseSpacing};
    public mode modeChoice;
    public enum fillOption {increaseSizeToFill,addExtra,noGaps,spaceEvenly};
    public fillOption fillChoice;

	private List<GameObject> objs{
		get{
			return objs;
		}
		set{
			objs=value;
		}
	}
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}
}
