using System.Collections;
using System.IO;
using System.Xml;

using UnityEngine;

public class SaveLevel : MonoBehaviour 
{
    [SerializeField]
    GameObject goPlayer;
    [SerializeField]
    GameObject goGoal;
    [SerializeField]
    GameObject[] agoPlatforms;

    GameObject[] agoTowers;
    GameObject[] agoTargets;

    string[] asSlotNames = {"A", "B", "C", "D", "E", "F"};

    string[] asStatsTags = new string[6];
    float[] afStatsSecs = new float[6];
    int[] aiStatsFras = new int[6];
    int[] aiStatsShts = new int[6];

    public void SaveLevelData()
    {
        RetrieveStatsData();
        RetrieveCreatedAssets();

        print(CreateLevel.sFilePath + CreateLevel.sFileName);

        using (XmlWriter writer = XmlWriter.Create(CreateLevel.sFilePath + CreateLevel.sFileName))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("LevelData");

            writer.WriteStartElement("Stats");

            writer.WriteStartElement("Tags");

            for (int i = 0; i < asStatsTags.Length; i++)
            {
                writer.WriteAttributeString(asSlotNames[i], asStatsTags[i]);
            }

            writer.WriteEndElement();

            writer.WriteStartElement("Secs");

            for (int i = 0; i < asStatsTags.Length; i++)
            {
                writer.WriteAttributeString(asSlotNames[i], afStatsSecs[i].ToString("00.00"));
            }

            writer.WriteEndElement();

            writer.WriteStartElement("Fras");

            for (int i = 0; i < asStatsTags.Length; i++)
            {
                writer.WriteAttributeString(asSlotNames[i], aiStatsFras[i].ToString("00000"));
            }

            writer.WriteEndElement();

            writer.WriteStartElement("Shts");

            for (int i = 0; i < asStatsTags.Length; i++)
            {
                writer.WriteAttributeString(asSlotNames[i], aiStatsShts[i].ToString("000"));
            }

            writer.WriteEndElement();

            writer.WriteEndElement();

            writer.WriteStartElement("PlayerStart");

            CreateTransformElements(writer, goPlayer);

            writer.WriteEndElement();

            writer.WriteStartElement("Goal");

            CreateTransformElements(writer, goGoal);

            writer.WriteEndElement();

            writer.WriteStartElement("Platforms");

            for (int i = 0; i < agoPlatforms.Length; i++)
            {
                writer.WriteStartElement("Platform");

                writer.WriteAttributeString("level", (i + 1).ToString());
                CreateTransformElements(writer, agoPlatforms[i], true, true);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteStartElement("Towers");

            foreach (GameObject tower in agoTowers)
            {
                writer.WriteStartElement("Tower");
                CreateTransformElements(writer, tower, true, true);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteStartElement("Targets");

            foreach (GameObject target in agoTargets)
            {
                writer.WriteStartElement("Target");
                writer.WriteAttributeString("type", "1");
                CreateTransformElements(writer, target);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }

    private void RetrieveStatsData()
    {
        bool tagsFound = false;
        bool secsFound = false;
        bool frasFound = false;
        bool shtsFound = false;

        if (File.Exists(CreateLevel.sFilePath + CreateLevel.sFileName))
        {
            using (XmlReader reader = XmlReader.Create(CreateLevel.sFilePath + CreateLevel.sFileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Tags":
                                for (int i = 0; i < asSlotNames.Length; i++)
                                {
                                    asStatsTags[i] = reader.GetAttribute(asSlotNames[i]);
                                }

                                tagsFound = true;
                                break;

                            case "Secs":
                                for (int i = 0; i < asSlotNames.Length; i++)
                                {
                                    afStatsSecs[i] = float.Parse(reader.GetAttribute(asSlotNames[i]));
                                }

                                secsFound = true;
                                break;

                            case "Fras":
                                for (int i = 0; i < asSlotNames.Length; i++)
                                {
                                    aiStatsFras[i] = int.Parse(reader.GetAttribute(asSlotNames[i]));
                                }

                                frasFound = true;
                                break;

                            case "Shts":
                                for (int i = 0; i < asSlotNames.Length; i++)
                                {
                                    aiStatsShts[i] = int.Parse(reader.GetAttribute(asSlotNames[i]));
                                }

                                shtsFound = true;
                                break;
                        }
                    }
                }
            }
        }

        if (!tagsFound || !secsFound || !frasFound || !shtsFound)
        {
            for (int i = 0; i < asSlotNames.Length; i++)
            {
                asStatsTags[i] = "---";
                afStatsSecs[i] = 999.99f;
                aiStatsFras[i] = 99999;
                aiStatsShts[i] = 999;
            }
        }
    }

    private void RetrieveCreatedAssets()
    {
        agoTowers = GameObject.FindGameObjectsWithTag("Tower");
        agoTargets = GameObject.FindGameObjectsWithTag("Target");
    }

    private void CreateTransformElements(XmlWriter writer, GameObject obj, bool writeScale = false, bool writeFullRot = false)
    {
        writer.WriteStartElement("Position");

        writer.WriteAttributeString("x", obj.transform.position.x.ToString());
        writer.WriteAttributeString("y", obj.transform.position.y.ToString());
        writer.WriteAttributeString("z", obj.transform.position.z.ToString());

        writer.WriteEndElement();

        if (writeFullRot)
        {
            writer.WriteStartElement("Rotation");

            writer.WriteAttributeString("x", obj.transform.rotation.x.ToString());
            writer.WriteAttributeString("y", obj.transform.rotation.y.ToString());
            writer.WriteAttributeString("z", obj.transform.rotation.z.ToString());

            writer.WriteEndElement();
        }
        else
        {
            writer.WriteElementString("Rotation", obj.transform.rotation.y.ToString());
        }

        if (writeScale)
        {
            writer.WriteStartElement("Scale");

            writer.WriteAttributeString("x", obj.transform.localScale.x.ToString());
            writer.WriteAttributeString("y", obj.transform.localScale.y.ToString());
            writer.WriteAttributeString("z", obj.transform.localScale.z.ToString());

            writer.WriteEndElement();
        }
    }
}
