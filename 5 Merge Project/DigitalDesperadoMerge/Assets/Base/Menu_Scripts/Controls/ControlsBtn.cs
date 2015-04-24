using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsBtn : MonoBehaviour {

    public enum KeyToGet
    {
        F = 0,
        B = 1,
        SL = 2,
        SR = 3,
        J = 4,
        Sh = 5,
        R = 6,
        M = 7
    }
    [SerializeField] private KeyToGet m_KeySetting;

    private KeyCode m_Key = KeyCode.None;
    public KeyCode CurrKey { get { return m_Key; } }
    [SerializeField] private ControlsInput InputObj;
    [SerializeField] private Text TextToSet;
    [SerializeField] private Color NormCol;
    [SerializeField] private Color TempCol;

    public void Setup()
    {
        //m_Key = GameSettings.Instance.
        switch (m_KeySetting)
        {
            case KeyToGet.F:
                m_Key = GameSettings.Instance.Forw;
                break;
            case KeyToGet.B:
                m_Key = GameSettings.Instance.Back;
                break;
            case KeyToGet.SL:
                m_Key = GameSettings.Instance.Left;
                break;
            case KeyToGet.SR:
                m_Key = GameSettings.Instance.Righ;
                break;
            case KeyToGet.J:
                m_Key = GameSettings.Instance.Jump;
                break;
            case KeyToGet.Sh:
                m_Key = GameSettings.Instance.Fire;
                break;
            case KeyToGet.R:
                m_Key = GameSettings.Instance.Rset;
                break;
            case KeyToGet.M:
                m_Key = GameSettings.Instance.Menu;
                break;
        }

        TextToSet.text = m_Key.ToString();
        TextToSet.color = NormCol;
    }

    public void SetKey(KeyCode _key)
    {
        if(m_Key == _key)
        {
            TextToSet.color = NormCol;
        }
        else
        {
            m_Key = _key;
            TextToSet.text = m_Key.ToString();
            TextToSet.color = TempCol;
        }

    }
    public void Apply()
    {
        switch (m_KeySetting)
        {
            case KeyToGet.F:
                GameSettings.Instance.Forw = m_Key;
                break;
            case KeyToGet.B:
                GameSettings.Instance.Back = m_Key;
                break;
            case KeyToGet.SL:
                GameSettings.Instance.Left = m_Key;
                break;
            case KeyToGet.SR:
                GameSettings.Instance.Righ = m_Key;
                break;
            case KeyToGet.J:
                GameSettings.Instance.Jump = m_Key;
                break;
            case KeyToGet.Sh:
                GameSettings.Instance.Fire = m_Key;
                break;
            case KeyToGet.R:
                GameSettings.Instance.Rset = m_Key;
                break;
            case KeyToGet.M:
                GameSettings.Instance.Menu = m_Key;
                break;
        }
        TextToSet.color = NormCol;
    }
    public void Clicked()
    {
        TextToSet.color = TempCol;
        InputObj.Activate(this);
    }
}
