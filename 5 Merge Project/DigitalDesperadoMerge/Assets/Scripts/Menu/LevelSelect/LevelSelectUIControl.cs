using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectUIControl : MonoBehaviour 
{

    [SerializeField] private List<GameObject> ObjsToActivate = new List<GameObject>();
    [SerializeField] private List<GameObject> ObjsToDeActivate = new List<GameObject>();

    [SerializeField] private LevelSelectUIPopulate Manager;
    [SerializeField] private UnityEngine.UI.Text CurrentLevelText;

    private static LevelSelectUIControl m_DataInstance;
    public static LevelSelectUIControl Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<LevelSelectUIControl>(); }
            return m_DataInstance;
        }
    }

    void Awake()
    {
        m_DataInstance = this;
    }

    public void OpenFolderUI()
    {
        foreach (GameObject _obj in ObjsToActivate)
            _obj.SetActive(true);

        Manager.Setup();
        CurrentLevelText.text = (LoadedLevels.Instance.iCurrentLvl + 1).ToString("00") + "-" + LoadedLevels.Instance.sGetCurrUrlName();
    }

    public void CloseFolderUI()
    {
        CurrentLevelText.text = "---";
        Manager.Clear();

        foreach (GameObject _obj in ObjsToDeActivate)
            _obj.SetActive(false);
    }

}
