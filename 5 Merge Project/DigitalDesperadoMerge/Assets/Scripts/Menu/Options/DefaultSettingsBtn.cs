using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefaultSettingsBtn : MonoBehaviour {

    public Button MusicBtn;
    public Button EfftsBtn;
    public Button ScreenBtn;
    public GraphicsControl GraphicsScript;
    public Slider FOVSlider;
    public Slider MusSlider;
    public Slider EffSlider;
    public Slider SenSlider;

    public void SetDefaults()
    {
        GameSettings.Instance.Music = true;
        GameSettings.Instance.Effects = true;
        GameSettings.Instance.Fullscreen = false;
        GameSettings.Instance.Quality = 2;
        GameSettings.Instance.SetVolume(75f, true);
        GameSettings.Instance.SetVolume(75f, false);
        GameSettings.Instance.SetFOV(90f);
        GameSettings.Instance.SetResolution(800, 600);
        GameSettings.Instance.SetSens(5f);

        MusicBtn.GetComponent<OptionsBigBtn>().Reset();
        EfftsBtn.GetComponent<OptionsBigBtn>().Reset();
        ScreenBtn.GetComponent<OptionsBigBtn>().Reset();
        GraphicsScript.Reset();
        FOVSlider.GetComponent<OptionsSlider>().Reset(90f);
        MusSlider.GetComponent<OptionsSlider>().Reset(75f);
        EffSlider.GetComponent<OptionsSlider>().Reset(75f);
        SenSlider.GetComponent<OptionsSlider>().Reset(5f);

        OptionsSetter.Instance.ResBtnUpdate(800, 600);
        GameSettings.Instance.ApplySettings();
    }
}
