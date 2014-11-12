using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class settingsSaver : MonoBehaviour {
    public Slider musicVolume, masterVolume, specialEffectsVolume, antiAliasing, frameCap;
    public Toggle vSync, fullscreen, fourByThreeToggle, fiveByFourToggle, sixteenByTenToggle, sixteenByNineToggle, customWindowToggle;
  
    public void save() {
        Debug.Log(Application.persistentDataPath+"/settings.whyareyoulookingatthis");
        BinaryFormatter bf=new BinaryFormatter();
        FileStream file=File.Create(Application.persistentDataPath+"/settings.whyareyoulookingatthis");
        playerSettings data=new playerSettings();
        data.MusicVolume=musicVolume.value;
        data.MasterVolume=masterVolume.value;
        data.SoundEffectsVolume=specialEffectsVolume.value;
        data.FrameCap=(int)frameCap.value;
        data.Fullscreen=fullscreen.isOn;
        switch ((int)antiAliasing.value) {
            case 0:
                data.Aa=playerSettings.antiAliasingOptions.none;
                break;
            case 1:
                data.Aa=playerSettings.antiAliasingOptions.two;
                break;
            case 2:
                data.Aa=playerSettings.antiAliasingOptions.four;
                break;
            case 3:
                data.Aa=playerSettings.antiAliasingOptions.eight;
                break;
        }
        if(fourByThreeToggle.isOn){
            data.resolutionOption=playerSettings.resolution.fourThree;
        }
        else if(fiveByFourToggle.isOn) {
            data.resolutionOption=playerSettings.resolution.fiveFour;
        }else if(sixteenByTenToggle.isOn){
            data.resolutionOption=playerSettings.resolution.sixteenTen;
        }else if(sixteenByNineToggle.isOn){
            data.resolutionOption=playerSettings.resolution.sixteenTen;
        }
        else if(customWindowToggle.isOn){
            data.resolutionOption=playerSettings.resolution.custom;
        }
        else {
            throw new Exception("No resolution toggle selected.");
        }
        bf.Serialize(file, data);

    }
}
[Serializable]
class playerSettings {
    public enum antiAliasingOptions {
        none, two, four, eight
    };
    public enum resolution {
        fourThree, fiveFour, sixteenTen, sixteenNine, custom
    };
    private resolution res;

    internal resolution resolutionOption {
        get { return res; }
        set { res=value; }
    }
    private antiAliasingOptions aa;

    internal antiAliasingOptions Aa {
        get { return aa; }
        set { aa=value; }
    }
    private int frameCap;

    public int FrameCap {
        get { return frameCap; }
        set { frameCap=value; }
    }
    private float masterVolume;

    public float MasterVolume {
        get { return masterVolume; }
        set { masterVolume=value; }
    }
    private float musicVolume;

    public float MusicVolume {
        get { return musicVolume; }
        set { musicVolume=value; }
    }
    private float soundEffectsVolume;

    public float SoundEffectsVolume {
        get { return soundEffectsVolume; }
        set { soundEffectsVolume=value; }
    }
    private bool fullscreen;

    public bool Fullscreen {
        get { return fullscreen; }
        set { fullscreen=value; }
    }


}
