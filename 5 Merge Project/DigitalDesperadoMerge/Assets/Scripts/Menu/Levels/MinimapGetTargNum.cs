using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinimapGetTargNum : MonoBehaviour {

    private const string m_Base = " Targets";

    public void SetNum(int _number)
    {
        GetComponent<Text>().text = _number.ToString() + m_Base;
    }
}
