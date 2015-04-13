using System.Collections.Generic;
using System.Globalization;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelsFolderBtn : MonoBehaviour 
{
    // Creates an Open File Dialog for xml files and selects that path
    public void SelectFilePath()
    {
        string filePath = "";

        filePath = EditorUtility.OpenFolderPanel("Load Level Folder", "", "");
        Debug.Log("filepath: " + filePath);

        MenuLoadLevelsFromXML.Instance.sLevelsFolderUrl = filePath;
    }
}
