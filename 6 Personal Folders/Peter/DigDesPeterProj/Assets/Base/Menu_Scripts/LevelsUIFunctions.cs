using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//functions for all levels related stuff
public class LevelsUIFunctions : MonoBehaviour {

    int iCurrentLevel;

    public Text txForwLevelText;
    public Text txBackLevelText;
    public Text txCurrLevelText;

    public StatsScript StatsObjScript;

    public GameObject MapObj;

    void ChangeSelectedLevel(bool _increment)
    {
        if (_increment)
        {
            iCurrentLevel++;
        }
        else
        {
            iCurrentLevel--;
        }
    }

    //gets necessary data from LoadedLevelsData and sets UI to it
    void UpdateUI()
    {
        //update texts
        //refresh stats
        //refresh map
    }
}
