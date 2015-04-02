using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Script which contains urls to all levels in the levels folder
public class LoadedLevels : MonoBehaviour 
{
    private List<string> m_LvlUrls = new List<string>();

    private List<string> m_LvlNams = new List<string>();
    private string m_LvlMaps = "map";

    [SerializeField] private StatsScript m_StatsObject;
    [SerializeField] private MapUI m_MapObject;
    [SerializeField] private List<LevelsNameUI> m_TextObjs = new List<LevelsNameUI>();

    [SerializeField] private List<string> sTags = new List<string>(6);
    [SerializeField] private List<string> sSecs = new List<string>(6);
    [SerializeField] private List<string> sFras = new List<string>(6);
    [SerializeField] private List<string> sShts = new List<string>(6);

    private int m_CurrLvlUrl = 0;
    public int iCurrentLvl { get { return m_CurrLvlUrl; } }

    void Start ()
    {
        for (int i = 0; i < 10; i++ )
        {
            m_LvlUrls.Add("url" + i.ToString());
            m_LvlNams.Add("Level " + i.ToString());
        }

        vUpdateData();
    }

    //get name
    public string sGetCurrUrlName(int _valueMod)
    {
        if ( (m_CurrLvlUrl + _valueMod) < 0 )
        {
            return m_LvlNams[m_LvlUrls.Count - 1];
        }
        else if ( (m_CurrLvlUrl + _valueMod) > m_LvlUrls.Count )
        {
            return m_LvlNams[0];
        }
        else
        {
            return m_LvlNams[(m_CurrLvlUrl + _valueMod)];
        }
    }

    //alter the level to display, then update data
    public void vChangeCurrentLevel(int _changeByValue)
    {
        if ((m_CurrLvlUrl + _changeByValue) >= m_LvlUrls.Count)
        {
            m_CurrLvlUrl = 0;
        }
        else if ((m_CurrLvlUrl + _changeByValue) < 0)
        {
            m_CurrLvlUrl = m_LvlUrls.Count - 1;
        }
        else
        {
            m_CurrLvlUrl += _changeByValue;
        }

        vUpdateData();
    }

    //update levelname display texts, tell stats to refresh with new data, tell map to generate anew
    private void vUpdateData()
    {
        foreach(LevelsNameUI obj in m_TextObjs)
        {
            obj.SendMessage("vGetText");
        }

        vGetNewLevelStats();
        m_StatsObject.vSetData(sTags,sSecs,sFras,sShts);
        m_MapObject.vRefresh(vGetNewMapStats());
    }

    //placeholder, currently just randomises times into strings with generic tags
    private void vGetNewLevelStats()
    {
        for (int l = 0; l < sTags.Count; l++ )
        {
            sTags[l] = "TG" + Random.Range(0, 9).ToString();
            sSecs[l] = Random.Range(0.0f, 10.0f).ToString();
            sFras[l] = Random.Range(1000, 10000).ToString();
            sShts[l] = Random.Range(5, 30).ToString();
            /*
            Debug.Log(
                "vGetNewLevelStats new stats | Tag: " + sTags[l]
                + " | Secs: " + sSecs[l]
                + " | Fras: " + sFras[l]
                + " | Shts: " + sShts[l]);
            */
        }
    }

    //placeholder, currently gives map a random set of coordinates to use
    private Vector2 vGetNewMapStats()
    {
        Vector2 _temp = new Vector2(Random.Range(-50f,+50f), Random.Range(-50f,+50f));
        return _temp;
    }
}
