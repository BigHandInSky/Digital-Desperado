using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlsSetter : MonoBehaviour {

    private static ControlsSetter m_DataInstance;
    public static ControlsSetter Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<ControlsSetter>(); }
            return m_DataInstance;
        }
    }

    [SerializeField] private ControlsInput InputGetter;
    [SerializeField] private List<ControlsBtn> m_Btns = new List<ControlsBtn>();

    void Awake()
    {
        m_DataInstance = this;
    }

    public void SetControls()
    {
        foreach (ControlsBtn _obj in m_Btns)
            _obj.Setup();
    }

    public void Default()
    {
        foreach (ControlsBtn _obj in m_Btns)
            _obj.DefaultBtn();

        Apply();
    }

    public void Apply()
    {
        InputGetter.Apply();
        foreach (ControlsBtn _obj in m_Btns)
            _obj.Apply();
        GameSettings.Instance.ApplyControls();
    }
}
