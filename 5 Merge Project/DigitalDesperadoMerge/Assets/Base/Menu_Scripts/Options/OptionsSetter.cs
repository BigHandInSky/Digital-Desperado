using UnityEngine;
using System.Collections;

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

    [SerializeField] private OptionsFovSlider ObjFOV;
    [SerializeField] private OptionsVolSlider ObjVol;
    [SerializeField] private OptionsSoundBtn ObjMus;
    [SerializeField] private OptionsSoundBtn ObjEff;

    void Awake()
    {
        m_DataInstance = this;
    }

    public void SetOptions()
    {
        Debug.Log("SetOptions called");
        ObjFOV.Setup();
        ObjVol.Setup();
        ObjMus.Setup();
        ObjEff.Setup();
    }

}
