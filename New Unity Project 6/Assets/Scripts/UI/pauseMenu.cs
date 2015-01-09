using UnityEngine;
using System.Collections;

public class pauseMenu : MonoBehaviour
{
    public string button;
    private Animator anim;
    private bool active;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
            if (active)
            {
                anim.SetBool("Active", true);
            }
            else
            {
                anim.SetBool("Active", false);
            }
        }
    }

}
