using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.IO;

using UnityEngine;
using UnityEngine.UI;


public class EndScreenLdrBrd : MonoBehaviour {

    [SerializeField] private Color TimeUnBeaten;
    [SerializeField] private Color TimeBeaten;

    public struct LdrBrdStat
    {
        public bool Beaten;
        public string Tag;
        public float Secs;
        public int Frames;
        public int Shots;
    }

    [SerializeField] private List<EndScreenLdrBrdEntry> Entries = new List<EndScreenLdrBrdEntry>();

    private LdrBrdStat[] Stats = new LdrBrdStat[6];
    public LdrBrdStat[] LdrBrdStats { get { return Stats; } }

    public void Setup()
    {
        XmlReader reader = XmlReader.Create(GameSettings.Instance.LoadLevelUrl);
        while (reader.Read())
        {
            if (reader.Name == "Tags")
            {
                Stats[0].Tag = reader.GetAttribute("A");
                Stats[1].Tag = reader.GetAttribute("B");
                Stats[2].Tag = reader.GetAttribute("C");
                Stats[3].Tag = reader.GetAttribute("D");
                Stats[4].Tag = reader.GetAttribute("E");
                Stats[5].Tag = reader.GetAttribute("F");
            }
            else if (reader.Name == "Secs")
            {
                Stats[0].Secs = float.Parse(reader.GetAttribute("A"));
                Stats[1].Secs = float.Parse(reader.GetAttribute("B"));
                Stats[2].Secs = float.Parse(reader.GetAttribute("C"));
                Stats[3].Secs = float.Parse(reader.GetAttribute("D"));
                Stats[4].Secs = float.Parse(reader.GetAttribute("E"));
                Stats[5].Secs = float.Parse(reader.GetAttribute("F"));
            }
            else if (reader.Name == "Fras")
            {
                Stats[0].Frames = int.Parse(reader.GetAttribute("A"));
                Stats[1].Frames = int.Parse(reader.GetAttribute("B"));
                Stats[2].Frames = int.Parse(reader.GetAttribute("C"));
                Stats[3].Frames = int.Parse(reader.GetAttribute("D"));
                Stats[4].Frames = int.Parse(reader.GetAttribute("E"));
                Stats[5].Frames = int.Parse(reader.GetAttribute("F"));
            }
            else if (reader.Name == "Shts")
            {
                Stats[0].Shots = int.Parse(reader.GetAttribute("A"));
                Stats[1].Shots = int.Parse(reader.GetAttribute("B"));
                Stats[2].Shots = int.Parse(reader.GetAttribute("C"));
                Stats[3].Shots = int.Parse(reader.GetAttribute("D"));
                Stats[4].Shots = int.Parse(reader.GetAttribute("E"));
                Stats[5].Shots = int.Parse(reader.GetAttribute("F"));
            }
        }
        reader.Close();

        int _loop = 0;
        foreach(EndScreenLdrBrdEntry _entry in Entries)
        {
            Stats[_loop].Beaten = (GameData.Instance.fTimeScsAndPenalty < Stats[_loop].Secs);

            _entry.SetData(Stats[_loop], TimeBeaten, TimeUnBeaten);
            _loop++;
        }
    }
}
