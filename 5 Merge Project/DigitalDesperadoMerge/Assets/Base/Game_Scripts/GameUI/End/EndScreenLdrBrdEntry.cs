using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenLdrBrdEntry : MonoBehaviour {

    [SerializeField] private Text Title;
    [SerializeField] private Text Tag;
    [SerializeField] private Text Sec;
    [SerializeField] private Text Fra;
    [SerializeField] private Text Sht;

    public void SetData(EndScreenLdrBrd.LdrBrdStat _statData, Color _beat, Color _noBeat)
    {
        if(_statData.Tag.Contains(" "))
            Tag.text = _statData.Tag.Replace(' ', '_');

        Tag.text = _statData.Tag;
        Sec.text = _statData.Secs.ToString("00.00");
        Fra.text = _statData.Frames.ToString("0000");
        Sht.text = _statData.Shots.ToString("000");

        if(_statData.Beaten)
        {
            Title.color = _beat;
            Tag.GetComponentInParent<Image>().color = _beat;
            Sec.GetComponentInParent<Image>().color = _beat;
            Fra.GetComponentInParent<Image>().color = _beat;
            Sht.GetComponentInParent<Image>().color = _beat;
        }
        else
        {
            Title.color = _noBeat;
            Tag.GetComponentInParent<Image>().color = _noBeat;
            Sec.GetComponentInParent<Image>().color = _noBeat;
            Fra.GetComponentInParent<Image>().color = _noBeat;
            Sht.GetComponentInParent<Image>().color = _noBeat;
        }
    }
    public void SetRanked(Color _ranked)
    {
        Title.color = _ranked;
        Tag.GetComponentInParent<Image>().color = _ranked;
        Sec.GetComponentInParent<Image>().color = _ranked;
        Fra.GetComponentInParent<Image>().color = _ranked;
        Sht.GetComponentInParent<Image>().color = _ranked;
    }
}
