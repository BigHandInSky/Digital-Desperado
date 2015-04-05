using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.IO;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//holds functions to get various data from an XML, from which the other Level UI objects use to get their respective data
public class MenuLoadLevelsFromXML : MonoBehaviour
{
    private static MenuLoadLevelsFromXML m_DataInstance;
    public static MenuLoadLevelsFromXML Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<MenuLoadLevelsFromXML>(); }
            return m_DataInstance;
        }
    }

    private const string sDefaultFolderPath = "C:/Users/Peter/Documents/GitHub/Digital-Desperado/6 Personal Folders/Peter/LevelTests";
    string sFileType = ".xml";

    public struct MenuLoadXMLMapData
    {
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
    }

    [SerializeField] private DirectoryInfo FolderInfo;
    public string sLevelsFolderUrl = "";
    [SerializeField] private List<string> FileUrls;
    [SerializeField] private List<string> FileNames;
    public List<string> Names { get { return FileNames; } }
    
    MenuLoadXMLMapData stMapUIPlayer;
    MenuLoadXMLMapData stMapUIEnd;
    List<MenuLoadXMLMapData> stMapUILevels;
    List<MenuLoadXMLMapData> stMapUITowers;
    List<MenuLoadXMLMapData> stMapUITargets;

    void Start ()
    {
        m_DataInstance = this;

        if (CheckUrl(sLevelsFolderUrl))
        {
            FolderInfo = new DirectoryInfo(sLevelsFolderUrl);
        }
        else
        {
            FolderInfo = new DirectoryInfo(sDefaultFolderPath);
            sLevelsFolderUrl = sDefaultFolderPath;
            Debug.LogError("FolderPath not assigned/is null, using default, assigning default as url");
        }

        if (FolderInfo != null)
        {
            FileInfo[] infos = FolderInfo.GetFiles("*.xml");

            foreach (FileInfo obj in infos)
            {
                //Debug.Log("FileInfo name : " + obj.Name);
                //Debug.Log("FileInfo name through path : " + Path.GetFileNameWithoutExtension(obj.Name));
                //Debug.Log("FileInfo full name : " + obj.FullName);

                FileUrls.Add(obj.FullName);
                FileNames.Add(Path.GetFileNameWithoutExtension(obj.Name));
            }	
        }
        else
        {
            Debug.LogError("FolderInfo checked to be null");
        }

        gameObject.GetComponent<LoadedLevels>().vUpdateData();
    }
    
	private bool CheckUrl(string _url)
	{
		if(
            _url == ""
		    || !_url.Contains(".xml")
            || Path.GetFullPath(_url) == null
            )
			return false;
		else 
			return true;
	}

    // Creates an Open File Dialog for xml files and selects that path
    public void SelectFilePath()
    {
        // File Path
        string filePath = "";
        // File Name
        string fileName = "";

        // Create File Dialog and retrieve path
        filePath = EditorUtility.OpenFilePanel("Load Level", "", "xml");

        // Split up file path by folder
        string[] folderNames = filePath.Split('/');
        // Find the file name
        fileName = folderNames[folderNames.Length - 1];
        // Remove the file name from the path
        filePath = filePath.Remove(filePath.Length - fileName.Length);

        // Set the Browse Level Text
        //browseLevelText.text = fileName;

        // Set the file path and name
        //sFilePath = filePath;
        //sFileName = fileName;
    }

    // Loads a level from an xml file
    public void LoadLevel()
    {
        // If the file path or name is empty, then return
        if (!CheckUrl(sLevelsFolderUrl))
        {
            Debug.Log("No path data");
            return;
        }

        // Xml reader for the file
        XmlReader reader = XmlReader.Create(sLevelsFolderUrl);

        // Read the first node
        reader.Read();

        // If the first node is not LevelData then return
        if (reader.Name != "LevelData")
        {
            Debug.Log("File not a supported level");

            return;
        }

        // While there are nodes to read
        while (reader.Read())
        {
            // If the node is an open node
            if (reader.IsStartElement())
            {
                // Check node name, instantiate the prefab for each node and assign its transform
                switch (reader.Name)
                {
                    case "Stats":
                        break;

                    case "PlayerStart":
                        break;

                    case "Goal":
                        break;

                    case "Platform":
                        break;

                    case "Tower":
                        break;

                    case "Target":
                        break;
                        /*
                    case "Target":
                        int targetType = Mathf.Clamp(int.Parse(reader.GetAttribute("type")) - 1, 0, agoPlatformPrefabs.Length);
                        GameObject target = Instantiate(agoTargetPrefabs[targetType]);
                        AssignTransform(target, reader.ReadSubtree());
                        break;*/
                }
            }
        }

        // Close the reader
        reader.Close();

        Debug.Log("Level Load Complete - " + sLevelsFolderUrl);
    }

    public enum StatsType
    {
        Tags,
        Secs,
        Fras,
        Shts
    }
    public List<string> GetLevelStats(int _levelUrlNum, StatsType _statName)
    {
        List<string> _tempStrings = new List<string>();

        // Xml reader for the file
        XmlReader reader = XmlReader.Create(FileUrls[_levelUrlNum]);

        // Read the first node
        reader.Read();

        // If the first node is not LevelData then return
        if (reader.Name != "LevelData")
        {
            Debug.LogError("File not a supported level");

            return _tempStrings;
        }
        
        while (reader.Read())
        {
            if (reader.Name == _statName.ToString())                
            {
                if(reader.GetAttribute("A") == null)
                {
                    Debug.LogError("MenuXML found no A Attribute on given stat in lsGetLevelStats");
                    break;
                }

                _tempStrings.Add(reader.GetAttribute("A"));
                _tempStrings.Add(reader.GetAttribute("B"));
                _tempStrings.Add(reader.GetAttribute("C"));
                _tempStrings.Add(reader.GetAttribute("D"));
                _tempStrings.Add(reader.GetAttribute("E"));
                _tempStrings.Add(reader.GetAttribute("F"));
            }
            
        }

        return _tempStrings;
    }

    public MenuLoadXMLMapData ReturnPlayerData(int _levelUrlNum)
    {
        MenuLoadXMLMapData _tempData = new MenuLoadXMLMapData();

        return _tempData;
    }
    public MenuLoadXMLMapData ReturnEndData(int _levelUrlNum)
    {
        MenuLoadXMLMapData _tempData = new MenuLoadXMLMapData();

        return _tempData;
    }
    public List<MenuLoadXMLMapData> ReturnLevelsData(int _levelUrlNum)
    {
        List<MenuLoadXMLMapData> _tempData = new List<MenuLoadXMLMapData>();

        return _tempData;
    }
    public List<MenuLoadXMLMapData> ReturnTowersData(int _levelUrlNum)
    {
        List<MenuLoadXMLMapData> _tempData = new List<MenuLoadXMLMapData>();

        return _tempData;
    }
    public List<MenuLoadXMLMapData> ReturnTargetData(int _levelUrlNum)
    {
        List<MenuLoadXMLMapData> _tempData = new List<MenuLoadXMLMapData>();

        return _tempData;
    }

    // Assigns transform data from a reader subtree to the specified object
    private void AssignTransform(GameObject obj, XmlReader reader)
    {
        // Position
        Vector3 position = Vector3.zero;
        // Rotation
        float rotation = 0;
        // Scale
        Vector3 scale = obj.transform.localScale;

        // While there are nodes to read
        while (reader.Read())
        {
            // If the node is an open node
            if (reader.IsStartElement())
            {
                // Check each node and retrieve the data
                switch (reader.Name)
                {
                    case "Position":
                        position = new Vector3(float.Parse(reader.GetAttribute("x"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign),
                            float.Parse(reader.GetAttribute("y"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign),
                            float.Parse(reader.GetAttribute("z"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign));
                        break;

                    case "Rotation":
                        reader.Read();
                        rotation = float.Parse(reader.Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        break;

                    case "Scale":
                        scale.x *= float.Parse(reader.GetAttribute("x"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        scale.y *= float.Parse(reader.GetAttribute("y"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        scale.z *= float.Parse(reader.GetAttribute("z"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        break;
                }
            }
        }

        // Set the Position, Rotation and Scale
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
        obj.transform.localScale = scale;
    }
}
