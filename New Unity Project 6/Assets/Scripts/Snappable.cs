using UnityEngine;
using System.Collections;

public abstract class Snappable : MonoBehaviour {
    public bool snapOnExplode = true;
    public abstract void destroy();
    
}
