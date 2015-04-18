using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameLdrBrdGetData : MonoBehaviour {

    [SerializeField] private List<Text> Tags = new List<Text>();
    [SerializeField] private List<Text> Secs = new List<Text>();
    [SerializeField] private List<Text> Fras = new List<Text>();
    [SerializeField] private List<Text> Shts = new List<Text>();

    [SerializeField] private List<Text> CurrData = new List<Text>();

	public void Load()
    {
        Debug.Log("loaded");

        CurrData[0].text = GameData.Instance.fTimeScs.ToString("00.00");
        CurrData[1].text = GameData.Instance.iTimeFr.ToString("00000");
        CurrData[2].text = GameData.Instance.iBullsShot.ToString("000");

        for (int lop = 0; lop < 6; lop++ )
        {
            Tags[lop].text = "XX" + Random.Range(0, 9).ToString();
            Secs[lop].text = Random.Range(0.0f, 100.0f).ToString("00.00");
            Fras[lop].text = Random.Range(100, 900).ToString("00000");
            Shts[lop].text = Random.Range(0, 99).ToString("000");
        }
    }
}
