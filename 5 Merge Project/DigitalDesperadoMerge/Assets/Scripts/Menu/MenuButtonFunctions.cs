using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButtonFunctions : MonoBehaviour 
{
    //Unity's UI let's us use functions from an attached script to the UI button
    //hence all the functions for the generic buttons are contained in here
    
    [SerializeField]
    private string sCameraTag = "";

    public void vChangeLevel(string _levelName)
    {
        //print("vChangeLevel called");
        Application.LoadLevel(_levelName);
    }
    public void vExitApp()
    {
        //print("vExitApp called");
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
}
