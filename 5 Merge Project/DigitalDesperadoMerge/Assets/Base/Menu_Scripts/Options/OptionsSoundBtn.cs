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

    public void BtnClick()
    {
        bOn = !bOn;

        if (bOn)
            ImgToChange.sprite = ImgOn;
        else
            ImgToChange.sprite = ImgOff;

        if (bMusic)
        {
            GameSettings.Instance.Music = bOn;
            AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.Menus);
        }
        else
            GameSettings.Instance.Effects = bOn;
    }

    public void Reset()
    {
        bOn = true;
        ImgToChange.sprite = ImgOn;
    }
}
