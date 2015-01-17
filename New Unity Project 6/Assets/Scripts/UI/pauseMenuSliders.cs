using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class pauseMenuSliders : MonoBehaviour {
    public Slider master, music;
    public textAppender masterTA, musicTA;
	// Use this for initialization
	void Awake () {
        FileStream f = File.Open(Application.persistentDataPath + "/settings.settings", FileMode.Open);
        playerSettings settings = (playerSettings)new BinaryFormatter().Deserialize(f);
        f.Close();
        master.value = settings.MasterVolume;
        music.value=settings.MusicVolume;
        masterTA.setValue(settings.MasterVolume);
        musicTA.setValue(settings.MusicVolume);
    }
	
	
}
