using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FolderListManager : MonoBehaviour {

    private DirectoryInfo CurrentDirectory;
    public string DirectoryUrl { get { return CurrentDirectory.FullName; } }
    public bool SuitableDirectory = false;

    private DirectoryInfo ParentDirectory;
    private DirectoryInfo RootDirectory;

    private float Spacing = 7f;
    private float EntryHeight = 30f;

    [SerializeField] private GameObject ObjToSpawn;
    [SerializeField] private RectTransform ScrollContent;
    [SerializeField] private List<GameObject> Entries;
    [SerializeField] private Text TextToSet;
    [SerializeField] private Button UpFolderBtn;
    
    public void RootLoad()
    {
        if (Directory.Exists("C:"))
        {
            RootDirectory = new DirectoryInfo("C:");
            LoadNewDirectory(RootDirectory);
        }
    }

    public void LoadNewDirectory(DirectoryInfo _newDirInfo)
    {
        Clear();

        if (_newDirInfo.Parent != null)
        {
            UpFolderBtn.enabled = true;
            ParentDirectory = _newDirInfo.Parent;
        }
        else
        {
            UpFolderBtn.enabled = false;
            ParentDirectory = null;
        }

        CurrentDirectory = _newDirInfo;
        TextToSet.text = CurrentDirectory.FullName;

        IsDirectorySuitable(CurrentDirectory);

        ReSizeContentsObj(CurrentDirectory.GetDirectories().Length);

        foreach(DirectoryInfo _entry in CurrentDirectory.GetDirectories())
        {
            FillList(_entry);
        }
    }

    private void FillList(DirectoryInfo _Directory)
    {
        GameObject _clone = Instantiate(ObjToSpawn);
        _clone.GetComponent<RectTransform>().SetParent(ScrollContent);
        _clone.GetComponent<RectTransform>().localScale = Vector2.one;
        _clone.GetComponent<FolderEntry>().SetupEntry(this.gameObject, _Directory);

        Entries.Add(_clone);
    }

    private void ReSizeContentsObj(int _size)
    {
        Debug.Log(_size);

        float _x = ScrollContent.sizeDelta.x;
        float _y = (EntryHeight * _size) + (Spacing * _size);

        ScrollContent.sizeDelta = new Vector2(_x, _y); 

        Vector2 _NewPos = new Vector2(0f, -((ObjToSpawn.GetComponent<RectTransform>().sizeDelta.y * _size) + (Spacing * _size)));
        ScrollContent.localPosition = _NewPos;
    }

    private void Clear()
    {
        foreach(GameObject _obj in Entries)
        {
            _obj.GetComponent<FolderEntry>().Delete();
        }
        Entries.Clear();
    }

    public void LoadParentDirectory()
    {
        if (ParentDirectory != null)
            LoadNewDirectory(ParentDirectory);
    }

    private void IsDirectorySuitable(DirectoryInfo _newDirInfo)
    {
        if (_newDirInfo.GetFiles("*.xml").Length > 0)
            SuitableDirectory = true;
        else
            SuitableDirectory = false;
    }
}
