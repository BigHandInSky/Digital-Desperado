using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GraphicsControl : MonoBehaviour 
{
    private int iCurrIndex = 0;
    private string[] QualityNames;

    [SerializeField] private Text NameText;
    [SerializeField] private Image NameBG;

    void Setup() 
    {
        QualityNames = QualitySettings.names;

        iCurrIndex = GameSettings.Instance.Quality;
        NameText.text = QualityNames[iCurrIndex];

        NameBG.color = OptionsSetter.Instance.NormCol;
	}

    public void ChangeName(bool _increment)
    {
        if(_increment)
        {
            if (iCurrIndex < QualityNames.Length - 1)
                iCurrIndex++;

        }
        else
        {
            if (iCurrIndex > 0)
                iCurrIndex--;
        }

        NameText.text = QualityNames[iCurrIndex];

        if (GameSettings.Instance.Quality != iCurrIndex)
            NameBG.color = OptionsSetter.Instance.TempCol;
        else
            NameBG.color = OptionsSetter.Instance.NormCol;
    }
    public void Apply()
    {
        GameSettings.Instance.Quality = iCurrIndex;
        NameBG.color = OptionsSetter.Instance.NormCol;
    }

    public void Reset()
    {
        iCurrIndex = GameSettings.Instance.Quality;
        NameText.text = QualityNames[iCurrIndex];
        NameBG.color = OptionsSetter.Instance.NormCol;
    }
}
