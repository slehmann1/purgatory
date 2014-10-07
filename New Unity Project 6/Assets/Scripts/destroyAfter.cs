using UnityEngine;
using System.Collections;

public class destroyAfter : MonoBehaviour {
    public float timeTilDestruction;
    public float timeBetweenAlpha;
    public float alphaStep;
    public void Start() {
        StartCoroutine("fade");
        StartCoroutine("destroy");
    }
    IEnumerator fade() {
        Color c=gameObject.renderer.material.color;
        while (gameObject.renderer.material.color.a>0.0f) {
            c.a-=alphaStep;
            gameObject.renderer.material.color=c;
            yield return new WaitForSeconds(timeBetweenAlpha);
    }
    }
    IEnumerator destroy() {
        yield return new WaitForSeconds(timeTilDestruction);
        Destroy(gameObject);
    }
}
