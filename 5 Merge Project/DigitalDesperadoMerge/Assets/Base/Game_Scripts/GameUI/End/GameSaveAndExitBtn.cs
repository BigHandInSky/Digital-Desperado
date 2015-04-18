using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSaveAndExitBtn : MonoBehaviour {

    [SerializeField] private InputField TagInputField;
    [SerializeField] private Text TagErrorField;
    [SerializeField] private SaveLevelData SaveDataComponent;

    private string sSceneToLoad = "Main";

    private const string sNormMsg = " ";
    private const string sErrorMsg = "Invalid Tag";
    //save script

    public void SaveAndExit()
    {
        Debug.Log("SaveAndExit called");
        //save script . save with tag (TagInputField.text)

        if (bCheckTagEntry())
        {
            SaveDataComponent.SaveLeaderboard(TagInputField.text);
            Application.LoadLevel(sSceneToLoad);
        }
        else
            TagErrorField.text = sErrorMsg;
    }
    public void Exit()
    {
        Debug.Log("Exit called");
        Application.LoadLevel(sSceneToLoad);
    }

    private bool bCheckTagEntry()
    {
        if (
            TagInputField.text == null
            || TagInputField.text == string.Empty
            || TagInputField.text.Length != 3
            || TagInputField.text.Contains(" ")
            )
            return false;
        else
            return true;
    }
}
