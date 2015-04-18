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

	// Initialization
	void Start () 
    {
	}

    // Saves the leaderboard back into the level data
    public void SaveLeaderboard(string newTag)
    {
        sLevelDataUrl = Application.dataPath + "/XML/" + GameSettings.Instance.m_LoadedLevelUrl;

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

                                if (i < 5)
                                {
                                    attribute.Value = "XX" + Random.Range(0, 9).ToString();
                                }
                                else
                                {
                                    attribute.Value = newTag;
                                }

                                statNode.Attributes.Append(attribute);
                            }
                        break;

                        case "Secs":
                            // Loop through each LeaderboardEntry and set the value as an attribute
                            for (int i = 0; i < I_ENTRIES; i++)
                            {
                                XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[i]);

                                if (i < 5)
                                {
                                    attribute.Value = Random.Range(0.0f, 100.0f).ToString("00.00");
                                }
                                else
                                {
                                    attribute.Value = GameData.Instance.fTimeScs.ToString("00.00");
                                }

                                statNode.Attributes.Append(attribute);
                            }
                        break;

                        case "Fras":
                            // Loop through each LeaderboardEntry and set the value as an attribute
                            for (int i = 0; i < I_ENTRIES; i++)
                            {
                                XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[i]);

                                if (i < 5)
                                {
                                    attribute.Value = Random.Range(100, 900).ToString("00000");
                                }
                                else
                                {
                                    attribute.Value = GameData.Instance.iTimeFr.ToString("00000");
                                }

                                statNode.Attributes.Append(attribute);
                            }
                        break;

                        case "Shts":
                            // Loop through each LeaderboardEntry and set the value as an attribute
                            for (int i = 0; i < I_ENTRIES; i++)
                            {
                                XmlAttribute attribute = levelDoc.CreateAttribute(AS_ATTRIBUTE_NAMES[i]);

                                if (i < 5)
                                {
                                    attribute.Value = Random.Range(0, 99).ToString("000");
                                }
                                else
                                {
                                    attribute.Value = GameData.Instance.iBullsShot.ToString("000");
                                }

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
