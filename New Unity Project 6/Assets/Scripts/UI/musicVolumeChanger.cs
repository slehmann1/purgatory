﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class musicVolumeChanger : MonoBehaviour {

    public void change(float f)
    {
        

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/settings.settings", FileMode.Open);
            playerSettings data = (playerSettings)bf.Deserialize(file);
            file.Close();
            data.MusicVolume = f;
            file = File.Create(Application.persistentDataPath + "/settings.settings");
            bf.Serialize(file, data);
            file.Close();

     
    }
}
