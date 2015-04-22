using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenAchievement : MonoBehaviour {

    [SerializeField] private Sprite NoGetAch;
    [SerializeField] private Sprite YuGetAch;

    public enum AchType
    {
        ParTime,
        AllTargs,
        NoMisses,
        NoFalls
    }
    [SerializeField]
    private AchType AchievType;

    public void Setup()
    {
        bool _temp = false;
        if(AchievType != AchType.ParTime)
        {
            switch(AchievType)
            {
                case AchType.AllTargs:
                    _temp = (GameData.Instance.iTargsLft == 0);
                    break;
                case AchType.NoMisses:
                    _temp = ((GameData.Instance.iTargsTtl - GameData.Instance.iTargsLft) == GameData.Instance.iBullsShot);
                    break;
                case AchType.NoFalls:
                    _temp = (GameData.Instance.iFalls == 0);
                    break;
            }

            SetCheck(_temp);
        }
    }

    public void SetCheck(bool _true)
    {
        if (_true)
            gameObject.GetComponent<Image>().sprite = YuGetAch;
        else
            gameObject.GetComponent<Image>().sprite = NoGetAch;
    }
}
