using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour 
{
    public static Transform tPlayerStartPosition;

    GameObject goPlayer;
    GameObject[] agoTargets;

    [SerializeField]
    GameObject goEndLevelScreen;
    [SerializeField]
    GameObject goReadyButton;

    void Awake()
    {
        FindLevelObjects();
    }

    private void FindLevelObjects()
    {
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        agoTargets = GameObject.FindGameObjectsWithTag("Target");
    }

    public void RestartPlayerPosition()
    {
        goPlayer.transform.position = tPlayerStartPosition.position;
        goPlayer.transform.rotation = tPlayerStartPosition.rotation;

        if (agoTargets.Length > 0)
        {
            foreach (GameObject target in agoTargets)
            {
                target.SetActive(true);
            }
        }

        goEndLevelScreen.SetActive(false);
        goReadyButton.SetActive(true);
    }
}
