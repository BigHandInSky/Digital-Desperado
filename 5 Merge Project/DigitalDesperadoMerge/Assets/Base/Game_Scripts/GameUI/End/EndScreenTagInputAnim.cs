﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenTagInputAnim : MonoBehaviour {

    [SerializeField] private Sprite m_Normal;
    [SerializeField] private Sprite m_Highlight;
    [SerializeField] private float m_fSwitchTime = 1f;
    private InputField InputComponent;
    public void Setup()
    {
        InputComponent = gameObject.GetComponent<InputField>();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        bool _On = true;
        while (true)
        {
            //if inputfield is not selected && text length != 3
            if (InputComponent.isFocused == false && InputComponent.text.Length != 3)
            {
                Debug.Log("not focused and less than 3 chars");
                if (InputComponent.text == "" || InputComponent.text.Contains(" "))
                {
                    Debug.Log("contains spaces or nothing");

                    _On = !_On;

                    if (_On)
                        gameObject.GetComponent<Image>().sprite = m_Normal;
                    else
                        gameObject.GetComponent<Image>().sprite = m_Highlight;
                }
            }
            else
            {
                Debug.Log("fine");

                gameObject.GetComponent<Image>().sprite = m_Normal;
            }

            yield return new WaitForSeconds(m_fSwitchTime);
        }
    }
}
