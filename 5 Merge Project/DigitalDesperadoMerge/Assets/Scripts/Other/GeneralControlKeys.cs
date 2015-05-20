using UnityEngine;
using System.Collections;

public class GeneralControlKeys : MonoBehaviour
{
    private static GeneralControlKeys m_DataInstance;
    public static GeneralControlKeys Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<GeneralControlKeys>(); }
            return m_DataInstance;
        }
    }
    void Awake()
    {
        m_DataInstance = this;
    }

    public bool bCanRestartOrMenu = true;

	void Update () 
    {
	    if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKey(GameSettings.Instance.Menu) && !Application.loadedLevelName.Contains("Main")
            && bCanRestartOrMenu)
        {
            Application.LoadLevel("Main");
        }
        else if (Input.GetKey(GameSettings.Instance.Rset) && Application.loadedLevelName.Contains("Game")
            && bCanRestartOrMenu)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<RestartLevel>().DoRestart();
        }
	}
}
