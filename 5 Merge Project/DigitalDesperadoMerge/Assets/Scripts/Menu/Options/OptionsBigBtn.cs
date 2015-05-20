using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsBigBtn : MonoBehaviour {

    private enum BtnType
    {
        Unset,
        Music,
        Effects,
        FullScreen
    }
    [SerializeField] private BtnType ButtonEnum = BtnType.Unset;
    [SerializeField] private bool bOn = true;
    public bool OnValue { get { return bOn; } }
    [SerializeField] private Image ImgToChange;
    [SerializeField] private Sprite ImgOn;
    [SerializeField] private Sprite ImgOff;

    public void Setup()
    {
        switch(ButtonEnum)
        {
            case BtnType.Unset:
                Debug.LogWarning("Unset BigBtn : " + gameObject);
                return;
            case BtnType.Music:
                bOn = GameSettings.Instance.Music;
                break;
            case BtnType.Effects:
                bOn = GameSettings.Instance.Effects;
                break;
            case BtnType.FullScreen:
                bOn = GameSettings.Instance.Fullscreen;
                break;
        }

        if (bOn)
            ImgToChange.sprite = ImgOn;
        else
            ImgToChange.sprite = ImgOff;

        ImgToChange.color = OptionsSetter.Instance.NormCol;
    }

    public void BtnClick()
    {
        bOn = !bOn;

        if (bOn)
            ImgToChange.sprite = ImgOn;
        else
            ImgToChange.sprite = ImgOff;

        switch (ButtonEnum)
        {
            case BtnType.Unset:
                Debug.LogWarning("Unset BigBtn : " + gameObject);
                return;

            case BtnType.Music:
                if (GameSettings.Instance.Music != bOn)
                    ImgToChange.color = OptionsSetter.Instance.TempCol;
                else
                    ImgToChange.color = OptionsSetter.Instance.NormCol;
                break;

            case BtnType.Effects:
                if (GameSettings.Instance.Effects != bOn)
                    ImgToChange.color = OptionsSetter.Instance.TempCol;
                else
                    ImgToChange.color = OptionsSetter.Instance.NormCol;
                break;

            case BtnType.FullScreen:
                if (GameSettings.Instance.Fullscreen != bOn)
                    ImgToChange.color = OptionsSetter.Instance.TempCol;
                else
                    ImgToChange.color = OptionsSetter.Instance.NormCol;
                break;
        }
    }

    public void Apply()
    {
        ImgToChange.color = OptionsSetter.Instance.NormCol;

        switch (ButtonEnum)
        {
            case BtnType.Unset:
                Debug.LogWarning("Unset BigBtn : " + gameObject);
                return;

            case BtnType.Music:
                GameSettings.Instance.Music = bOn;
                break;

            case BtnType.Effects:
                GameSettings.Instance.Effects = bOn;
                break;

            case BtnType.FullScreen:
                GameSettings.Instance.Fullscreen = bOn;
                break;
        }
    }

    public void Reset()
    {
        switch (ButtonEnum)
        {
            case BtnType.Unset:
                Debug.LogWarning("Unset BigBtn : " + gameObject);
                return;
            case BtnType.Music:
                bOn = GameSettings.Instance.Music;
                break;
            case BtnType.Effects:
                bOn = GameSettings.Instance.Effects;
                break;
            case BtnType.FullScreen:
                bOn = GameSettings.Instance.Fullscreen;
                break;
        }

        if (bOn)
            ImgToChange.sprite = ImgOn;
        else
            ImgToChange.sprite = ImgOff;
        ImgToChange.color = OptionsSetter.Instance.NormCol;
    }
}
