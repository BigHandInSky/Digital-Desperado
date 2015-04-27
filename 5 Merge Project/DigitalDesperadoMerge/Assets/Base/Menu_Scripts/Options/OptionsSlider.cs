using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsSlider : MonoBehaviour
{
    [SerializeField] private Text TextObject;
    [SerializeField] private Slider SlideObj;
    [SerializeField] private float Value;

    public enum SliderType
    {
        Volume,
        FOV,
        Sensitivity
    }
    [SerializeField]
    private SliderType m_Type;

    public void Setup()
    {
        Debug.Log("setup called");

        if (m_Type == SliderType.Volume)
            Value = GameSettings.Instance.Volume;
        else if (m_Type == SliderType.FOV)
            Value = GameSettings.Instance.FOV;
        else if (m_Type == SliderType.Sensitivity)
            Value = GameSettings.Instance.Sens;

        SlideObj.value = Value;
        TextObject.text = (SlideObj.value.ToString());
        TextObject.color = OptionsSetter.Instance.NormCol;
    }

    public void SetText()
    {
        TextObject.text = (SlideObj.value).ToString();
        Value = SlideObj.value;

        if (m_Type == SliderType.Volume && Value != GameSettings.Instance.Volume)
            TextObject.color = OptionsSetter.Instance.TempCol;
        else if (m_Type == SliderType.FOV && Value != GameSettings.Instance.FOV)
            TextObject.color = OptionsSetter.Instance.TempCol;
        else if (m_Type == SliderType.Sensitivity && Value != GameSettings.Instance.Sens)
            TextObject.color = OptionsSetter.Instance.TempCol;
        else
            TextObject.color = OptionsSetter.Instance.NormCol;

    }

    public void Apply()
    {
        GameSettings.Instance.SetSens(Value);

        if (m_Type == SliderType.Volume)
            GameSettings.Instance.SetVolume(Value);
        else if (m_Type == SliderType.FOV)
            GameSettings.Instance.SetFOV(Value);
        else if (m_Type == SliderType.Sensitivity)
            GameSettings.Instance.SetSens(Value);

        TextObject.color = OptionsSetter.Instance.NormCol;
    }
    public void Reset(float _val)
    {
        SlideObj.value = _val;
        TextObject.text = _val.ToString();
        Value = _val;
        TextObject.color = OptionsSetter.Instance.NormCol;
    }
}
