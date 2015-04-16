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
        DontDestroyOnLoad(this.gameObject);
    }

    private bool bMusicOn = true;
    public bool Music { get { return bMusicOn; } set { bMusicOn = value; } }
    private bool bEffectsOn = true;
    public bool Effects { get { return bEffectsOn; } set { bEffectsOn = value; } }

    private float fVolume = 10;
    public float Volume { get { return fVolume; } }
    private float fFOV = 10f;
    public float FOV { get { return fFOV; } }

    private int iResWidth = 800;
    public int RWidth { get { return iResWidth; } }
    private int iResHeight = 600;
    public int RHeight { get { return iResHeight; } }


    public void SetResolution(int _width, int _height)
    {
        iResWidth = _width;
        iResHeight = _height;
        Screen.SetResolution(_width, _height, true);
    }
    public void SetVolume(float _Val)
    {
        fVolume = _Val;
        AudioListener.volume = _Val;
    }
    public void SetFOV(float _Val)
    {
        fFOV = _Val;
        //Camera.main.fieldOfView = _Val;
    }
}
