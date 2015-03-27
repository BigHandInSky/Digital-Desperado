using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Script which contains urls to all levels in the levels folder
public class LoadedLevels : MonoBehaviour 
{
    private List<string> lsUrls = new List<string>();
    public string sLevelsFolder;

    void Start ()
    {
        for (int i = 0; i < 10; i++ )
        {
            lsUrls.Add("Level " + i.ToString());
        }
    }

    //get name
    public string sGetUrlName(int _urlsArrPos)
    {
        return lsUrls[_urlsArrPos];
    }
    //get stats
    //get levelData
}
