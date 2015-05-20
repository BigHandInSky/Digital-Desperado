using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EndScreenStart : MonoBehaviour {

    [SerializeField] private List<GameObject> ObjsToActivate = new List<GameObject>();
    [SerializeField] private List<GameObject> ObjsToDeActivate = new List<GameObject>();

    [SerializeField] private Text TitleText;

	public void SetupEndScreen()
    {
        foreach(GameObject _obj in ObjsToActivate)
        {
            _obj.SetActive(true);
            _obj.SendMessage("Setup");
        }

        TitleText.text = GameSettings.Instance.LoadLevelName;

        foreach (GameObject _obj in ObjsToDeActivate)
            _obj.SetActive(false);
    }
}
