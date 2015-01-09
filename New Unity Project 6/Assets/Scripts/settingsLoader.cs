using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class settingsLoader : MonoBehaviour
{


    void Start()
    {
        loadSettings();
    }
    private static void createDefaults()
    {

    }
    public static void loadSettings()
    {

        try
        {
            FileStream f = File.Open(Application.persistentDataPath + "/settings.settings", FileMode.Open);

            playerSettings settings = (playerSettings)new BinaryFormatter().Deserialize(f);
            f.Close();
            switch (settings.Aa)
            {
                case playerSettings.antiAliasingOptions.none:
                    QualitySettings.antiAliasing = 0;
                    break;
                case playerSettings.antiAliasingOptions.two:
                    QualitySettings.antiAliasing = 2;
                    break;
                case playerSettings.antiAliasingOptions.four:
                    QualitySettings.antiAliasing = 4;
                    break;
                case playerSettings.antiAliasingOptions.eight:
                    QualitySettings.antiAliasing = 8;
                    break;
            }
            Application.targetFrameRate = settings.FrameCap;
            switch (settings.FrameCap)
            {
                case 30:
                    QualitySettings.vSyncCount = 2;
                    break;
                case 60:
                    QualitySettings.vSyncCount = 1;

                    break;
                default:
                    QualitySettings.vSyncCount = 0;
                    break;

            }

            AudioListener.volume = settings.MasterVolume / 100;
            constants.setMusicVolume(settings.MusicVolume);
            constants.setSoundEffectsVolume(settings.SoundEffectsVolume);

        }
        catch (FileNotFoundException)
        {
            createDefaults();
        }
    }

}
