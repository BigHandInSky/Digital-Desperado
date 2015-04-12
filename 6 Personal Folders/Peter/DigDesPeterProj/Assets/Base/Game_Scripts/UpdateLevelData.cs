using System.Collections.Generic;
using System.Globalization;
using System.Xml;

using UnityEngine;

// Class storing data for a single leaderboard entry

public class LeaderboardEntry
{
    // Tag
    public string sTag;
    // Time in frames 
    public int iFrames;
    // Time in seconds
    public float fSecs;
    // Number of shots fired
    public int iShots;

    // Default constructor
    public LeaderboardEntry()
    {
        sTag = "";
        iFrames = 0;
        fSecs = 0;
        iShots = 0;
    }

    // Constructor assigning each stat
    public LeaderboardEntry(string tag, int frames, float time, int shots)
    {
        sTag = tag;
        iFrames = frames;
        fSecs = time;
        iShots = shots;
    }
}

// Loads leaderboard data from the level xml file, checks a new time in frames against
// the leaderboard and saves the updated leaderboard

public class UpdateLevelData : MonoBehaviour 
{
    // Level Xml File Url
    string sLevelDataUrl;

    // List of Leaderboard Entries
    List<LeaderboardEntry> lEntries;
    // Number of Leaderboard Entries
    const int I_ENTRIES = 6;
    // Array of stat node attribute names
    string[] AS_ATTRIBUTE_NAMES = {"A", "B", "C", "D", "E", "F"};

	// Initialization
	void Start () 
    {
        // Create the list of LeaderboardEntries
        lEntries = new List<LeaderboardEntry>();

        // Create each entry and add it to the list
        for (int i = 0; i < I_ENTRIES; i++)
        {
            LeaderboardEntry entry = new LeaderboardEntry();
            lEntries.Add(entry);
        }

        // Retrieve the LeaderboardData
        RetrieveLeaderboardData();
	}

    // Collision Enter
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckLeaderboard();
        }
    }

    // Loads Leaderboard Data
    private void RetrieveLeaderboardData()
    {
        using (XmlReader reader = XmlReader.Create(sLevelDataUrl))
        {
            // Loop through the document
            while (reader.Read())
            {
                // Check the name of each node
                switch (reader.Name)
                {
                    case "Tags":
                        // Loop through each attribute and retrieve its value
                        for (int i = 0; i < I_ENTRIES; i++)
                        {
                            lEntries[i].sTag = reader.GetAttribute(AS_ATTRIBUTE_NAMES[i]);
                        }
                    break;

                    case "Fras":
                        // Loop through each attribute and retrieve its value
                        for (int i = 0; i < I_ENTRIES; i++)
                        {
                            lEntries[i].iFrames = int.Parse(reader.GetAttribute(AS_ATTRIBUTE_NAMES[i]));
                        }
                    break;

                    case "Secs":
                        // Loop through each attribute and retrieve its value
                        for (int i = 0; i < I_ENTRIES; i++)
                        {
                            lEntries[i].fSecs = float.Parse(reader.GetAttribute(AS_ATTRIBUTE_NAMES[i]), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        }
                    break;

                    case "Shts":
                        // Loop through each attribute and retrieve its value
                        for (int i = 0; i < I_ENTRIES; i++)
                        {
                            lEntries[i].iShots = int.Parse(reader.GetAttribute(AS_ATTRIBUTE_NAMES[i]));
                        }
                    break;
                }
            }
        }
    }

    // Checks a time in frames against the leaderboard and inserts it where necessary
    private void CheckLeaderboard()
    {
        // Get Frames from level ------- Needs to be set to retrieve from level
        int frames = 0;

        for (int i = 0; i < I_ENTRIES; i++)
        {
            if (frames < lEntries[i].iFrames)
            {
                // Set Tag, Seconds and Shots -------- Needs to be set to retrieve from level
                LeaderboardEntry entry = new LeaderboardEntry("", frames, 0, 0);

                // Insert the new entry and remove the last one
                lEntries.Insert(i, entry);
                lEntries.RemoveAt(lEntries.Count - 1);

                // Save the new leaderboard - Need to insert the other stats from the Level
                SaveLeaderboard("", frames, 0, 0);
            }
        }
    }

    // Saves the leaderboard back into the level data
    private void SaveLeaderboard(string tag, int frames, float time, int shots)
    {
        // Level xml document
        XmlDocument levelDoc = new XmlDocument();
        // Load document from url
        levelDoc.Load(sLevelDataUrl);

        // Loop through each node under the root node to find the stats node
        foreach (XmlNode node in levelDoc.ChildNodes[0].ChildNodes)
        {
            if (node.Name == "Stats")
            {
                // Loop through each stat node
                foreach (XmlNode statNode in node.ChildNodes)
                {
                    // Check the name of each node 
                    switch (statNode.Name)
                    {
                        case "Tags":
                            // Loop through each LeaderboardEntry and set the value as an attribute
                            for (int i = 0; i < I_ENTRIES; i++)
                            {
                                XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[i]);
                                attribute.Value = lEntries[i].sTag;

                                statNode.Attributes.Append(attribute);
                            }
                        break;

                        case "Secs":
                            // Loop through each LeaderboardEntry and set the value as an attribute
                            for (int i = 0; i < I_ENTRIES; i++)
                            {
                                XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[i]);
                                attribute.Value = lEntries[i].fSecs.ToString();

                                statNode.Attributes.Append(attribute);
                            }
                        break;

                        case "Fras":
                            // Loop through each LeaderboardEntry and set the value as an attribute
                            for (int i = 0; i < I_ENTRIES; i++)
                            {
                                XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[i]);
                                attribute.Value = lEntries[i].iFrames.ToString();

                                statNode.Attributes.Append(attribute);
                            }
                        break;

                        case "Shts":
                            // Loop through each LeaderboardEntry and set the value as an attribute
                            for (int i = 0; i < I_ENTRIES; i++)
                            {
                                XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[i]);
                                attribute.Value = lEntries[i].iShots.ToString();

                                statNode.Attributes.Append(attribute);
                            }
                        break;
                    }
                }
            }
        }

        // Save the document
        levelDoc.Save(sLevelDataUrl);
    }
}
