using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenTagInputAnim : MonoBehaviour {

    [SerializeField] private Sprite m_Normal;
    [SerializeField] private Sprite m_Highlight;
    private float m_fSwitchTime = 0.4f;
    private InputField InputComponent;
    public void Setup()
    {
        InputComponent = gameObject.GetComponent<InputField>();
        StartCoroutine(Animate());
        InputComponent.text = GameSettings.Instance.PreviousTag;
    }

    IEnumerator Animate()
    {
        bool _On = true;
        while (true)
        {
            //if inputfield is not selected && text length != 3
            if (InputComponent.isFocused == false)
            {
                if (InputComponent.text.Length != 3
                    || !char.IsLetterOrDigit(InputComponent.text, 0)
                    || !char.IsLetterOrDigit(InputComponent.text, 1)
                    || !char.IsLetterOrDigit(InputComponent.text, 2))
                {
                    _On = !_On;

                    if (_On)
                        gameObject.GetComponent<Image>().sprite = m_Normal;
                    else
                        gameObject.GetComponent<Image>().sprite = m_Highlight;
                }
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = m_Normal;
            }

            yield return new WaitForSeconds(m_fSwitchTime);
        }
    }
}
