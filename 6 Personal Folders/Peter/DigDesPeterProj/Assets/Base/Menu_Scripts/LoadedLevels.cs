using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Script which contains urls to all levels in the levels folder
public class LoadedLevels : MonoBehaviour 
{
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

        m_MapObject.vClearMap();
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
    private Vector2 vGetNewMapStats()
    {
        Vector2 _temp = new Vector2(Random.Range(-50f,+50f), Random.Range(-50f,+50f));
        return _temp;
    }
}
