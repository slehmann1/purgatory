using UnityEngine;
using System.Collections;

public abstract class ButtonComboBase : MonoBehaviour {

    public KeyCode [] combo;
    public bool repeatable;
    private int currentPosition=0;

    void Start() {
        currentPosition=0;
    }
    public void activate() {
        Debug.Log("THAT COMBO WAS PRESSED");
    }
	void Update () {
        if (Input.anyKey&&Input.inputString!="") {
                if (Input.inputString==combo [currentPosition].ToString().ToLower()) {
                    currentPosition++;
                    if (currentPosition==combo.Length) {
                        if (!repeatable) {
                            activate();
                            Destroy(this);
                        }
                        else {
                            activate();
                            currentPosition=0;
                        }
                    }
                }
                else {
                    currentPosition=0;
                }
            
        }
	}
}
