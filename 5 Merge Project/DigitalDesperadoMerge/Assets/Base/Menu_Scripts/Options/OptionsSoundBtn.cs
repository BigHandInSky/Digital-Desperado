using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsSoundBtn : MonoBehaviour {

    [SerializeField] private bool bMusic = true;
    [SerializeField] private bool bOn = true;
    public bool OnValue { get { return bOn; } }
    [SerializeField] private Image ImgToChange;
    [SerializeField] private Sprite ImgOn;
    [SerializeField] private Sprite ImgOff;

    public void Setup()
    {
        if (bMusic)
        {
            bOn = GameSettings.Instance.Music;
        }
        else
            bOn = GameSettings.Instance.Effects;

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
        
        if(bMusic)
        {
            if (GameSettings.Instance.Music != bOn)
                ImgToChange.color = OptionsSetter.Instance.TempCol;
            else
                ImgToChange.color = OptionsSetter.Instance.NormCol;
        }
        else
        {
            if (GameSettings.Instance.Effects != bOn)
                ImgToChange.color = OptionsSetter.Instance.TempCol;
            else
                ImgToChange.color = OptionsSetter.Instance.NormCol;
        }
    }

    public void Apply()
    {
        ImgToChange.color = OptionsSetter.Instance.NormCol;

        if (bMusic)
        {
            GameSettings.Instance.Music = bOn;
        }
        else
            GameSettings.Instance.Effects = bOn;
    }

    public void Reset()
    {
        bOn = true;
        ImgToChange.sprite = ImgOn;
        ImgToChange.color = OptionsSetter.Instance.NormCol;
    }
}
