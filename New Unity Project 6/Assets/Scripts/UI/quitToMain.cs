using UnityEngine;
using System.Collections;

public class quitToMain : MonoBehaviour {
    public void quit()
    {
        pauseMenu.resetTimeScale();
        Application.LoadLevel(0);
    }
}
