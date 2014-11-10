using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class antiAliasingText : MonoBehaviour {
    private Text t;
    private int step;
    void Start() {
        t=GetComponent<Text>();
    }
    private void setText() {
        string s="Off";
        switch (step) {
            case 1:
                s="2";
                break;
            case 2:
                s="4";
                break;
            case 3:
                s="8";
                break;
        }
        if (step!=0) {
            s+=" times";
        }
        t.text=s;


    }
    public void setAliasing(float i) {
        step=(int)i;
        setText();
    }
}
