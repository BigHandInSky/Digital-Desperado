using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefaultSettingsBtn : MonoBehaviour {

    public Button MusicBtn;
    public Button EfftsBtn;
    public Slider FOVSlider;
    public Slider VolSlider;

    public void SetDefaults()
    {
        GameSettings.Instance.Music = true;
        GameSettings.Instance.Effects = true;
        GameSettings.Instance.SetVolume(100f);
        GameSettings.Instance.SetFOV(90f);
        GameSettings.Instance.SetResolution(800, 600);

        MusicBtn.GetComponent<OptionsSoundBtn>().Reset();
        EfftsBtn.GetComponent<OptionsSoundBtn>().Reset();
        FOVSlider.GetComponent<OptionsFovSlider>().Reset(90f);
        VolSlider.GetComponent<OptionsVolSlider>().Reset(100f);

        GameSettings.Instance.ApplySettings();
    }
}
