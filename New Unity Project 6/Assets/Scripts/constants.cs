using UnityEngine;
using System.Collections;

public class constants : MonoBehaviour
{
    private static float musicVolume, soundEffectVolume;
    public static void setMusicVolume(float newVol)
    {
        musicVolume = newVol;
    }
    public static void setSoundEffectsVolume(float newVol)
    {
        soundEffectVolume = newVol;
    }
    public static float getSoundEffectsVolume()
    {
        return soundEffectVolume;
    }
    public static float getMusicVolume()
    {
        return musicVolume;
    }
}
