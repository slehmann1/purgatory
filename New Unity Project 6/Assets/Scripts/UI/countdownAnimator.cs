using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class countdownAnimator : MonoBehaviour {
    Text t;
    Shadow s;
    [Tooltip("The message displayed after the countdown")]
    public string finalMessage;
    [Tooltip("The time for go to fade in seconds")]
    public float fadeTime;
    private float originalShadowColor;

    void Awake()
    {
        t = GetComponent<Text>();
        s = GetComponent<Shadow>();
        originalShadowColor = s.effectColor.a;
    }
    public void run()
    {
       StartCoroutine( changeText());
    }
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
    public void reset()
    {
        StopAllCoroutines();
        returnToNormal();
    }
    private void returnToNormal()
    {
         t.enabled = false;
        t.color = new Color(t.color.r, t.color.g, t.color.b, 1);
        s.effectColor = new Color(s.effectColor.r, s.effectColor.g, s.effectColor.b, originalShadowColor);
        }
    IEnumerator fadeText()
    {
        float fadeStep = 1f / fadeTime;
        Debug.Log(fadeStep);
        while (t.color.a != 0)
        {
            Debug.Log(t.color);
            Debug.Log(Time.unscaledDeltaTime);
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a - (fadeStep*Time.unscaledDeltaTime));
            s.effectColor = new Color(s.effectColor.r, s.effectColor.g, s.effectColor.b, s.effectColor.a-(fadeStep*Time.unscaledDeltaTime));
            if(t.color.a<0){
                t.color = new Color(t.color.r, t.color.g, t.color.b, 0);
                s.effectColor = new Color(s.effectColor.r, s.effectColor.g, s.effectColor.b, 0);
            }
            
            yield return null;
        }
        returnToNormal();
    }
    IEnumerator changeText()
    {
        t.enabled = true;
        
        for (int i = 3; i >=1; i--)
        {
            t.text = i.ToString();
            yield return StartCoroutine(WaitForRealSeconds(1f));
        }
        t.text = "go!";
        pauseMenu.resetTimeScale();
        yield return StartCoroutine(WaitForRealSeconds(0.1f));
        StartCoroutine(fadeText());
    }
}
