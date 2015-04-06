using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsVolSlider : MonoBehaviour 
{
    [SerializeField] private Text TextObject;
    [SerializeField] private Scrollbar ScrollBar;
    [SerializeField] private float Value;

    void Start()
    {
        SetText();
    }

    public void SetText()
    {
        TextObject.text = (ScrollBar.value * 10f).ToString("00");
        Value = ScrollBar.value;
    }
}
