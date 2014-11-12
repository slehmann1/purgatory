using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
public class loadGameSetup : MonoBehaviour {
    public Text [] saves;
    void Start() {
		try{
        string [] fileNames=Directory.GetFiles(Application.persistentDataPath+"/saves/");
        for (int i=0; i<fileNames.Length; i++ ) {
            fileNames[i]=fileNames [i].Replace(Application.persistentDataPath+"/saves/", "");
            fileNames[i]=fileNames [i].Substring(0,fileNames[i].LastIndexOf(".save"));
				Debug.Log (fileNames[i]);
            saves [i].text=fileNames [i];
        }
        int x=fileNames.Length;
        while(x<3){//totally didn't design it this way to get a heart in...
            saves [x].transform.parent.gameObject.SetActive(false);
            x++;
        }
		}catch (DirectoryNotFoundException){
			Directory.CreateDirectory(Application.persistentDataPath+"/saves");//create the folder
			Start();
    }
}
}
