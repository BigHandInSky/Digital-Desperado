using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour 
{
    public static Transform tPlayerStartPosition;

    GameObject goPlayer;
    GameObject[] agoTargets;
    GameObject[] agoTargetFrags;

    [SerializeField] private GameObject goEndLevelScreen;
    [SerializeField] private GameObject goReadyButton;

    void Awake()
    {
        FindLevelObjects();
    }

    private void FindLevelObjects()
    {
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        agoTargets = GameObject.FindGameObjectsWithTag("Target");
        agoTargetFrags = GameObject.FindGameObjectsWithTag("Effect");
    }

    public void RestartPlayerPosition()
    {
        goPlayer.transform.position = tPlayerStartPosition.position;
        goPlayer.transform.rotation = tPlayerStartPosition.rotation;

        if (agoTargets.Length > 0)
            foreach (GameObject target in agoTargets)
            {
                target.SetActive(true);
            }

        if (agoTargetFrags.Length > 0)
            foreach (GameObject frag in agoTargetFrags)
            {
                DestroyObject(frag);
            }

        goEndLevelScreen.SetActive(false);
        goReadyButton.SetActive(true);
    }
}
