using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButtonFunctions : MonoBehaviour 
{    
    private const string sCameraTag = "MainCamera";

    public void vChangeLevel(string _levelName)
    {
        Application.LoadLevel(_levelName);
    }
    public void vExitApp()
    {
        Application.Quit();
    }
    public void vSetCamMenuNum(int _menutogoto)
    {
        GameObject.FindGameObjectWithTag(sCameraTag).GetComponent<CameraTransitionScript>().iTransToMenuNum = _menutogoto;
    }
    public void vCamTransition(GameObject goPointToGoto)
    {
        GameObject.FindGameObjectWithTag(sCameraTag).GetComponent<CameraTransitionScript>()
            .vTransition(goPointToGoto.transform.position, goPointToGoto.transform.rotation);
    }
    public void CloseExitPrompt()
    {
        PlayerShootLaser.bCanShoot = true;
        PlayerMovementScript.SetControls = true;

        if (Application.loadedLevelName.Contains("Game"))
            GameData.Instance.PauseTime = false;

        if (!Application.loadedLevelName.Contains("Main"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
