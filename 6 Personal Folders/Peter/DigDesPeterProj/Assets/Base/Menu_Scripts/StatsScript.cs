using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//given a set of strings/variables, will pass onto the right texts
public class StatsScript : MonoBehaviour 
{

    int iState = 0;

    public Image imStatsObj;
    public Text txStats1;
    public Text txStats2;
    public Text txStats3;
    public Text txStats4;
    public Text txStats5;
    public Text txStats6;

    public float fRotateStatsTime = 5f;

    void Start()
    {

    }

    //counts down, then refreshes image + texts to new stats to display
    /*IEnumerable ieRotateStats()
    {
        float fTimer = fRotateStatsTime;

        while (fTimer > 0)
        {
            fTimer--;
        }
    }*/

    //sets image sprite, re-gets data for stats depending on current level selected
    void vRefresh()
    {

    }
}
