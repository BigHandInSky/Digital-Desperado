using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelsNameUI : MonoBehaviour {

    [SerializeField] private LoadedLevels LevelsDataObj;
    [SerializeField] private int iValueFromCurrLvlToDisplay;
    
    private void vGetText()
    {
        gameObject.GetComponent<Text>().text = LevelsDataObj.sGetCurrUrlName(iValueFromCurrLvlToDisplay);
    }
}
