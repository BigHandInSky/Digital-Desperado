using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Script which contains urls to all levels in the levels folder
public class LoadedLevels : MonoBehaviour 
{
    private List<string> m_LvlUrls = new List<string>();

    private List<string> m_LvlNams = new List<string>();
    private StatsScript.StatsFormat m_LvlStts = new StatsScript.StatsFormat(6);
    private string m_LvlMaps = "map";

    [SerializeField] private StatsScript m_StatsObject;
    [SerializeField] private MapUI m_MapObject;

    private int m_CurrLvlUrl = 0;
    public int iCurrentLvl { get { return m_CurrLvlUrl; } }

    void Start ()
    {
        for (int i = 0; i < 10; i++ )
        {
            m_LvlUrls.Add("url" + i.ToString());
            m_LvlNams.Add("Level " + i.ToString());
            //m_LvlStts.Add("url " + i.ToString());
            //m_LvlMaps.Add("lvlmap " + i.ToString());
        }
    }

    //get name
    public string sGetCurrUrlName()
    {
        return m_LvlUrls[m_CurrLvlUrl];
    }

    public void vChangeCurrentLevel(int _changeByValue)
    {
        if (
            (m_CurrLvlUrl + _changeByValue) >= m_LvlUrls.Count
            || (m_CurrLvlUrl + _changeByValue) < 0
            )
        {
            m_CurrLvlUrl = 0;
        }
        else
        {
            m_CurrLvlUrl += _changeByValue;
        }
    }

    private void vUpdateData()
    {
        m_StatsObject.vSetData(m_LvlStts);
        m_MapObject.vRefresh();
    }
}
