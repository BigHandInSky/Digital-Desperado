using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.IO;

//using UnityEditor;
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

    private string sDefaultFolderPath;
    public string sLevelsFolderUrl { get { return sDefaultFolderPath; } }
    string sFileType = ".xml";

    public enum MapDataObjType
    {
        Play,
        EndT,
        Levl,
        Towr,
        Targ
    }
    public struct MenuLoadXMLMapData
    {
        public MapDataObjType Type;
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
    }

    [SerializeField] private DirectoryInfo FolderInfo;
    [SerializeField] private List<string> FileUrls;
    public List<string> Urls { get { return FileUrls; } }
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

        sDefaultFolderPath = Application.dataPath + "/StreamingAssets/XML";

        GetFolder();
    }

    public void GetFolder()
    {
        FolderInfo = new DirectoryInfo(sDefaultFolderPath);
        int failedLevels = 0;
        
        if (FolderInfo != null)
        {
            FileInfo[] infos = FolderInfo.GetFiles("*.xml");
            FileUrls.Clear();
            FileNames.Clear();

            foreach (FileInfo obj in infos)
            {
                if (CheckEntireXMLLevel(obj.FullName))
                {
                    FileUrls.Add(obj.FullName);
                    FileNames.Add(Path.GetFileNameWithoutExtension(obj.Name));
                }
                else
                {
                    failedLevels++;
                }
            }

            if (failedLevels > 0)
            {
                ErrorScript.Instance.OpenError(ErrorScript.Errors.InvalidLevel, "Failed to load " + failedLevels.ToString() + " Levels. \nCause: Invalid Level File");
            }
        }
        else
        {
            Debug.LogError("FolderInfo checked to be null");
        }

        if (GameSettings.Instance.bComeFromGame)
            LoadedLevels.Instance.vUpdateData();
    }
    
	public bool CheckUrl(string _url, bool _XML)
	{
        if (_XML && !_url.Contains(".xml"))
            return false;

		if( _url == ""
            || Path.GetFullPath(_url) == null )
			return false;
		else 
			return true;
	}
    private bool CheckXML(string _url)
    {
        return true;
        /*XmlReader reader = XmlReader.Create(_url);
        reader.Read();
        if (reader.Name != "LevelData")
        {
            Debug.LogError("File not a supported level");

            return false;
        }
        else
        {
            reader.Close();
            return true;
        }*/
    }

    private bool CheckEntireXMLLevel(string _url)
    {
        bool leveldata = false;
        bool statsNode = false;
        bool playerNode = false;
        bool endNode = false;
        bool platformsNode = false;
        bool targetsNode = false;
        bool towersNode = false;

        using (XmlReader reader = XmlReader.Create(_url))
        {/*
            reader.Read();

            if (reader.Name == "LevelData")
            {
                leveldata = true;
            }
            */
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "LevelData":
                            leveldata = true;
                            break;

                        case "Stats":
                            statsNode = true;
                        break;

                        case "PlayerStart":
                            playerNode = true;
                        break;

                        case "Goal":
                            endNode = true;
                        break;

                        case "Platforms":
                            platformsNode = true;
                        break;

                        case "Towers":
                            towersNode = true;
                        break;

                        case "Targets":
                            targetsNode = true;
                        break;
                    }
                }
            }
        }

        if (!leveldata)
        {
            Debug.LogError("File not a supported level - no root");
            return false;
        }

        if (!statsNode)
        {
            Debug.LogError("File not a supported level - no stats");
            return false;
        }

        if (!playerNode)
        {
            Debug.LogError("File not a supported level - no player");
            return false;
        }

        if (!endNode)
        {
            Debug.LogError("File not a supported level - no end");
            return false;
        }

        if (!platformsNode)
        {
            Debug.LogError("File not a supported level - no platform");
            return false;
        }

        if (!towersNode)
        {
            Debug.LogError("File not a supported level - no towers");
            return false;
        }

        if (!targetsNode)
        {
            Debug.LogError("File not a supported targets");
            return false;
        }

        return true;
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
        if (!CheckXML(FileUrls[_levelUrlNum]))
        {
            return _tempStrings;
        }

        XmlReader reader = XmlReader.Create(FileUrls[_levelUrlNum]);        
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
        reader.Close();

        return _tempStrings;
    }
    public List<MenuLoadXMLMapData> GetLevelObjs(int _levelUrlNum)
    {
        List<MenuLoadXMLMapData> _temps = new List<MenuLoadXMLMapData>();
        if (!CheckXML(FileUrls[_levelUrlNum]))
        {
            return _temps;
        }

        XmlReader reader = XmlReader.Create(FileUrls[_levelUrlNum]);   
        while (reader.Read())
        {
            switch (reader.Name)
            {
                case "PlayerStart":
                    MenuLoadXMLMapData _newDataPlay = GetLevelObjValues(reader.ReadSubtree(), MapDataObjType.Play);
                    _temps.Add(_newDataPlay);
                    break;

                case "Goal":
                    MenuLoadXMLMapData _newDataEndT = GetLevelObjValues(reader.ReadSubtree(), MapDataObjType.EndT);
                    _temps.Add(_newDataEndT);
                    break;

                case "Platform":
                    MenuLoadXMLMapData _newDataLevl = GetLevelObjValues(reader.ReadSubtree(), MapDataObjType.Levl);
                    _temps.Add(_newDataLevl);
                    break;

                case "Tower":
                    MenuLoadXMLMapData _newDataTowr = GetLevelObjValues(reader.ReadSubtree(), MapDataObjType.Towr);
                    _temps.Add(_newDataTowr);
                    break;

                case "Target":
                    MenuLoadXMLMapData _newDataTarg = GetLevelObjValues(reader.ReadSubtree(), MapDataObjType.Targ);
                    _temps.Add(_newDataTarg);
                    break;
            }
        }

        reader.Close();

        return _temps;
    }

    private MenuLoadXMLMapData GetLevelObjValues(XmlReader _reader, MapDataObjType _type)
    {
        MenuLoadXMLMapData _temp = new MenuLoadXMLMapData();
        _temp.Type = _type;

        float _rot = 0f;

        while (_reader.Read())
        {
            if (_reader.IsStartElement())
            {
                switch (_reader.Name)
                {
                    case "Position":
                        _temp.Position = new Vector3(float.Parse(_reader.GetAttribute("x"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign),
                            float.Parse(_reader.GetAttribute("y"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign),
                            float.Parse(_reader.GetAttribute("z"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign));
                        break;

                    case "Rotation":
                        if (_reader.AttributeCount > 0)
                        {
                            _rot = float.Parse(_reader.GetAttribute("y"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        }
                        else
                        {
                            _reader.Read();
                            _rot = float.Parse(_reader.Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        }
                            break;

                    case "Scale":
                        _temp.Scale.x = float.Parse(_reader.GetAttribute("x"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        _temp.Scale.y = float.Parse(_reader.GetAttribute("y"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        _temp.Scale.z = float.Parse(_reader.GetAttribute("z"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        break;
                }
            }
        }

        _temp.Rotation = new Quaternion(0f, _rot, 0f, 0f);

       /* Debug.Log("MenuLoadLevelsFromXML - GetLevelObjValues - Position: " + _temp.Position);
        Debug.Log("MenuLoadLevelsFromXML - GetLevelObjValues - Rotation: " + _temp.Rotation);
        Debug.Log("MenuLoadLevelsFromXML - GetLevelObjValues - Scale: " + _temp.Scale);
        Debug.Log("MenuLoadLevelsFromXML - GetLevelObjValues - Type: " + _temp.Type);*/
        return _temp;
    }
}
