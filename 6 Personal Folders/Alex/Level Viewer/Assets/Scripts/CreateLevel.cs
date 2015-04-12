using System.Collections.Generic;
using System.Globalization;
using System.Xml;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Manages the loading and creation of levels

public class CreateLevel : MonoBehaviour 
{
    // File Path (Minus Filename)
    string sFilePath = "";
    // File Name (Including .xml Extension)
    string sFileName = "";

    // Parent Transform for Level Objects
    public Transform tLevelRootObject;

    // Player Prefab
    public GameObject goPlayerPrefab;
    // Goal Prefab
    public GameObject goGoalPrefab;
    // Array of Platform Prefabs
    public GameObject[] agoPlatformPrefabs;
    // Array of Tower Prefabs
    public GameObject[] agoTowerPrefabs;
    // Array of Target Prefabs
    public GameObject[] agoTargetPrefabs;

    // Number of targets in a level
    int iTargets = 0;

    // Browse Level Text UI
    public InputField browseLevelText;
    // Target Text UI
    public Text targetsText;

	// Initialization
	void Start () 
    {
	}
	
	// Update
	void Update () 
    {
	}

    // Creates an Open File Dialog for xml files and selects that path
    public void SelectFilePath()
    {
        // File Path
        string filePath = "";
        // File Name
        string fileName = "";

        // Create File Dialog and retrieve path
        filePath = EditorUtility.OpenFilePanel("Load Level", "", "xml");

        // Split up file path by folder
        string[] folderNames = filePath.Split('/');
        // Find the file name
        fileName = folderNames[folderNames.Length - 1];
        // Remove the file name from the path
        filePath = filePath.Remove(filePath.Length - fileName.Length);

        // Set the Browse Level Text
        browseLevelText.text = fileName;

        // Set the file path and name
        sFilePath = filePath;
        sFileName = fileName;
    }

    // Loads a level from an xml file
    public void LoadLevel()
    {
        // If the file path or name is empty, then return
        if (sFilePath == "" || sFileName == "")
        {
            Debug.Log("No path selected");
            return;
        }

        // Xml reader for the file
        XmlReader reader = XmlReader.Create(sFilePath + sFileName);

        // Read the first node
        reader.Read();

        // If the first node is not LevelData then return
        if (reader.Name != "LevelData")
        {
            Debug.Log("File not a supported level");

            return;
        }

        // Reset number of targets
        iTargets = 0;
        // Destroy any current level objects
        DestroyCurrentLevel();

        // While there are nodes to read
        while (reader.Read())
        {
            // If the node is an open node
            if (reader.IsStartElement())
            {
                // Check node name, instantiate the prefab for each node and assign its transform
                switch (reader.Name)
                {
                    case "PlayerStart":
                        GameObject player = Instantiate(goPlayerPrefab);
                        AssignTransform(player, reader.ReadSubtree());
                    break;

                    case "Goal":
                        GameObject goal = Instantiate(goGoalPrefab);
                        AssignTransform(goal, reader.ReadSubtree());
                    break;

                    case "Platform":
                        int platformLevel = Mathf.Clamp(int.Parse(reader.GetAttribute("level")) - 1, 0, agoPlatformPrefabs.Length);
                        GameObject platform = Instantiate(agoPlatformPrefabs[platformLevel]);
                        AssignTransform(platform, reader.ReadSubtree());
                    break;

                    case "Tower":
                        int towerType = Mathf.Clamp(int.Parse(reader.GetAttribute("type")) - 1, 0, agoTowerPrefabs.Length);
                        GameObject tower = Instantiate(agoTowerPrefabs[towerType]);
                        AssignTransform(tower, reader.ReadSubtree());
                    break;

                    case "Target":
                        int targetType = Mathf.Clamp(int.Parse(reader.GetAttribute("type")) - 1, 0, agoPlatformPrefabs.Length);
                        GameObject target = Instantiate(agoTargetPrefabs[targetType]);
                        AssignTransform(target, reader.ReadSubtree());
                        
                        // Increment the number of targets
                        iTargets++;
                    break;
                }
            }
        }

        // Close the reader
        reader.Close();

        // Update the TargetText
        targetsText.text = "Targets: " + iTargets;

        Debug.Log("Level Load Complete - " + sFileName);
    }

    // Destroys any child level objects of the parent transform
    private void DestroyCurrentLevel()
    {
        // Number of Level Objects
        int noOfLevelObjects = tLevelRootObject.childCount;

        // If there are objects to destroy
        if (noOfLevelObjects > 0)
        {
            // Create an array of objects
            GameObject[] levelObjects = new GameObject[noOfLevelObjects];

            // Loop through each object and add it to the object array
            for (int i = 0; i < noOfLevelObjects; i++)
            {
                levelObjects[i] = tLevelRootObject.GetChild(i).gameObject;
            }

            // Loop through each object in the array and destroy it
            foreach (GameObject levelObject in levelObjects)
            {
                Destroy(levelObject);
            }
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
        while(reader.Read())
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
                        scale.x *= float.Parse(reader.GetAttribute("x"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        scale.y *= float.Parse(reader.GetAttribute("y"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                        scale.z *= float.Parse(reader.GetAttribute("z"), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
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
