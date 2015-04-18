using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameReplayBtn : MonoBehaviour {

    [SerializeField] private List<GameObject> goObjsToActivate;
    [SerializeField] private List<GameObject> goObjsToDeactivate;

    public void Restart()
    {
        Debug.Log("restart called");

        GameData.Instance.Restart();
        //activate loading
        //call xxx to reload objects in world
        GameData.Instance.GetObjects();
        //deactivate given objects
    }
}
