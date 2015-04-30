using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenPenalty : MonoBehaviour 
{
    [SerializeField] private Text MissedText;
    [SerializeField] private Text TimeText;

    private const string sMissedEnd = " Missed";
    private const string sTimeEnd = " Secs";

    public void Setup()
    {
        if (GameData.Instance.iTargsLft > 0)
        {
            MissedText.text = GameData.Instance.iTargsLft.ToString() + sMissedEnd;
            TimeText.text = "+" + GameData.Instance.fTimePenalty.ToString() + sTimeEnd;
        }
        else
        {
            MissedText.text = "None" + sMissedEnd;
            TimeText.text = " ";
        }
    }
}
