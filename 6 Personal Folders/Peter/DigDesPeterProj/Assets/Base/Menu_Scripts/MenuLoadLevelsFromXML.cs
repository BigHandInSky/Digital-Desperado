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
    private bool CheckXML(string _url)
    {
        XmlReader reader = XmlReader.Create(_url);
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
        }
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
                        _reader.Read();
                        _rot = float.Parse(_reader.Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
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

        Debug.Log("MenuLoadLevelsFromXML - GetLevelObjValues - Position: " + _temp.Position);
        Debug.Log("MenuLoadLevelsFromXML - GetLevelObjValues - Rotation: " + _temp.Rotation);
        Debug.Log("MenuLoadLevelsFromXML - GetLevelObjValues - Scale: " + _temp.Scale);
        Debug.Log("MenuLoadLevelsFromXML - GetLevelObjValues - Type: " + _temp.Type);
        return _temp;
    }
}
