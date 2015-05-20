using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsResBtn : MonoBehaviour {

    [SerializeField] private int Width = 800;
    [SerializeField] private int Height = 600;
    private bool bSelected = false;

    void Setup()
    {
        if (Width == GameSettings.Instance.RWidth
            && Height == GameSettings.Instance.RHeight)
        {
            GetComponent<Image>().color = GetComponent<Button>().colors.disabledColor;
            bSelected = true;
        }
    }

    public void SetResolution()
    {
        if (!bSelected)
        {
            bSelected = true;
            GetComponent<Image>().color = OptionsSetter.Instance.TempCol;
            OptionsSetter.Instance.ResBtnUpdate(Width, Height);
        }
    }

    public void Apply()
    {
        if (bSelected
            && Width != GameSettings.Instance.RWidth
            && Height != GameSettings.Instance.RHeight)
        {
            GameSettings.Instance.SetResolution(Width, Height);
            GetComponent<Image>().color = GetComponent<Button>().colors.disabledColor;
            OptionsSetter.Instance.ResBtnUpdate(Width, Height);
        }
    }

    public void UpdateRes(int _width, int _height)
    {
        if (Width == GameSettings.Instance.RWidth
            && Height == GameSettings.Instance.RHeight)
        {
            GetComponent<Image>().color = GetComponent<Button>().colors.disabledColor;
            bSelected = true;
        }
        else if (Width != _width
            || Height != _height)
        {
            GetComponent<Image>().color = OptionsSetter.Instance.ResBtnNormCol;
            bSelected = false;
        }
    }
}
