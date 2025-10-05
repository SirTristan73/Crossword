
using EventBus;
using UnityEngine;


[System.Serializable]
public class SaveFile
{

    public GameSettings GameSettings = new GameSettings();

    public SaveFile()
    {


    }
}



[System.Serializable]
public class GameSettings
{
    public LanguageState _savedLanguage;
    public float _masterVolume;
    public float _musicVolume;
    public float _sfxVolume;


    public GameSettings()
    {
        _masterVolume = 1f;
        _musicVolume = 1f;
        _sfxVolume = 1f;
    }

}