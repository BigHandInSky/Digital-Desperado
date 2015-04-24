using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControlsInput : MonoBehaviour {

    [SerializeField] private Image m_ImgToSet;
    [SerializeField] private Text m_TextToSet;
    [SerializeField] private Text m_HelpText;

    [SerializeField] private Sprite m_Unselected;
    [SerializeField] private Sprite m_Selected;
    [SerializeField] private Sprite m_InputDetect;

    private bool m_Listen = false;
    private KeyCode m_LastInput;
    private ControlsBtn m_BtnToSetPostChange;

    private const string m_StrUnselected = "Select Key to\nchange below";
    private const string m_StrSelected = "Enter New Key";
    private const string m_StrInputDetect = "Key Entered\nApply to save entered Keys";
    private const string m_StrCancel = "Cancelled";

    void Update()
    {
        if(m_Listen)
        {
            m_HelpText.text = m_StrSelected;

            if(Input.GetKey(KeyCode.Backspace))
            {
                m_ImgToSet.sprite = m_Unselected;
                m_BtnToSetPostChange.SetKey(m_BtnToSetPostChange.CurrKey);
                m_HelpText.text = m_StrCancel;
                m_Listen = false;
            }
            else if (FetchKey() != KeyCode.None)
            {
                m_ImgToSet.sprite = m_InputDetect;
                //set text
                m_TextToSet.text = m_LastInput.ToString();
                //set last button key
                m_BtnToSetPostChange.SetKey(m_LastInput);

                m_Listen = false;
                m_ImgToSet.sprite = m_Unselected;
                m_HelpText.text = m_StrInputDetect;
            }
        }
    }

    public void Apply()
    {
        m_HelpText.text = m_StrUnselected;
    }
    public void Activate(ControlsBtn _btnCalled)
    {
        m_HelpText.text = m_StrSelected;
        m_BtnToSetPostChange = _btnCalled;
        m_Listen = true;
        m_ImgToSet.sprite = m_Selected;
    }
    KeyCode FetchKey()
    {
        Debug.Log("FetchKey");

        int _TotKeycodes = System.Enum.GetNames(typeof(KeyCode)).Length;
        for (int i = 0; i < _TotKeycodes; i++)
        {
            if (Input.GetKey((KeyCode)i))
            {
                m_LastInput = (KeyCode)i;
                return (KeyCode)i;
            }
        }

        return KeyCode.None;
    }
}
