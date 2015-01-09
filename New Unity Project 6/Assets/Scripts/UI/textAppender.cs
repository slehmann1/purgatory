using UnityEngine.UI;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Text))]
public class textAppender : MonoBehaviour
{
    private Text t;
    private float value;
    public string appender;

    private void setText()
    {


        GetComponent<Text>().text = Mathf.Round(value) + appender;





    }
    public void setValue(float i)
    {
        value = i;
        setText();
    }
}
