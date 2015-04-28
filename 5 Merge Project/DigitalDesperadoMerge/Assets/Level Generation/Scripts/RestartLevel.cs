using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RestartLevel : MonoBehaviour 
{
    private Transform tPlayerStartPosition;

    private GameObject goPlayer;
    private GameObject[] agoTargetFrags;
    private GameObject[] agoTargetSpawners;

    [SerializeField] private GameRdyCountdown goCountdown;
    [SerializeField] private List<GameObject> goObjsToActivate = new List<GameObject>();
    [SerializeField] private List<GameObject> goObjsToDeactivate = new List<GameObject>();

    [SerializeField] private PlayerMovementScript PlayerControlObj;
    [SerializeField] private PlayerShootLaser PlayerShootObj;
    
    public void DoRestart()
    {
        Debug.Log("DoRestart");
        FindLevelObjects();

        GameData.Instance.Restart();
        PlayerControlObj.AllowControls(false, true);
        PlayerShootObj.bCanShoot = false;

        FindLevelObjects();
        RestartPlayer();
        RestartTargets();
        SetUI();

        goCountdown.gameObject.SetActive(true);
        goCountdown.StartCountdown();
    }

    private void FindLevelObjects()
    {
        if (tPlayerStartPosition == null)
            tPlayerStartPosition = GameObject.FindGameObjectWithTag("SpawnPosition").transform;

        if (goPlayer == null)
            goPlayer = GameObject.FindGameObjectWithTag("Player");

        if(agoTargetFrags == null)
            agoTargetFrags = GameObject.FindGameObjectsWithTag("Effect");

        if (agoTargetSpawners == null)
            agoTargetSpawners = GameObject.FindGameObjectsWithTag("TargSpawner");
    }
    private void RestartPlayer()
    {
        goPlayer.transform.position = tPlayerStartPosition.position;
        goPlayer.transform.rotation = tPlayerStartPosition.rotation;
    }
    private void RestartTargets()
    {
        if (agoTargetFrags.Length > 0)
            foreach (GameObject frag in agoTargetFrags)
            {
                DestroyObject(frag);
            }

        foreach (GameObject target in agoTargetSpawners)
        {
            Debug.Log("Resetting target: " + target);
            target.GetComponent<TargetRespawner>().vRespawn();
        }
    }
    private void SetUI()
    {
        foreach (GameObject ui in goObjsToActivate)
        {
            ui.SetActive(true);
        }
        foreach (GameObject ui in goObjsToDeactivate)
        {
            ui.SetActive(false);
        }
    }
}
