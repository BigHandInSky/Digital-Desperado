using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenPenalty : MonoBehaviour 
{
    [SerializeField] private Text MissedText;
    [SerializeField] private Text TimeText;

    private const string sMainText = "No Penalty";
    private const string sMissedEnd = " Missed";
    private const string sTimeEnd = " Secs";

    public void Setup()
    {
        if (GameData.Instance.iTargsLft > 0)
        {
            GetComponent<Text>().text = "Penalty";
            MissedText.text = GameData.Instance.iTargsLft.ToString() + sMissedEnd;
            TimeText.text = "+" + GameData.Instance.fTimePenalty.ToString() + sTimeEnd;
        }
        else
        {
            GetComponent<Text>().text = sMainText;
            MissedText.text = " ";
            TimeText.text = " ";
        }
    }
}
