using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ErrorScript : MonoBehaviour {

    private static ErrorScript m_DataInstance;
    public static ErrorScript Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<ErrorScript>(); }
            return m_DataInstance;
        }
    }

    [SerializeField] private List<GameObject> ObjsToOpenAndClose;
    [SerializeField] private Text ErrorText;
    private const string sErrorDefaultText = "[Error Text]";

    void Awake()
    {
        m_DataInstance = this;
    }

    public enum Errors
    {
        FolderNoLevels,
        InvalidLevel,
        ErrorSaving
    }

    public void OpenError(Errors _Type, string _AdditionalText)
    {
        SetObjActives(true);
        switch (_Type)
        {
            case Errors.FolderNoLevels:
                ErrorText.text = "No Levels found in folder\n" + _AdditionalText;
                break;
            case Errors.InvalidLevel:
                ErrorText.text = "Invalid Level File\n" + _AdditionalText;
                break;
            case Errors.ErrorSaving:
                ErrorText.text = "Error during Saving\n" + _AdditionalText;
                break;
        }
    }
    public void CloseError()
    {
        SetObjActives(false);
        ErrorText.text = sErrorDefaultText;
    }

    void SetObjActives(bool _value)
    {
        foreach (GameObject obj in ObjsToOpenAndClose)
            obj.SetActive(_value);
    }
}
