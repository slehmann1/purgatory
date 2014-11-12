using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
public class loadGameSetup : MonoBehaviour {
    public Text [] saves;
    void Start() {
        string [] fileNames=Directory.GetFiles(Application.persistentDataPath+"/saves/");
        for (int i=0; i<fileNames.Length; i++ ) {
            fileNames[i]=fileNames [i].Replace(Application.persistentDataPath+"/saves/", "");
            fileNames[i]=fileNames [i].TrimEnd(".save".ToCharArray());
            saves [i].text=fileNames [i];
        }
        int x=fileNames.Length;
        while(x<3){//totally didn't design it this way to get a heart in...
            saves [x].transform.parent.gameObject.SetActive(false);
            x++;
        }
    }
}
