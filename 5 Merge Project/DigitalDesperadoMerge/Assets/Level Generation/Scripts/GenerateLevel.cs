using System.Collections.Generic;
using System.Globalization;
using System.Xml;

using UnityEngine;

public class GenerateLevel : MonoBehaviour 
{
    string sFilePath = "";

    [SerializeField]
    private GameLoadingScript LoadingObj;

    [SerializeField]
    Transform tLevelRootObject;
    [SerializeField]
    GameObject goPlayer;
    [SerializeField]
    GameObject goGoal;

    [SerializeField]
    GameObject goPlayerStartPrefab;
    [SerializeField]
    GameObject[] agoPlatformPrefabs;
    [SerializeField]
    GameObject goTowerPrefab;
    [SerializeField]
    GameObject[] agoTargetPrefabs;

	// Initialization
	void Start () 
    {
        LoadLevel();
	}

    private void LoadLevel()
    {
        //sFilePath = Application.dataPath + "/XML/" + GameSettings.Instance.LoadLevelUrl;
        sFilePath = GameSettings.Instance.LoadLevelUrl;

        using (XmlReader reader = XmlReader.Create(sFilePath))
        {
            reader.Read();

            if (reader.Name != "LevelData")
            {
                // Dialog for not supported level
            }

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "PlayerStart":
                            GameObject playerStart = Instantiate(goPlayerStartPrefab);
                            XmlReader transformSubTree = reader.ReadSubtree();

                            AssignTransform(goPlayer, transformSubTree);
                            AssignTransform(playerStart, transformSubTree);
                        break;

                        case "Goal":
                            AssignTransform(goGoal, reader.ReadSubtree());
                        break;

                        case "Platform":
                            int platformLevel = Mathf.Clamp(int.Parse(reader.GetAttribute("level")) - 1, 0, agoPlatformPrefabs.Length - 1);
                            GameObject platform = Instantiate(agoPlatformPrefabs[platformLevel]);
                            AssignTransform(platform, reader.ReadSubtree());
                        break;

                        case "Tower":
                            GameObject tower = Instantiate(goTowerPrefab);
                            AssignTransform(tower, reader.ReadSubtree());
                        break;

                        case "Target":
                            //int targetType = Mathf.Clamp(int.Parse(reader.GetAttribute("type")) - 1, 0, agoTargetPrefabs.Length - 1);
                            int targetType = Random.Range(0, agoTargetPrefabs.Length);
                            GameObject target = Instantiate(agoTargetPrefabs[targetType]);
                            AssignTransform(target, reader.ReadSubtree());
                        break;
                    }
                }
            }

            LoadingObj.SwitchToRdy();
        }
    }

    // Assigns transform data from a reader subtree to the specified object
    private void AssignTransform(GameObject obj, XmlReader reader)
    {
        // Position
        Vector3 position = Vector3.zero;
        // Rotation
        float rotation = 0;
        // Scale
        Vector3 scale = obj.transform.localScale;

        // While there are nodes to read
        while (reader.Read())
        {
            // If the node is an open node
            if (reader.IsStartElement())
            {
                // Check each node and retrieve the data
                switch (reader.Name)
                {
                    case "Position":
                        position = new Vector3(float.Parse(reader.GetAttribute("x"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign),
                            float.Parse(reader.GetAttribute("y"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign),
                            float.Parse(reader.GetAttribute("z"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign));
                        break;

                    case "Rotation":
                        reader.Read();
                        rotation = float.Parse(reader.Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        break;

                    case "Scale":
                        scale.x = float.Parse(reader.GetAttribute("x"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        scale.y = float.Parse(reader.GetAttribute("y"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        scale.z = float.Parse(reader.GetAttribute("z"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        break;
                }
            }
        }

        // Set the Position, Rotation and Scale
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
        obj.transform.localScale = scale;

        // Set the transform parent to the Level Root Transform
        obj.transform.parent = tLevelRootObject;
    }
}
