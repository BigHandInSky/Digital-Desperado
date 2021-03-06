﻿using UnityEngine;
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

    public bool bDebug = false;
    void Awake()
    {
        QualitySettings.SetQualityLevel(0, true);
        Screen.SetResolution(800, 600, false);

        m_KeySettings.Add(KeyCode.W);
        m_KeySettings.Add(KeyCode.S);
        m_KeySettings.Add(KeyCode.A);
        m_KeySettings.Add(KeyCode.D);

        m_KeySettings.Add(KeyCode.Space);
        m_KeySettings.Add(KeyCode.Mouse0);
        m_KeySettings.Add(KeyCode.R);
        m_KeySettings.Add(KeyCode.F);

        if (bDebug)
            SaveData();

        m_DataInstance = this;
        DontDestroyOnLoad(this.gameObject);
	    LoadData();
    }

    public bool bComeFromGame = false;
    void OnLevelWasLoaded(int level)
    {
        if (! GeneralControlKeys.Instance.bCanRestartOrMenu)
            GeneralControlKeys.Instance.bCanRestartOrMenu = true;

        if (Application.loadedLevelName.Contains("Tutorial") || Application.loadedLevelName.Contains("Sandbox"))
        {
            bComeFromGame = false;
            AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.Other);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            ApplyFOV();
        }
        else if (Application.loadedLevelName.Contains("Main"))
        {
            AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.Menus);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (bComeFromGame)
            {
                Camera.main.GetComponent<CameraTransitionScript>().GotoLevelSide();
                SaveData();
            }
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
        //Debug.Log("GameSettings SaveData persistent path: " + Application.persistentDataPath);

		PlayerData data = new PlayerData ();
        data.LoadLevelInt = m_LoadedLevelInt;
		data.Effects = Effects;
        data.MusVolume = fMusicVolume;
        data.EffVolume = fEffxsVolume;
		data.Music = Music;
        data.Sens = fSens;
		data.FOV = FOV;
		data.RHeight = RHeight;
		data.RWidth = RWidth;
        data.Keys = m_KeySettings;
        data.Fullscreen = bFullscreen;
        data.Quality = iQualityIndex;
        data.Tag = sPreviousTag;

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

            if (iQualityIndex >= 0
                && iQualityIndex < QualitySettings.names.Length)
                iQualityIndex = data.Quality;
            else
                iQualityIndex = 0;

            bFullscreen = data.Fullscreen;

            if (data.RWidth >= 800)
			    SetResolution(data.RWidth,data.RHeight);
            else
                SetResolution(800, 600);

            m_LoadedLevelInt = data.LoadLevelInt;
            fMusicVolume = data.MusVolume;
            fEffxsVolume = data.EffVolume;
            SetFOV(data.FOV);
            SetSens(data.Sens);
            m_KeySettings = data.Keys;

			Effects = data.Effects;
			Music = data.Music;

            if (data.Tag != null)
                sPreviousTag = data.Tag;
            else
                sPreviousTag = "---";

            ApplySettings();
		}
	}

    #region Options
    private bool bMusicOn = true;
    public bool Music { get { return bMusicOn; } set { bMusicOn = value; } }
    private bool bEffectsOn = true;
    public bool Effects { get { return bEffectsOn; } set { bEffectsOn = value; } }

    private float fMusicVolume = 1f;
    private float fEffxsVolume = 1f;
    public float MusVolume { get { return fMusicVolume; } }
    public float EffVolume { get { return fEffxsVolume; } }
    private float fFOV = 90f;
    public float FOV { get { return fFOV; } }
    private float fSens = 5f;
    public float Sens { get { return fSens; } }
    #endregion

    #region Graphics
    private bool bFullscreen = false;
    public bool Fullscreen { get { return bFullscreen; } set { bFullscreen = value; } }
    private int iResWidth = 1280;
    public int RWidth { get { return iResWidth; } }
    private int iResHeight = 720;
    public int RHeight { get { return iResHeight; } }
    private int iQualityIndex = 0;
    public int Quality { get { return iQualityIndex; } set { iQualityIndex = value; } }
    #endregion

    private string sPreviousTag = "---";
    public string PreviousTag { get { return sPreviousTag; } set { sPreviousTag = value; } }

    #region Keys
    private List<KeyCode> m_KeySettings = new List<KeyCode>();
    public List<KeyCode> CurrentKeySettings { get { return m_KeySettings; } }
    
    public KeyCode Forw { get { return m_KeySettings[0]; } set { m_KeySettings[0] = value; } }
    public KeyCode Back { get { return m_KeySettings[1]; } set { m_KeySettings[1] = value; } }
    public KeyCode Left { get { return m_KeySettings[2]; } set { m_KeySettings[2] = value; } }
    public KeyCode Righ { get { return m_KeySettings[3]; } set { m_KeySettings[3] = value; } }
    public KeyCode Jump { get { return m_KeySettings[4]; } set { m_KeySettings[4] = value; } }
    public KeyCode Fire { get { return m_KeySettings[5]; } set { m_KeySettings[5] = value; } }
    public KeyCode Rset { get { return m_KeySettings[6]; } set { m_KeySettings[6] = value; } }
    public KeyCode Menu { get { return m_KeySettings[7]; } set { m_KeySettings[7] = value; } }
    public KeyCode HARD_EXIT { get { return KeyCode.Escape; } }
    #endregion

    private List<string> m_LoadedUrls = new List<string>();
    public string LoadLevelUrl 
    { 
        get
        {
            if (m_LoadedLevelInt < m_LoadedUrls.Count && m_LoadedLevelInt > -1)
                return m_LoadedUrls[m_LoadedLevelInt];
            else
            {
                Debug.LogWarning("GameSettings was given an int greater than url array");
                return m_LoadedUrls[0];
            }
        } 
    }
    public string LoadLevelName
    {
        get
        {
            string _temp = m_LoadedUrls[m_LoadedLevelInt];
            _temp = _temp.Split('\\')[_temp.Split('\\').Length - 1];

            int _index = _temp.IndexOf(".");

            if (_index > 0)
                _temp = _temp.Remove(_index);
            return _temp;
        }
    }

    private int m_LoadedLevelInt = 0;
    public int LevelInt 
    { 
        get { return m_LoadedLevelInt; } 
        set 
        {
            if (value < m_LoadedUrls.Count
                && value > -1)
                m_LoadedLevelInt = value; 
            else
            {
                Debug.LogWarning("GameSettings was given an invalid int: " + value);
                m_LoadedLevelInt = 0;
            }
        } 
    }

    public void SetUrls(List<string> _Urls)
    {
        //if (_Urls.Count < 1)
            m_LoadedUrls = _Urls;
    }
    public void SetResolution(int _width, int _height)
    {
        iResWidth = _width;
        iResHeight = _height;
    }
    public void SetVolume(float _Val, bool _music)
    {
        if(_music)
            fMusicVolume = _Val * 0.01f;
        else
            fEffxsVolume = _Val * 0.01f;
    }
    public void SetFOV(float _Val) { fFOV = _Val; }
    public void SetSens(float _val) { fSens = _val; }

    public void ApplySettings()
    {
        QualitySettings.SetQualityLevel(iQualityIndex, true);
        Screen.SetResolution(iResWidth, iResHeight, bFullscreen);

        if (AudioManagerMusic.Instance)
            AudioManagerMusic.Instance.SetVolume();

        SaveData();
    }
    public void ApplyControls()
    {
        SaveData();
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
    public float MusVolume;
    public float EffVolume;
	public float FOV;
    public float Sens;
	public bool Music;
	public bool Effects;
	public int RWidth;
    public int RHeight;
    public int Quality;
    public bool Fullscreen;
    public List<KeyCode> Keys;
    public string Tag;
}



