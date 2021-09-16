using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsInDiffScenes : MonoBehaviour
{

    private float backgroundfloatMusic, backgroundfloatSFX;

    [SerializeField]
    private AudioSource audioSourceMusic, audioSourceSFX;

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private Slider backgroundSliderMusic, backgroundSliderSFX;


    private void Awake()
    {
        continueSoundSettings();
    }

    private void continueSoundSettings()
    {
        backgroundfloatSFX = PlayerPrefs.GetFloat(UtilityClass.SFXPrefs);
        audioSourceSFX.volume = backgroundfloatSFX;


        backgroundfloatMusic = PlayerPrefs.GetFloat(UtilityClass.MusicPrefs);
        audioSourceMusic.volume = backgroundfloatMusic;
    }
    private void Start()
    {

        backgroundSliderMusic.value = backgroundfloatMusic;
    }
    public void setVolumeLevelMusicInGame()
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(backgroundSliderMusic.value) * 20);
        PlayerPrefs.SetFloat(UtilityClass.MusicPrefs, backgroundSliderMusic.value);
    }

    public void setVolumeLevelSFXInGame()
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(backgroundSliderSFX.value) * 20);
        PlayerPrefs.SetFloat(UtilityClass.SFXPrefs, backgroundSliderSFX.value);
    }
    private void saveSoundSettingsOutsideMainMenu()
    {
        PlayerPrefs.SetFloat(UtilityClass.MusicPrefs, backgroundSliderMusic.value);
        PlayerPrefs.SetFloat(UtilityClass.SFXPrefs, backgroundSliderSFX.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            saveSoundSettingsOutsideMainMenu();
        }
    }

}
