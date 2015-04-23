using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelsNameUI : MonoBehaviour {

    [SerializeField] private LoadedLevels LevelsDataObj;
    [SerializeField] private int iValueFromCurrLvlToDisplay;
    [SerializeField] private bool bGetUrl = false;
    [SerializeField] private bool bGetInt = false;
    
    private void vGetText()
    {
        if (bGetUrl)
            gameObject.GetComponent<Text>().text = LevelsDataObj.sGetFullUrl();
        else if (bGetInt)
            gameObject.GetComponent<Text>().text = LevelsDataObj.iCurrentLvl.ToString("00");
        else
            gameObject.GetComponent<Text>().text = LevelsDataObj.sGetCurrUrlName(iValueFromCurrLvlToDisplay);
    }
}
