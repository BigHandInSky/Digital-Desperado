using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsSoundBtn : MonoBehaviour {

    [SerializeField]
    private bool bOn = true;
    [SerializeField]
    private Image ImgToChange;
    [SerializeField]
    private Sprite ImgOn;
    [SerializeField]
    private Sprite ImgOff;

    public void BtnClick()
    {
        bOn = !bOn;

        if(bOn)
        {
            ImgToChange.sprite = ImgOn;
        }
        else
        {
            ImgToChange.sprite = ImgOff;
        }
    }

}
