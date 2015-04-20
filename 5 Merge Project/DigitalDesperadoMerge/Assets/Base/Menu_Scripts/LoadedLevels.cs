using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Script which contains urls to all levels in the levels folder
public class LoadedLevels : MonoBehaviour
{
    private static LoadedLevels m_DataInstance;
    public static LoadedLevels Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<LoadedLevels>(); }
            return m_DataInstance;
        }
    }

    [SerializeField] private string sSceneToLoad;

    [SerializeField] private StatsScript m_StatsObject;
    [SerializeField] private MapUI m_MapObject;
    [SerializeField] private List<LevelsNameUI> m_TextObjs = new List<LevelsNameUI>();

    [SerializeField] private List<string> sTags = new List<string>(6);
    [SerializeField] private List<string> sSecs = new List<string>(6);
    [SerializeField] private List<string> sFras = new List<string>(6);
    [SerializeField] private List<string> sShts = new List<string>(6);
    
    private int m_LevelsCount;
    private int m_CurrLvlUrl = 0;
    public int iCurrentLvl { get { return m_CurrLvlUrl; } }
    
    void Awake()
    {
        m_DataInstance = this;
    }

    public void vLoadLevel()
    {
        GameSettings.Instance.SetLevelUrl(MenuLoadLevelsFromXML.Instance.Urls[m_CurrLvlUrl]);
        Application.LoadLevel(sSceneToLoad);
    }
    //get name
    public string sGetCurrUrlName(int _valueMod)
    {
        if ( (m_CurrLvlUrl + _valueMod) < 0 )
        {
            return MenuLoadLevelsFromXML.Instance.Names[m_LevelsCount - 1];
        }
        else if ((m_CurrLvlUrl + _valueMod) > m_LevelsCount)
        {
            return MenuLoadLevelsFromXML.Instance.Names[0];
        }
        else
        {
            return MenuLoadLevelsFromXML.Instance.Names[(m_CurrLvlUrl + _valueMod)];
        }
    }

    public string sGetFullUrl()
    {
        //Debug.Log("sGetFullUrl");
        return MenuLoadLevelsFromXML.Instance.sLevelsFolderUrl;
    }

    //alter the level to display, then update data
    public void vChangeCurrentLevel(int _changeByValue)
    {
        if ((m_CurrLvlUrl + _changeByValue) > m_LevelsCount - 1)
        {
            m_CurrLvlUrl = 0;
        }
        else if ((m_CurrLvlUrl + _changeByValue) < 0)
        {
            m_CurrLvlUrl = m_LevelsCount - 1;
        }
        else
        {
            m_CurrLvlUrl += _changeByValue;
        }

        vUpdateData();
    }

    //update levelname display texts, tell stats to refresh with new data, tell map to generate anew
    public void vUpdateData()
    {
        m_LevelsCount = MenuLoadLevelsFromXML.Instance.Names.Count;

        foreach(LevelsNameUI obj in m_TextObjs)
        {
            obj.SendMessage("vGetText");
        }

       vGetNewLevelStats();
       m_StatsObject.vSetData(sTags,sSecs,sFras,sShts);

       vGetNewMapData();
    }
    
    //placeholder, currently just randomises times into strings with generic tags
    private void vGetNewLevelStats()
    {
        sTags = MenuLoadLevelsFromXML.Instance.GetLevelStats(iCurrentLvl, MenuLoadLevelsFromXML.StatsType.Tags);
        sSecs = MenuLoadLevelsFromXML.Instance.GetLevelStats(iCurrentLvl, MenuLoadLevelsFromXML.StatsType.Secs);
        sFras = MenuLoadLevelsFromXML.Instance.GetLevelStats(iCurrentLvl, MenuLoadLevelsFromXML.StatsType.Fras);
        sShts = MenuLoadLevelsFromXML.Instance.GetLevelStats(iCurrentLvl, MenuLoadLevelsFromXML.StatsType.Shts);
    }

    //placeholder, currently gives map a random set of coordinates to use
    private void vGetNewMapData()
    {
        m_MapObject.vClearMap();

        List<MenuLoadLevelsFromXML.MenuLoadXMLMapData> _mapList = MenuLoadLevelsFromXML.Instance.GetLevelObjs(iCurrentLvl);
        List<MenuLoadLevelsFromXML.MenuLoadXMLMapData> _mapListLate = new List<MenuLoadLevelsFromXML.MenuLoadXMLMapData>();

        foreach(MenuLoadLevelsFromXML.MenuLoadXMLMapData obj in _mapList)
        {
            if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.Play)
                m_MapObject.vSetupMapUIPlayer(obj.Position, obj.Rotation.y);

            else if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.Targ)
                m_MapObject.vSetupMapUITarget(obj.Position, obj.Rotation.y);

            else if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.Levl)
                m_MapObject.vSetupMapUILevel(obj.Position, obj.Scale, obj.Rotation.y);

            else if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.Towr)
                m_MapObject.vSetupMapUITower(obj.Position, obj.Scale, obj.Rotation.y);

            else if (obj.Type == MenuLoadLevelsFromXML.MapDataObjType.EndT)
                m_MapObject.vSetupMapUIEndTower(obj.Position, obj.Rotation.y);
        }
    }

    public void vResetToZero()
    {
        m_CurrLvlUrl = 0;
        vUpdateData();
    }
}
