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
    public bool bCanExit = true;

	void Update () 
    {
        if (bCanExit && Input.GetKeyDown(GameSettings.Instance.HARD_EXIT)
            && !Application.loadedLevelName.Contains("First"))
        {
            PlayerShootLaser.bCanShoot = false;
            PlayerMovementScript.SetControls = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Application.loadedLevelName.Contains("Game"))
                GameData.Instance.PauseTime = true;

            GameObject.FindGameObjectWithTag("Prompt").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Prompt").transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(GameSettings.Instance.Menu) && !Application.loadedLevelName.Contains("Main")
            && bCanRestartOrMenu)
        {
            Application.LoadLevel("Main");
        }
        else if (Input.GetKeyDown(GameSettings.Instance.Rset) && Application.loadedLevelName.Contains("Game")
            && bCanRestartOrMenu)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<RestartLevel>().DoRestart();
        }
	}
}
