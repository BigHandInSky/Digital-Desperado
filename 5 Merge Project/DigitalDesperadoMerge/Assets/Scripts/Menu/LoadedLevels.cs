﻿using UnityEngine;
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
    private int m_CurrLvlUrl;
    public int iCurrentLvl { get { return m_CurrLvlUrl; } }
    
    void Start()
    {
        m_CurrLvlUrl = GameSettings.Instance.LevelInt;
        m_DataInstance = this;
    }

    public void vLoadLevel()
    {
        GameSettings.Instance.SetUrls(MenuLoadLevelsFromXML.Instance.Urls);
        GameSettings.Instance.LevelInt = m_CurrLvlUrl;
        Application.LoadLevel(sSceneToLoad);
    }
    //get name
    public string sGetCurrUrlName(int _valueMod = 0)
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
    public void vSelectLevelByNum(int _arrayValue)
    {
        if (_arrayValue  > m_LevelsCount - 1
            || _arrayValue < 0)
        {
            m_CurrLvlUrl = 0;
        }
        else
        {
            m_CurrLvlUrl = _arrayValue;
        }

        vUpdateData();
    }

    //update levelname display texts, tell stats to refresh with new data, tell map to generate anew
    public void vUpdateData()
    {
        m_LevelsCount = MenuLoadLevelsFromXML.Instance.Names.Count;

        if (m_CurrLvlUrl > m_LevelsCount)
            m_CurrLvlUrl = 0;

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
        m_MapObject.Setup();
    }

    public void vResetToZero()
    {
        m_CurrLvlUrl = 0;
        GameSettings.Instance.LevelInt = 0;
        vUpdateData();
    }
}
