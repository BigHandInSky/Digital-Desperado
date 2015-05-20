using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionsSetter : MonoBehaviour {

    private static OptionsSetter m_DataInstance;
    public static OptionsSetter Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<OptionsSetter>(); }
            return m_DataInstance;
        }
    }

    [SerializeField]
    private List<GameObject> OptionObjects = new List<GameObject>();
    [SerializeField]
    private List<OptionsResBtn> ResButtons = new List<OptionsResBtn>();

    public Color NormCol;
    public Color ResBtnNormCol;
    public Color TempCol;

    void Awake()
    {
        m_DataInstance = this;
    }

    public void SetOptions()
    {
        foreach (GameObject _obj in OptionObjects)
            _obj.SendMessage("Setup");

        foreach (OptionsResBtn _obj in ResButtons)
            _obj.SendMessage("Setup");

        ResBtnUpdate(GameSettings.Instance.RWidth, GameSettings.Instance.RHeight);
    }
    public void Apply()
    {
        foreach (GameObject _obj in OptionObjects)
            _obj.SendMessage("Apply");

        foreach (OptionsResBtn _obj in ResButtons)
            _obj.SendMessage("Apply");

        GameSettings.Instance.ApplySettings();
    }
    public void ResBtnUpdate(int _newWidth, int _newHeight)
    {
        foreach (OptionsResBtn _obj in ResButtons)
            _obj.UpdateRes(_newWidth, _newHeight);
    }
}
