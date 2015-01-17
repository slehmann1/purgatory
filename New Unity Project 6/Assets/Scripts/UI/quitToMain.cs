using UnityEngine;
using System.Collections;

public class quitToMain : MonoBehaviour {
     
    public void quit()
    {
        Application.LoadLevel(0);
    }
}
