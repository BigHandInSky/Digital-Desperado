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
    private const string m_StrSelected = "Enter New Key\nBackspace to cancel";
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
                SetKeyVal(m_LastInput);
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                m_LastInput = KeyCode.Mouse0;
                SetKeyVal(m_LastInput);
            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                m_LastInput = KeyCode.Mouse0;
                SetKeyVal(m_LastInput);
            }
            else if (Input.GetKey(KeyCode.Mouse2))
            {
                m_LastInput = KeyCode.Mouse0;
                SetKeyVal(m_LastInput);
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
        int _TotKeycodes = System.Enum.GetNames(typeof(KeyCode)).Length;
        for (int i = 0; i < _TotKeycodes; i++)
        {
            if (Input.GetKey((KeyCode)i) && (KeyCode)i != KeyCode.Escape)
            {
                m_LastInput = (KeyCode)i;
                return (KeyCode)i;
            }
        }

        return KeyCode.None;
    }

    void SetKeyVal(KeyCode _key)
    {
        m_ImgToSet.sprite = m_InputDetect;
        //set text
        m_TextToSet.text = _key.ToString();
        //set last button key
        m_BtnToSetPostChange.SetKey(_key);

        m_Listen = false;
        m_ImgToSet.sprite = m_Unselected;
        m_HelpText.text = m_StrInputDetect;
    }
}
