using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectFolderBtn : MonoBehaviour {

    [SerializeField] private FolderUIControl FolderParent;
    [SerializeField] private FolderListManager Manager;

    public void Setup()
    {
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        while (true)
        {
            gameObject.GetComponent<Button>().interactable = Manager.SuitableDirectory;

            yield return new WaitForEndOfFrame();
        }
    }

    public void Select()
    {
        MenuLoadLevelsFromXML.Instance.GetFolder(Manager.DirectoryUrl);
        FolderParent.CloseFolderUI();
    }
}
