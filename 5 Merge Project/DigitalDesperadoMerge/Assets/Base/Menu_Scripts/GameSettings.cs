using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
        m_KeySettings.Add(KeyCode.W);
        m_KeySettings.Add(KeyCode.A);
        m_KeySettings.Add(KeyCode.S);
        m_KeySettings.Add(KeyCode.D);

        m_KeySettings.Add(KeyCode.Space);
        m_KeySettings.Add(KeyCode.Mouse0);
        m_KeySettings.Add(KeyCode.R);
        m_KeySettings.Add(KeyCode.Y);

        m_DataInstance = this;
        DontDestroyOnLoad(this.gameObject);
	    LoadData ();
    }

    public bool bComeFromGame = false;
    void OnLevelWasLoaded(int level)
    {
        //if another gameSettings detected, delete other
        if (Application.loadedLevelName.Contains("Tutorial"))
        {
            bComeFromGame = false;
            AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.Loading);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            ApplyFOV();
        }
        else if (Application.loadedLevelName.Contains("Main"))
        {
            SaveData();

            AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.Menus);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (bComeFromGame)
                Camera.main.GetComponent<CameraTransitionScript>().GotoLevelSide();
            else
                Camera.main.GetComponent<CameraTransitionScript>().FirstLoad();

            bComeFromGame = false;
        }
        else if (Application.loadedLevelName.Contains("Game"))
        {
            ApplyFOV();
            bComeFromGame = true;
        }
    }

	public void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/DesperadoPlayerData.dat");
        Debug.Log("GameSettings SaveData persistent path: " + Application.persistentDataPath);

		PlayerData data = new PlayerData ();
        data.LoadLevelInt = m_LoadedLevelInt;
		data.Effects = Effects;
		data.Volume = Volume;
		data.Music = Music;
		data.FOV = FOV;
		data.RHeight = RHeight;
		data.RWidth = RWidth;
        data.Keys = m_KeySettings;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void LoadData()
	{
		if(File.Exists(Application.persistentDataPath + "/DesperadoPlayerData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/DesperadoPlayerData.dat", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close ();

			SetResolution(data.RWidth,data.RHeight);
            SetLevelUrl(data.LoadLevelInt);
			SetVolume(data.Volume);
            SetFOV(data.FOV);
            m_KeySettings = data.Keys;

			Effects = data.Effects;
			Music = data.Music;
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

    private List<KeyCode> m_KeySettings = new List<KeyCode>();
    #region KeyReturns
    public KeyCode Forw { get { return m_KeySettings[0]; } }
    public KeyCode Back { get { return m_KeySettings[1]; } }
    public KeyCode Left { get { return m_KeySettings[2]; } }
    public KeyCode Righ { get { return m_KeySettings[3]; } }
    public KeyCode Jump { get { return m_KeySettings[4]; } }
    public KeyCode Fire { get { return m_KeySettings[5]; } }
    public KeyCode Rset { get { return m_KeySettings[6]; } }
    public KeyCode Menu { get { return m_KeySettings[7]; } }
    public KeyCode HARD_EXIT { get { return KeyCode.Escape; } }
    #endregion

    private List<string> m_LoadedUrls = new List<string>();
    public string LoadLevelUrl { get { return m_LoadedUrls[m_LoadedLevelInt]; } }
    public string LoadLevelName
    {
        get
        {
            string _temp = m_LoadedUrls[m_LoadedLevelInt];
            _temp = _temp.Split('\\')[_temp.Split('\\').Length - 1];
            int _index = _temp.IndexOf(".");
            _temp.Remove(_index, 4);
            return _temp;
        }
    }

    private int m_LoadedLevelInt = 0;
    public int LevelInt { get { return m_LoadedLevelInt; } set { m_LoadedLevelInt = value; } }

    public void SetLevelUrl(int _index)
    {
        m_LoadedLevelInt = _index;
    }
    public void SetUrls(List<string> _Urls)
    {
        m_LoadedUrls = _Urls;
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
    public void SetControls(KeyCode _w, KeyCode _s, KeyCode _a, KeyCode _d, KeyCode _jump, KeyCode _fire, KeyCode _reset, KeyCode _menu)
    {
        m_KeySettings[0] = _w;
        m_KeySettings[1] = _s;
        m_KeySettings[2] = _a;
        m_KeySettings[3] = _d;

        m_KeySettings[4] = _jump;
        m_KeySettings[5] = _fire;
        m_KeySettings[6] = _reset;
        m_KeySettings[7] = _menu;
    }

    public void ApplySettings()
    {
        SaveData();
        Screen.SetResolution(iResWidth, iResHeight, false);
        AudioListener.volume = (fVolume * 0.01f);
    }
    public void ApplyFOV()
    {
        Camera.main.fieldOfView = fFOV;
    }
}

[Serializable]
class PlayerData
{
	public int LoadLevelInt;
	public float Volume;
	public float FOV;
	public bool Music;
	public bool Effects;
	public int RWidth;
	public int RHeight;
    public List<KeyCode> Keys;
}



