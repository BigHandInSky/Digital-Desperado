using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenBtn : MonoBehaviour 
{
    public enum BtnType
    {
        Replay,
        Menu,
        Next
    }
    [SerializeField] private BtnType ButtonType;

    [SerializeField] private Text ErrorText;
    [SerializeField] private InputField TagInput;
    [SerializeField] private SaveLevelData SaveComponent;

    public void Setup()
    {

    }

    public void ClickBtn()
    {
        if (ButtonType == BtnType.Replay)
        {
            ErrorText.text = "";
            GameObject.FindGameObjectWithTag("GameController").GetComponent<RestartLevel>().DoRestart();
        }
        else if (ButtonType == BtnType.Menu)
        {
            if (!bCheckTagEntry())
            {
                ErrorText.text = "Invalid Tag";
                return;
            }

            SaveComponent.SaveData(TagInput.text);
            Application.LoadLevel("Main");
        }
        else if (ButtonType == BtnType.Next)
        {
            if (!bCheckTagEntry())
            {
                ErrorText.text = "Invalid Tag";
                return;
            }

            SaveComponent.SaveData(TagInput.text);
            GameSettings.Instance.SetLevelUrl(GameSettings.Instance.LevelInt + 1);
            Application.LoadLevel("GamePlaHol");
        }
    }

    private bool bCheckTagEntry()
    {
        if (
            TagInput.text == null
            || TagInput.text == string.Empty
            || TagInput.text.Length != 3
            || TagInput.text.Contains(" ")
            )
            return false;
        else
            return true;
    }
}
