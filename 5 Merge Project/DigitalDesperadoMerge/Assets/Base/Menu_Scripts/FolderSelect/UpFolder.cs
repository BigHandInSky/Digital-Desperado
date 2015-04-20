using UnityEngine;
using System.Collections;

public class UpFolder : MonoBehaviour {

    [SerializeField] private FolderListManager ListManager;

	public void Select()
    {
        ListManager.LoadParentDirectory();
    }
}
