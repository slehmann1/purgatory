using UnityEngine;
using System.Collections;

public class speedChangeEasterEgg : ButtonComboBase {
    private Player_Movement p;
    private const float speedChange=1f;
    private float speedMultiple;
    private bool currActive=false;
    private float initMaxSpeed, initMoveForce;
    public void Start() {
        p=GameObject.Find("Player").GetComponent<Player_Movement>();
        speedMultiple=p.maxSpeed/p.moveForce;
        initMaxSpeed=p.maxSpeed;
        initMoveForce=p.moveForce;
    }
    void Update() {
        base.Update();
        if (currActive) {
            p.maxSpeed+=speedChange/speedMultiple;
            p.moveForce+=speedChange*speedMultiple;
        }
    }
    public override void activate() {
        if (currActive) {
            currActive=false;
            reset();
            Debug.Log("RESET");
        }
        else {
            currActive=true;
        }
    }
    private void reset() {
        p.moveForce=initMoveForce;
        p.maxSpeed=initMaxSpeed;
    }
}
