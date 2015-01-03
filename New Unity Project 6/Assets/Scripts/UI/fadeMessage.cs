using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fadeMessage : MonoBehaviour
{
    public string[] messages;
    private int prevMessage = 0; //TODO save this value
    public Text message;
    private bool activated = false;
    private float origTimeScale;
    public CanvasGroup canvas;
    [Tooltip("The speed at which the screen fades in")]
    public float fadeFactor;


    void Start()
    {
        origTimeScale = Time.timeScale;
    }
    public void activate()
    {
        canvas.gameObject.SetActive(true);
        canvas.alpha = 0;
        setText();
        Time.timeScale = 0f;
        activated = true;
        StartCoroutine("fadeIn");
        prevMessage += 1;
    }
    IEnumerator fadeIn()
    {

        while (canvas.alpha < 1)
        {
            canvas.alpha += (fadeFactor * Time.unscaledDeltaTime);// has to be unscaled delta time - time scale is set to 0
            if (canvas.alpha > 1)
            {
                canvas.alpha = 1;
            }
            yield return null;

        }

    }
    private void setText()
    {
        if (prevMessage < messages.Length)
        {
            message.text = messages[prevMessage];
        }
        else
        {
            prevMessage = 0;
            setText();
        }
        StartCoroutine("checkForClick");
    }
    IEnumerator checkForClick()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                continueGame();
            }
            yield return null;
        }
    }
    public void continueGame()
    {
        StopCoroutine("checkForClick");
        canvas.gameObject.SetActive(false);
        Time.timeScale = origTimeScale;
    }
}
