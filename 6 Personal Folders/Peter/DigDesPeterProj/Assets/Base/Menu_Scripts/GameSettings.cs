using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

    private static GameSettings m_DataInstance;
    public static GameSettings Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<GameSettings>(); }
            return m_DataInstance;
        }
    }

    void Awake()
    {
        m_DataInstance = this;
    }
}
