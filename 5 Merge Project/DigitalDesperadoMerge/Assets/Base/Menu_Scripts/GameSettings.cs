using UnityEngine;
using UnityEngine.UI;
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

    void OnLevelWasLoaded(int level)
    {
        //if another gameSettings detected, delete other
        if (Application.loadedLevelName.Contains("Tutorial"))
        {
            AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.Loading);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            ApplyFOV();
        }
        else if (Application.loadedLevelName.Contains("Main"))
        {
            AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.Menus);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Application.loadedLevelName.Contains("Game"))
        {
            ApplyFOV();
        }
    }

    private bool bMusicOn = true;
    public bool Music { get { return bMusicOn; } set { bMusicOn = value; } }
    private bool bEffectsOn = true;
    public bool Effects { get { return bEffectsOn; } set { bEffectsOn = value; } }

    private float fVolume = 10;
    public float Volume { get { return fVolume; } }
    private float fFOV = 90f;
    public float FOV { get { return fFOV; } }

    private int iResWidth = 800;
    public int RWidth { get { return iResWidth; } }
    private int iResHeight = 600;
    public int RHeight { get { return iResHeight; } }


    private string m_LoadedLevelUrl;
    public string LoadLevelUrl { get { return m_LoadedLevelUrl; } }

    public void SetLevelUrl(string _url)
    {
        m_LoadedLevelUrl = _url;
    }

    public void SetResolution(int _width, int _height)
    {
        iResWidth = _width;
        iResHeight = _height;
    }
    public void SetVolume(float _Val)
    {
        fVolume = _Val;
    }
    public void SetFOV(float _Val)
    {
        fFOV = _Val;
    }
    public void ApplySettings()
    {
        Screen.SetResolution(iResWidth, iResHeight, true);
        AudioListener.volume = (fVolume * 0.01f);
    }
    public void ApplyFOV()
    {
        Camera.main.fieldOfView = fFOV;
    }
}
