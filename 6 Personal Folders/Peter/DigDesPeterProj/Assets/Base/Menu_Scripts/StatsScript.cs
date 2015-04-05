using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//given a set of strings/variables, will pass onto the right texts
public class StatsScript : MonoBehaviour 
{
    private int iState = 0;

    [SerializeField]
    private float fRotateStatsTime = 2f;

    [SerializeField] private Image imStatsObj;

    [SerializeField]
    private List<Sprite> m_StatImgSprites = new List<Sprite>(4);

    [SerializeField]
    private List<Text> txStatTexts = new List<Text>(6);

    public List<string> sTags;
    public List<string> sSecs;
    public List<string> sFras;
    public List<string> sShts;

    void Start()
    {/*
        for (int i = 0; i < 6; i++ )
        {
            sTags.Add("?");
            sSecs.Add("?");
            sFras.Add("?");
            sShts.Add("?");
        }*/

        StartCoroutine("ieRotateStats");
    }

    public void vSetData(List<string> _tags, List<string> _secs, List<string> _frames, List<string> _shots)
    {
        sTags.Clear();
        sSecs.Clear();
        sFras.Clear();
        sShts.Clear();

        sTags = _tags;
        sSecs = _secs;
        sFras = _frames;
        sShts = _shots;
        vUpdateTexts();
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

        vUpdateTexts();
    }

    private void vUpdateTexts()
    {
        int _switchLoopInt = 0;

        foreach (Text obj in txStatTexts)
        {
            switch (iState)
            {
                case 0:
                    obj.text = sTags[_switchLoopInt];                    
                    break;

                case 1:
                    obj.text = sSecs[_switchLoopInt];                    
                    break;

                case 2:
                    obj.text = sFras[_switchLoopInt];                    
                    break;

                case 3:
                    obj.text = sShts[_switchLoopInt];                    
                    break;
            }

            _switchLoopInt++;
        }
        
    }
}
