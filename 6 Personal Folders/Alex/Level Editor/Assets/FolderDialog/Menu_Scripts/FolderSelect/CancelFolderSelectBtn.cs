using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CancelFolderSelectBtn : MonoBehaviour {

    [SerializeField] private FolderUIControl FolderParent;

    public void Setup()
    {
        //if (MenuLoadLevelsFromXML.Instance.Urls.Count < 1)
           //gameObject.GetComponent<Button>().interactable = false;
    }

    public void Select()
    {
        //if(MenuLoadLevelsFromXML.Instance.Urls.Count > 0)
            FolderParent.CloseFolderUI();
    }
}
