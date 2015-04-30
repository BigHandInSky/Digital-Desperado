using System.Collections.Generic;
using System.Globalization;
using System.Xml;

using UnityEngine;

// Loads leaderboard data from the level data and saves the updated leaderboard to the xml

public class SaveLevelData : MonoBehaviour 
{
    // Level Xml File Url
    string sLevelDataUrl = "";

    // Number of Leaderboard Entries
    const int I_ENTRIES = 6;
    // Array of stat node attribute names
    string[] AS_ATTRIBUTE_NAMES = {"A", "B", "C", "D", "E", "F"};

    [SerializeField] private EndScreenLdrBrd LeaderBoard;

    private string CurrTag;
    private float CurrSecs;
    private int CurrFrames;
    private int CurrShots;

    private EndScreenLdrBrd.LdrBrdStat[] DataStats = new EndScreenLdrBrd.LdrBrdStat[0];

    public void SaveData(string _EntryTag)
    {
        //get current data
        CurrTag = _EntryTag;
        CurrSecs = GameData.Instance.fTimeScsAndPenalty;
        CurrFrames = GameData.Instance.iTimeFr;
        CurrShots = GameData.Instance.iBullsShot;

        //get data from leaderboard
        DataStats = LeaderBoard.LdrBrdStats;

        //order current data into leaderboard by seconds
        for (int loop = 0; loop < DataStats.Length; loop++)
        {
            //if current < loop's value
            if (CurrSecs < DataStats[loop].Secs)
            {
                for (int subloop = DataStats.Length - 1; subloop >= loop; subloop--)
                {
                    if (subloop + 1 < DataStats.Length)
                    {
                        DataStats[subloop + 1] = DataStats[subloop];
                    }
                }

                //set loop value as current
                DataStats[loop].Tag = CurrTag;
                DataStats[loop].Secs = CurrSecs;
                DataStats[loop].Frames = CurrFrames;
                DataStats[loop].Shots = CurrShots;
                
                break;
            }
        }

        SaveLeaderboard();
    }

    // Saves the leaderboard back into the level data
    private void SaveLeaderboard()
    {
        sLevelDataUrl = GameSettings.Instance.LoadLevelUrl;

        bool _tagsSaved = false;
        bool _secsSaved = false;
        bool _frassSaved = false;
        bool _shotsSaved = false;

        // Level xml document
        XmlDocument levelDoc = new XmlDocument();
        // Load document from url
        levelDoc.Load(sLevelDataUrl);

        foreach (XmlNode node in levelDoc.ChildNodes)
        {
            if (_tagsSaved
                && _secsSaved
                && _frassSaved
                && _shotsSaved)
                break;

            foreach (XmlNode subnode in node.ChildNodes)
            {
                if (subnode.Name == "Stats")
                {
                    // Loop through each stat node
                    foreach (XmlNode statNode in subnode.ChildNodes)
                    {
                        // Check the name of each node 
                        switch (statNode.Name)
                        {
                            case "Tags":
                                //loop through structs
                                for (int loop = 0; loop < DataStats.Length; loop++)
                                {
                                    //foreach one make a new attribute
                                    XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[loop]);
                                    //set value as data
                                    attribute.Value = DataStats[loop].Tag;
                                    //append to node
                                    statNode.Attributes.Append(attribute);

                                    if (loop == DataStats.Length - 1)
                                        _tagsSaved = true;
                                }
                                break;

                            case "Secs":
                                //loop through structs
                                for (int loop = 0; loop < DataStats.Length; loop++)
                                {
                                    //foreach one make a new attribute
                                    XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[loop]);
                                    //set value as data
                                    attribute.Value = DataStats[loop].Secs.ToString("00.00");
                                    //append to node
                                    statNode.Attributes.Append(attribute);

                                    if (loop == DataStats.Length - 1)
                                        _secsSaved = true;
                                }
                                break;

                            case "Fras":
                                //loop through structs
                                for (int loop = 0; loop < DataStats.Length; loop++)
                                {
                                    //foreach one make a new attribute
                                    XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[loop]);
                                    //set value as data
                                    attribute.Value = DataStats[loop].Frames.ToString("00000");
                                    //append to node
                                    statNode.Attributes.Append(attribute);

                                    if (loop == DataStats.Length - 1)
                                        _frassSaved = true;
                                }
                                break;

                            case "Shts":
                                //loop through structs
                                for (int loop = 0; loop < DataStats.Length; loop++)
                                {
                                    //foreach one make a new attribute
                                    XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[loop]);
                                    //set value as data
                                    attribute.Value = DataStats[loop].Shots.ToString("000");
                                    //append to node
                                    statNode.Attributes.Append(attribute);

                                    if (loop == DataStats.Length - 1)
                                        _shotsSaved = true;
                                }
                                break;
                        }
                    }
                }
            }
        }

        

        // Save the document
        levelDoc.Save(sLevelDataUrl);
    }
}
