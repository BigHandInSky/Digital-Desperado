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

    public Color NormCol;
    public Color TempCol;

    void Awake()
    {
        m_DataInstance = this;
    }

    public void SetOptions()
    {
        foreach (GameObject _obj in OptionObjects)
            _obj.SendMessage("Setup");
    }
    public void Apply()
    {
        foreach (GameObject _obj in OptionObjects)
            _obj.SendMessage("Apply");
        GameSettings.Instance.ApplySettings();
    }
}
