using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class saveManager : MonoBehaviour {
    public Text t;

    public void createNewGame() {
        string s=t.text;
        s=s.Replace(' ','_');
        foreach(char c in Path.GetInvalidFileNameChars()){
            s=s.Replace(c,'_');
            Debug.Log(c);
        }
      
        BinaryFormatter bf=new BinaryFormatter();
        FileStream file=File.Create(Application.persistentDataPath+"/" +s+".save");
        playerSave save=new playerSave();
        save.fileName=s;
        bf.Serialize(file,save);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
