using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenLdrBrdEntry : MonoBehaviour {

    private Color TimeUnBeaten;
    private Color TimeBeaten;

    [SerializeField] private Text Title;
    [SerializeField] private Text Tag;
    [SerializeField] private Text Sec;
    [SerializeField] private Text Fra;
    [SerializeField] private Text Sht;

    public void SetData(EndScreenLdrBrd.LdrBrdStat _statData, Color _beat, Color _noBeat)
    {
        TimeBeaten = _beat;
        TimeUnBeaten = _noBeat;

        if(_statData.Tag.Contains(" "))
            Tag.text = _statData.Tag.Replace(' ', '_');

        Tag.text = _statData.Tag;
        Sec.text = _statData.Secs.ToString("00.00");
        Fra.text = _statData.Frames.ToString("0000");
        Sht.text = _statData.Shots.ToString("000");

        if(_statData.Beaten)
        {
            Title.color = TimeBeaten;
            Tag.GetComponentInParent<Image>().color = TimeBeaten;
            Sec.GetComponentInParent<Image>().color = TimeBeaten;
            Fra.GetComponentInParent<Image>().color = TimeBeaten;
            Sht.GetComponentInParent<Image>().color = TimeBeaten;
        }
        else
        {
            Title.color = TimeUnBeaten;
            Tag.GetComponentInParent<Image>().color = TimeUnBeaten;
            Sec.GetComponentInParent<Image>().color = TimeUnBeaten;
            Fra.GetComponentInParent<Image>().color = TimeUnBeaten;
            Sht.GetComponentInParent<Image>().color = TimeUnBeaten;
        }
    }
}
