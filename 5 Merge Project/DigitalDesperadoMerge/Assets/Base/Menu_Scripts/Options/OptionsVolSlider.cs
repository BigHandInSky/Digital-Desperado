using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsVolSlider : MonoBehaviour 
{
    [SerializeField] private Text TextObject;
    [SerializeField] private Slider SlideObj;
    [SerializeField] private float Value;
    public float VolValue { get { return Value; } }

    public void Setup()
    {
        Debug.Log("setup called");
        Value = GameSettings.Instance.Volume;
        SlideObj.value = Value;
    }

    public void SetText()
    {
        TextObject.text = (SlideObj.value.ToString());
        Value = SlideObj.value;
        GameSettings.Instance.SetVolume(Value);
    }
    public void Reset(float _val)
    {
        SlideObj.value = _val;
        TextObject.text = _val.ToString();
        Value = _val;
    }
}
