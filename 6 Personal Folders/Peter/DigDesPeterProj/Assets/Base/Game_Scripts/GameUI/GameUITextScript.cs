using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUITextScript : MonoBehaviour {

    public enum GameUITextType
    {
        Frames,
        Secs,
        Bullets
    }

    [SerializeField]
    private GameUITextType m_TextType;
    [SerializeField]
    private Text m_UITextToSet;

    void Awake()
    {
        StartCoroutine("GetData");
    }

    IEnumerator GetData()
    {
        while(true)
        {
            switch (m_TextType)
            {
                case GameUITextType.Bullets:
                    m_UITextToSet.text = GameData.Instance.iBullsShot.ToString("000");
                    break;

                case GameUITextType.Frames:
                    m_UITextToSet.text = GameData.Instance.iTimeFr.ToString("000000");
                    break;

                case GameUITextType.Secs:
                    m_UITextToSet.text = GameData.Instance.fTimeScs.ToString("000.0");
                    break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
