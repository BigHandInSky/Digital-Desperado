using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameReplayBtn : MonoBehaviour {

    [SerializeField] private List<GameObject> goObjsToActivate;
    [SerializeField] private List<GameObject> goObjsToDeactivate;

    [SerializeField] private PlayerMovementScript PlayerControlObj;

    public void Restart()
    {
        Debug.Log("restart called");

        GameData.Instance.Restart();
        //activate loading
        //call xxx to reload objects in world
        //reset player controls
        PlayerControlObj.AllowControls(false, true);
        //deactivate given objects
    }
}
