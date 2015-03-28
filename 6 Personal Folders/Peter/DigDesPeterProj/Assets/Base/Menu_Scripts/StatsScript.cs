using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//given a set of strings/variables, will pass onto the right texts
public class StatsScript : MonoBehaviour 
{
    public struct StatsFormat
    {
        public StatsFormat(int _listSize)
        {
            sTags = new List<string>(_listSize);
            sSecs = new List<string>(_listSize);
            sFras = new List<string>(_listSize);
            sShts = new List<string>(_listSize);
        }

        public List<string> sTags;
        public List<string> sSecs;
        public List<string> sFras;
        public List<string> sShts;
    }

    private int iState = 0;
    private StatsFormat m_StatData;

    [SerializeField]
    private float fRotateStatsTime = 2f;

    [SerializeField] private Image imStatsObj;

    [SerializeField]
    private List<Sprite> m_StatImgSprites = new List<Sprite>(4);

    [SerializeField]
    private List<Text> txStatTexts = new List<Text>(6);
    
    void Start()
    {
        StartCoroutine("ieRotateStats");
    }

    public void vSetData(StatsFormat _newData)
    {
        m_StatData = _newData;
    }

    IEnumerator ieRotateStats()
    {
        float fTimer = fRotateStatsTime;

        while (true)
        {
            fTimer -= Time.deltaTime;

            if(fTimer < 0f)
            {
                fTimer = fRotateStatsTime;
                vRefresh();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void vRefresh()
    {
        iState++;

        if (iState >= m_StatImgSprites.Count)
            iState = 0;

        imStatsObj.sprite = m_StatImgSprites[iState];

        int _switchLoopInt = 0;

        switch(iState)
        {
            case 0:
                foreach (Text obj in txStatTexts)
                {
                    obj.text = m_StatData.sTags[_switchLoopInt];
                    _switchLoopInt++;
                }
                break;
            case 1:
                foreach (Text obj in txStatTexts)
                {
                    obj.text = m_StatData.sSecs[_switchLoopInt];
                    _switchLoopInt++;
                }
                break;
            case 2:
                foreach (Text obj in txStatTexts)
                {
                    obj.text = m_StatData.sFras[_switchLoopInt];
                    _switchLoopInt++;
                }
                break;
            case 3:
                foreach (Text obj in txStatTexts)
                {
                    obj.text = m_StatData.sShts[_switchLoopInt];
                    _switchLoopInt++;
                }
                break;
        }
    }
}
