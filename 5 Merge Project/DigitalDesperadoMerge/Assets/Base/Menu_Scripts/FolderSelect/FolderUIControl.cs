using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FolderUIControl : MonoBehaviour 
{
    [SerializeField] private List<GameObject> ObjsToActivate = new List<GameObject>();
    [SerializeField] private List<GameObject> ObjsToDeActivate = new List<GameObject>();

    [SerializeField] private FolderListManager Manager;
    [SerializeField] private SelectFolderBtn Select;
    [SerializeField] private CancelFolderSelectBtn Cancel;

    private static FolderUIControl m_DataInstance;
    public static FolderUIControl Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<FolderUIControl>(); }
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

        Manager.RootLoad();
        Select.Setup();
        Cancel.Setup();
    }

    public void CloseFolderUI()
    {
        foreach (GameObject _obj in ObjsToDeActivate)
            _obj.SetActive(false);
    }
}
