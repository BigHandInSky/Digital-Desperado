using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenPrevious : MonoBehaviour {

    [SerializeField] private Text SecText;
    [SerializeField] private Text FraText;
    [SerializeField] private Text BullText;

    public void Setup()
    {
        SecText.text = GameData.Instance.fTimeScs.ToString("00.00");
        FraText.text = GameData.Instance.iTimeFr.ToString("00000");
        BullText.text = GameData.Instance.iBullsShot.ToString("000");
    }
}
