using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private int firstPlayInt;

    private float backgroundfloatMusic, backgroundfloatSFX;

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private Slider backgroundSliderMusic, backgroundSliderSFX;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(UtilityClass.FirstPlay);

        if (firstPlayInt == 0)
        {
            backgroundfloatMusic = .25f;
            backgroundfloatSFX = .25f;
            backgroundSliderMusic.value = backgroundfloatMusic;
            backgroundSliderSFX.value = backgroundfloatSFX;

            PlayerPrefs.SetFloat(UtilityClass.MusicPrefs, backgroundfloatMusic);
            PlayerPrefs.SetFloat(UtilityClass.SFXPrefs, backgroundfloatSFX);
            PlayerPrefs.SetInt(UtilityClass.FirstPlay, -1);
        }
        else
        {
            backgroundfloatMusic = PlayerPrefs.GetFloat(UtilityClass.MusicPrefs);
            backgroundSliderMusic.value = backgroundfloatMusic;

            backgroundfloatSFX = PlayerPrefs.GetFloat(UtilityClass.SFXPrefs);
            backgroundSliderSFX.value = backgroundfloatSFX;
        }
    }


    public void saveSoundSettings()
    {
        PlayerPrefs.SetFloat(UtilityClass.MusicPrefs, backgroundSliderMusic.value);
        PlayerPrefs.SetFloat(UtilityClass.SFXPrefs, backgroundSliderSFX.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            saveSoundSettings();
        }
    }

    public void setVolumeLevelMusic()
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(backgroundSliderMusic.value) * 20);
        PlayerPrefs.SetFloat(UtilityClass.MusicPrefs, backgroundSliderMusic.value);
    }

    public void setVolumeLevelSFX()
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(backgroundSliderSFX.value) * 20);
        PlayerPrefs.SetFloat(UtilityClass.SFXPrefs, backgroundSliderSFX.value);
    }
}
