using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsFovSlider : MonoBehaviour 
{
    [SerializeField] private Text TextObject;
    [SerializeField] private Slider SlideObj;
    [SerializeField] private float Value = 90f;
    public float FOVValue { get { return Value; } }

    void Start()
    {
        SetText();
    }

    public void SetText()
    {
        TextObject.text = (SlideObj.value).ToString();
        Value = SlideObj.value;
        GameSettings.Instance.SetFOV(Value);
    }
    public void Reset(float _val)
    {
        SlideObj.value = _val;
        TextObject.text = _val.ToString();
        Value = _val;
    }
}
