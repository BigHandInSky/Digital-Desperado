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
    public static string sFilePath = "";
    // File Name (Including .xml Extension)
    public static string sFileName = "";

    [SerializeField]
    GameObject goPlayer;
    [SerializeField]
    GameObject goGoal;
    [SerializeField]
    GameObject[] agoPlatforms;

    // Parent Transform for Level Objects
    [SerializeField]
    Transform tLevelRootObject;
    // Array of Tower Prefabs
    [SerializeField]
    GameObject goTowerPrefab;
    // Array of Target Prefabs
    [SerializeField]
    GameObject[] agoTargetPrefabs;

    [SerializeField]
    Transform tDefaultPlayerTransform;
    [SerializeField]
    Transform tDefaultGoalTransform;
    [SerializeField]
    Transform[] atDefaultPlatformTransforms;

    [SerializeField]
    GameObject goProjectManagementPanel;
    [SerializeField]
    GameObject goToolbarPanel;

    [SerializeField]
    Text levelTitleText;

    [SerializeField]
    SelectObject objectSelecter;

    // Number of targets in a level
    int iTargets = 0;

    public int Targets
    {
        get { return iTargets; }
    }

    // Creates an Open File Dialog for xml files and selects that path
    private void SelectFilePath()
    {
        // File Path
        string filePath = "";
        // File Name
        string fileName = "";

        // Create File Dialog and retrieve path
        filePath = EditorUtility.OpenFilePanel("Load Level", "", "xml");

        if (filePath != "" || filePath != string.Empty)
        {
            print("Select File");

            // Split up file path by folder
            string[] folderNames = filePath.Split('/');
            // Find the file name
            fileName = folderNames[folderNames.Length - 1];
            // Remove the file name from the path
            filePath = filePath.Remove(filePath.Length - fileName.Length);

            // Set the file path and name
            sFilePath = filePath;
            sFileName = fileName;
        }
    }

    public void CreateNewLevel()
    {
        string projectPath = "";
        string fileName = "";

        projectPath = EditorUtility.SaveFilePanel("Create New Project", "", "New Untitled Level.xml", ".xml");

        if (projectPath != "" ||
            projectPath != string.Empty)
        {
            // Split up file path by folder
            string[] folderNames = projectPath.Split('/');
            // Find the file name
            fileName = folderNames[folderNames.Length - 1];
            // Remove the file name from the path
            projectPath = projectPath.Remove(projectPath.Length - fileName.Length);

            // Set the file path and name
            sFilePath = projectPath;
            sFileName = fileName;

            DestroyCurrentLevel();
            ResetPreMadeObjectTransforms();
            ActivatePreMadeObjects();

            goProjectManagementPanel.SetActive(false);
            goToolbarPanel.SetActive(true);
            levelTitleText.text = sFileName.Remove(sFileName.Length - 4);
        }
    }

    // Loads a level from an xml file
    public void LoadLevel()
    {
        SelectFilePath();

        // If the file path or name is empty, then return
        if (sFilePath == "" || sFileName == "")
        {
            Debug.Log("No path selected");
            return;
        }

        // Xml reader for the file
        using (XmlReader reader = XmlReader.Create(sFilePath + sFileName))
        {
            // Read the first node
            reader.Read();

            // If the first node is not LevelData then return
            if (reader.Name != "LevelData")
            {
                reader.Read();

                if (reader.Name != "LevelData")
                {
                    Debug.Log("File not a supported level");

                    return;
                }
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
                            AssignTransform(goPlayer, reader.ReadSubtree());
                            break;

                        case "Goal":
                            AssignTransform(goGoal, reader.ReadSubtree());
                            break;

                        case "Platform":
                            int platformLevel = Mathf.Clamp(int.Parse(reader.GetAttribute("level")) - 1, 0, agoPlatforms.Length);
                            AssignTransform(agoPlatforms[platformLevel], reader.ReadSubtree());
                            break;

                        case "Tower":
                            GameObject tower = Instantiate(goTowerPrefab);
                            AssignTransform(tower, reader.ReadSubtree());
                            tower.transform.parent = tLevelRootObject;
                            break;

                        case "Target":
                            int targetType = Mathf.Clamp(int.Parse(reader.GetAttribute("type")) - 1, 0, agoTargetPrefabs.Length);
                            GameObject target = Instantiate(agoTargetPrefabs[targetType]);
                            AssignTransform(target, reader.ReadSubtree());
                            target.transform.parent = tLevelRootObject;

                            // Increment the number of targets
                            iTargets++;
                            break;
                    }
                }
            }
        }

        ActivatePreMadeObjects();

        goProjectManagementPanel.SetActive(false);
        goToolbarPanel.SetActive(true);
        levelTitleText.text = sFileName.Remove(sFileName.Length - 4);

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
    }

    private void ResetPreMadeObjectTransforms()
    {
        goPlayer.transform.position = tDefaultPlayerTransform.position;
        goPlayer.transform.rotation = tDefaultPlayerTransform.rotation;

        goGoal.transform.position = tDefaultGoalTransform.position;
        goGoal.transform.rotation = tDefaultGoalTransform.rotation;

        for (int i = 0; i < agoPlatforms.Length; i++)
        {
            agoPlatforms[i].transform.position = atDefaultPlatformTransforms[i].position;
            agoPlatforms[i].transform.rotation = atDefaultPlatformTransforms[i].rotation;
        }
    }

    private void ActivatePreMadeObjects()
    {
        goPlayer.SetActive(true);
        goGoal.SetActive(true);

        foreach (GameObject platform in agoPlatforms)
        {
            platform.SetActive(true);
        }
    }

    public void CreateTarget()
    {
        GameObject target = (GameObject)Instantiate(agoTargetPrefabs[0], Vector3.zero, Quaternion.identity);
        target.transform.parent = tLevelRootObject.transform;

        objectSelecter.SelectNewObject(target);
    }

    public void CreateTower()
    {
        GameObject tower = (GameObject)Instantiate(goTowerPrefab, Vector3.zero, Quaternion.identity);
        tower.transform.parent = tLevelRootObject.transform;

        objectSelecter.SelectNewObject(tower);
    }
}
