using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsSensSlider : MonoBehaviour
{
    [SerializeField] private Text TextObject;
    [SerializeField] private Slider SlideObj;
    [SerializeField]
    private float Value;
    public float SensValue { get { return Value; } }

    public void Setup()
    {
        Debug.Log("setup called");
        Value = GameSettings.Instance.Sens;
        SlideObj.value = Value;
        TextObject.text = (SlideObj.value.ToString());
    }

    public void SetText()
    {
        TextObject.text = (SlideObj.value.ToString());
        Value = SlideObj.value;
        GameSettings.Instance.SetSens(Value);
    }
    public void Reset(float _val)
    {
        SlideObj.value = _val;
        TextObject.text = _val.ToString();
        Value = _val;
    }
}
