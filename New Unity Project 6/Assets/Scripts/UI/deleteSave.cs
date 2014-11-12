using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
public class deleteSave : MonoBehaviour {
    public Text save1, save2, save3;
    private string [] fileNames;
    public saveManager sAndM;
    private string laterText;

    public void setTextAfter(string s) {
        laterText=s;
    }
	// Use this for initialization
	void Start () {
        fileNames=Directory.GetFiles(Application.persistentDataPath+"/saves/");
        save1.text=fileNames [0];
        save2.text=fileNames [1];
        save3.text=fileNames [2];
	}
    public void delete(int i) {
        File.Delete(fileNames[i]);
        sAndM.create(laterText);
    }

}
