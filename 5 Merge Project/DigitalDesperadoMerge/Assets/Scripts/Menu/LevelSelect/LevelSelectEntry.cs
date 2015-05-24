using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectEntry : MonoBehaviour {

    private int ArrayInteger;
    private GameObject ListManager;

    [SerializeField] private Text TextToSet;

    public void SetupEntry(GameObject _manager, string _levelName, int _arrayPos)
    {
        ArrayInteger = _arrayPos;
        ListManager = _manager;
        //00 - ....
        TextToSet.text = (_arrayPos + 1).ToString("00") + " - " + _levelName;
    }

    public void Select()
    {
        LoadedLevels.Instance.vSelectLevelByNum(ArrayInteger);
        LevelSelectUIControl.Instance.CloseFolderUI();
    }
    public void Delete()
    {
        DestroyObject(this.gameObject);
    }
}
