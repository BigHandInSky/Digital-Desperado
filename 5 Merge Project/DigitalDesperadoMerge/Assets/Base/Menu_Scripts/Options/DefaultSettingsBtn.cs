﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefaultSettingsBtn : MonoBehaviour {

    public Button MusicBtn;
    public Button EfftsBtn;
    public Slider FOVSlider;
    public Slider VolSlider;
    public Slider SenSlider;

    public void SetDefaults()
    {
        GameSettings.Instance.Music = true;
        GameSettings.Instance.Effects = true;
        GameSettings.Instance.SetVolume(0.75f, true);
        GameSettings.Instance.SetVolume(0.75f, false);
        GameSettings.Instance.SetFOV(90f);
        GameSettings.Instance.SetResolution(800, 600);
        GameSettings.Instance.SetSens(5f);

        MusicBtn.GetComponent<OptionsSoundBtn>().Reset();
        EfftsBtn.GetComponent<OptionsSoundBtn>().Reset();
        FOVSlider.GetComponent<OptionsSlider>().Reset(90f);
        VolSlider.GetComponent<OptionsSlider>().Reset(100f);
        SenSlider.GetComponent<OptionsSlider>().Reset(5f);

        GameSettings.Instance.ApplySettings();
    }
}
