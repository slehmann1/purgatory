using UnityEngine;
using System.Collections;

public class pauseMenu : MonoBehaviour
{
    public string button;
    private Animator anim;
    private bool active;
    private static float origTimeScale = 1f;
    public countdownAnimator ca;
    public void deactivate()
    {
        anim.SetBool("Active", false);
        ca.run();
    }
    public static void resetTimeScale()
    {
        Time.timeScale = origTimeScale;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void activate()
    {
        ca.reset();
        anim.SetBool("Active", true);
        origTimeScale = Time.timeScale;
        Time.timeScale = 0;
       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
            if (active)
            {
                activate();
            }
            else
            {
                deactivate();   
            }
        }
    }

}
