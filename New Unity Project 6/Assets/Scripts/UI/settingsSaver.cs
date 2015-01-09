using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class settingsSaver : MonoBehaviour
{
    public Slider musicVolume, masterVolume, specialEffectsVolume, antiAliasing;
    public Toggle thirtyFPS, sixtyFPS, noFrameCap;

    public void Start()
    {
        setUp();
    }
    private void setUp()
    {

        FileStream f = File.Open(Application.persistentDataPath + "/settings.settings", FileMode.Open);

        playerSettings settings = (playerSettings)new BinaryFormatter().Deserialize(f);
        f.Close();
        Debug.Log(settings.FrameCap);
        switch (settings.FrameCap)
        {
            case 30:
                thirtyFPS.isOn = true;
                break;
            case 60:
                sixtyFPS.isOn = true;
                break;
            default:
                noFrameCap.isOn = true;
                break;
        }
        musicVolume.value = settings.MusicVolume;
        specialEffectsVolume.value = settings.SoundEffectsVolume;
        antiAliasing.value = (int)settings.Aa;
        masterVolume.value = settings.MasterVolume;



    }

    public void save()
    {
        Debug.Log(Application.persistentDataPath + "/settings.txt");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/settings.settings");
        playerSettings data = new playerSettings();
        data.MusicVolume = musicVolume.value;
        data.MasterVolume = masterVolume.value;
        data.SoundEffectsVolume = specialEffectsVolume.value;
        if (thirtyFPS.isOn)
        {
            data.FrameCap = 30;
        }
        else if (sixtyFPS.isOn)
        {
            data.FrameCap = 60;
        }
        else
        {
            data.FrameCap = -1;
        }
        Application.targetFrameRate = data.FrameCap;
        switch ((int)antiAliasing.value)
        {
            case 0:
                data.Aa = playerSettings.antiAliasingOptions.none;
                break;
            case 1:
                data.Aa = playerSettings.antiAliasingOptions.two;
                break;
            case 2:
                data.Aa = playerSettings.antiAliasingOptions.four;
                break;
            case 3:
                data.Aa = playerSettings.antiAliasingOptions.eight;
                break;
        }
        bf.Serialize(file, data);
        file.Close();
        settingsLoader.loadSettings();
    }

}
[Serializable]
class playerSettings
{
    public enum antiAliasingOptions
    {
        none, two, four, eight
    };
    private antiAliasingOptions aa;

    internal antiAliasingOptions Aa
    {
        get { return aa; }
        set { aa = value; }
    }
    private int frameCap;//-1 for none

    public int FrameCap
    {
        get { return frameCap; }
        set { frameCap = value; }
    }
    private float masterVolume;

    public float MasterVolume
    {
        get { return masterVolume; }
        set { masterVolume = value; }
    }
    private float musicVolume;

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }
    private float soundEffectsVolume;

    public float SoundEffectsVolume
    {
        get { return soundEffectsVolume; }
        set { soundEffectsVolume = value; }
    }
}
