using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class FolderEntry : MonoBehaviour {

    private DirectoryInfo FolderInfo;
    private GameObject ListManager;

    [SerializeField] private Text TextToSet;

	public void SetupEntry(GameObject _manager, DirectoryInfo _folder)
    {
        FolderInfo = _folder;
        ListManager = _manager;
        TextToSet.text = FolderInfo.FullName;
    }

    public void Select()
    {
        ListManager.GetComponent<FolderListManager>().LoadNewDirectory(FolderInfo);
    }

    public void Delete()
    {
        DestroyObject(this.gameObject);
    }
}
