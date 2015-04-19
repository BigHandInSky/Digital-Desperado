using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

public class GameLdrBrdGetData : MonoBehaviour {

    public struct LdrBrdStat
    {
        public string Tag;
        public float Secs;
        public int Frames;
        public int Shots;
    }

    [SerializeField] private List<Text> Tags = new List<Text>();
    [SerializeField] private List<Text> Secs = new List<Text>();
    [SerializeField] private List<Text> Fras = new List<Text>();
    [SerializeField] private List<Text> Shts = new List<Text>();

    [SerializeField] private List<Text> CurrData = new List<Text>();

    private LdrBrdStat[] Stats = new LdrBrdStat[6];
    public LdrBrdStat[] LdrBrdStats { get { return Stats; } }

	public void Load()
    {
        Debug.Log("loaded");

        CurrData[0].text = GameData.Instance.fTimeScs.ToString("00.00");
        CurrData[1].text = GameData.Instance.iTimeFr.ToString("00000");
        CurrData[2].text = GameData.Instance.iBullsShot.ToString("000");

        XmlReader reader = XmlReader.Create(GameSettings.Instance.LoadLevelUrl);
        while (reader.Read())
        {
            if (reader.Name == "Tags")
            {
                Tags[0].text = Stats[0].Tag = reader.GetAttribute("A");
                Tags[1].text = Stats[1].Tag = reader.GetAttribute("B");
                Tags[2].text = Stats[2].Tag = reader.GetAttribute("C");
                Tags[3].text = Stats[3].Tag = reader.GetAttribute("D");
                Tags[4].text = Stats[4].Tag = reader.GetAttribute("E");
                Tags[5].text = Stats[5].Tag = reader.GetAttribute("F");
            }
            else if (reader.Name == "Secs")
            {
                Stats[0].Secs = float.Parse(reader.GetAttribute("A"));
                Stats[1].Secs = float.Parse(reader.GetAttribute("B"));
                Stats[2].Secs = float.Parse(reader.GetAttribute("C"));
                Stats[3].Secs = float.Parse(reader.GetAttribute("D"));
                Stats[4].Secs = float.Parse(reader.GetAttribute("E"));
                Stats[5].Secs = float.Parse(reader.GetAttribute("F"));

                Secs[0].text = reader.GetAttribute("A");
                Secs[1].text = reader.GetAttribute("B");
                Secs[2].text = reader.GetAttribute("C");
                Secs[3].text = reader.GetAttribute("D");
                Secs[4].text = reader.GetAttribute("E");
                Secs[5].text = reader.GetAttribute("F");
            }
            else if (reader.Name == "Fras")
            {
                Stats[0].Frames = int.Parse(reader.GetAttribute("A"));
                Stats[1].Frames = int.Parse(reader.GetAttribute("B"));
                Stats[2].Frames = int.Parse(reader.GetAttribute("C"));
                Stats[3].Frames = int.Parse(reader.GetAttribute("D"));
                Stats[4].Frames = int.Parse(reader.GetAttribute("E"));
                Stats[5].Frames = int.Parse(reader.GetAttribute("F"));

                Fras[0].text = reader.GetAttribute("A");
                Fras[1].text = reader.GetAttribute("B");
                Fras[2].text = reader.GetAttribute("C");
                Fras[3].text = reader.GetAttribute("D");
                Fras[4].text = reader.GetAttribute("E");
                Fras[5].text = reader.GetAttribute("F");
            }
            else if (reader.Name == "Shts")
            {
                Stats[0].Shots = int.Parse(reader.GetAttribute("A"));
                Stats[1].Shots = int.Parse(reader.GetAttribute("B"));
                Stats[2].Shots = int.Parse(reader.GetAttribute("C"));
                Stats[3].Shots = int.Parse(reader.GetAttribute("D"));
                Stats[4].Shots = int.Parse(reader.GetAttribute("E"));
                Stats[5].Shots = int.Parse(reader.GetAttribute("F"));

                Shts[0].text = reader.GetAttribute("A");
                Shts[1].text = reader.GetAttribute("B");
                Shts[2].text = reader.GetAttribute("C");
                Shts[3].text = reader.GetAttribute("D");
                Shts[4].text = reader.GetAttribute("E");
                Shts[5].text = reader.GetAttribute("F");
            }
        }
        reader.Close();

    }
}
