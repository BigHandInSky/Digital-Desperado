using UnityEngine;
using System.Collections;

public class BasePlayerExitLevel : MonoBehaviour {

    public string sSceneToExitTo;
    public KeyCode kcExitKey = KeyCode.Escape;
	
	void Update () 
    {
	    if(Input.GetKeyDown(kcExitKey))
        {
            Application.LoadLevel(sSceneToExitTo);
        }
	}
}
