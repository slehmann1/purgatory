using UnityEngine.UI;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Text))]
public class percentageScript : MonoBehaviour {
    private Text t;
    private float volume;
    void Start() {
        t=GetComponent<Text>();
    }
    private void setText() {
        t.text=Mathf.Round(volume)+" %";
         

    }
    public void setVolume(float i) {
        volume=i;
        setText();
    }
}
