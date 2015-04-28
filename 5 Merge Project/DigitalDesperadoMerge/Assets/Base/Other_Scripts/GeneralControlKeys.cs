using UnityEngine;
using System.Collections;

public class GeneralControlKeys : MonoBehaviour 
{

	void Update () 
    {
	    if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKey(GameSettings.Instance.Menu) && !Application.loadedLevelName.Contains("Main"))
        {
            Application.LoadLevel("Main");
        }
        else if (Input.GetKey(GameSettings.Instance.Rset) && Application.loadedLevelName.Contains("Game"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<RestartLevel>().DoRestart();
        }
	}
}
