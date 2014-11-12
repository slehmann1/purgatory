using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;

[RequireComponent (typeof (Text))]
public class saveManager : MonoBehaviour {
    public Text t;
    private string [] files=new string[3];
    public GameObject overRideSaveSlot;
    public deleteSave nintendoDS;
    public void checkForGames() {
        try {
            files=Directory.GetFiles(Application.persistentDataPath+"/saves/");
        }
        catch (DirectoryNotFoundException){//no saves have been created yet
            Directory.CreateDirectory(Application.persistentDataPath+"/saves");//create the folder
        }
    }

    public void create(string s) {
        if (s=="") {
            throw new Exception("Empty file name exception");
        }
        s=s.Replace(' ', '_');
        foreach (char c in Path.GetInvalidFileNameChars()) {
            s=s.Replace(c, '_');
        }

        BinaryFormatter bf=new BinaryFormatter();
        FileStream file=File.Create(Application.persistentDataPath+"/saves/"+s+".save");
        playerSave save=new playerSave();
        save.fileName=s;
        bf.Serialize(file, save);
        Application.LoadLevel(1);
    }
    public void createNewGame() {
        checkForGames();
        string s=t.text;
        if(files.Length<3){//checks if enough save slots
        create(s);
        }
        else {
            overRideSaveSlot.SetActive(true);
            nintendoDS.setTextAfter(s);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
