using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPreferences 
{
    private static string PLAYER_SFX_VOLUME_PREF = "sfxVolume";
    private static string PLAYER_MUSIC_VOLUME_PREF = "musicVolume";
    private static float DEFAULT_VOLUME = .5f;

    private static string PLAYER_BINDINGS = "InputBindings";


    public static void SaveSfxVolume(float volume)
    {
        PlayerPrefs.SetFloat(PLAYER_SFX_VOLUME_PREF, volume);
    }

    public static void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(PLAYER_MUSIC_VOLUME_PREF, volume);
    }

    public static float LoadSfxVolume()
    {
        return PlayerPrefs.GetFloat(PLAYER_SFX_VOLUME_PREF, DEFAULT_VOLUME);
    }
    
    public static float LoadMusicVolume()
    {
        return PlayerPrefs.GetFloat(PLAYER_MUSIC_VOLUME_PREF, DEFAULT_VOLUME);
    }

    public static void SaveInputBindings(string bindings)
    {
        PlayerPrefs.SetString(PLAYER_BINDINGS, bindings);
    }

    public static bool InputBindingsHasRecord()
    {
        return PlayerPrefs.HasKey(PLAYER_BINDINGS);
    }

    public static string LoadInputBindings()
    {
        return PlayerPrefs.GetString(PLAYER_BINDINGS);
    }
}
