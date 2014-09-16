using UnityEngine;
using System.Collections;
public class CarDecelerator : MonoBehaviour {
	public GameObject endPoint;
	public float endSpeed,amountOfTime;
	public float getEndSpeed(){
		return endSpeed;
	}
	public GameObject getEndPoint(){
		return endPoint;
	}
	public float getAmountOfTime(){
		return amountOfTime;
	}
}
