﻿using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {
    
	private static GameData m_DataInstance;
	public static GameData Instance
		{	
        get {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<GameData>(); }
            return m_DataInstance;
			}
		}

    public bool PauseTime = false;

    private float m_fTimePenaltyPerTarget = 2f;

    [SerializeField] private Transform m_Camera;
    public Transform trCamera { get { return m_Camera; } }
    [SerializeField] private Transform m_EndLvlTow;
    public Transform trEndLvlTow { get { return m_EndLvlTow; } }

    private GameObject[] agoTargets;
    public GameObject[] Targets { get { return agoTargets; } }
    [SerializeField] private GameUITargetScript m_UITargetsLeft;
    [SerializeField] private GameUITargetScript m_UITargetsShot;

    private int m_TargetsLeft = 0;
    public int iTargsLft { get { return m_TargetsLeft; } }
    private int m_TargetsTotl = 0;
    public int iTargsTtl { get { return m_TargetsTotl; } }
    private float m_fTargetShaderIncrement = 0f;
    public float fCurrShaderIncrement { get { return m_fTargetShaderIncrement * (m_TargetsTotl - m_TargetsLeft); } }

    private int m_BullsShot = 0;
    public int iBullsShot { get { return m_BullsShot; } }

    private int m_TimeFrames = 0;
    public int iTimeFr { get { return m_TimeFrames; } }
    private float m_TimeSecs = 0;
    public float fTimeScs { get { return m_TimeSecs; } }
    public float fTimeScsAndPenalty { get { return m_TimeSecs + (m_TargetsLeft * m_fTimePenaltyPerTarget); } }
    public float fTimePenalty { get { return (m_TargetsLeft * m_fTimePenaltyPerTarget); } }

    private int m_TimesFell = 0;
    public int iFalls { get { return m_TimesFell; } }

    void Awake() 
    {
        m_DataInstance = this;
        
        if(Application.loadedLevelName.Contains("Tutorial"))
            StartData();
    }

    public void StartData()
    {
        if (agoTargets == null)
            agoTargets = GameObject.FindGameObjectsWithTag("Target");

        m_TargetsTotl = agoTargets.Length;
        m_TargetsLeft = m_TargetsTotl;
        m_fTargetShaderIncrement = (100f / m_TargetsTotl);

        StartCoroutine("UpdateTime");
        vUpdateTargetUIs();
    }

    IEnumerator UpdateTime()
    {
        while(true)
        {
            if(!PauseTime)
            {
                m_TimeFrames++;
                m_TimeSecs += Time.deltaTime;
                if (m_TimeSecs % 60f == 0)
                    AudioManagerEffects.Instance.PlaySound(AudioManagerEffects.Effects.GameOneMin);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void vStopCounting()
    {
        StopCoroutine("UpdateTime");
    }

    void vUpdateTargetUIs()
    {
        if (!m_UITargetsLeft && Application.loadedLevelName.Contains("Tutorial"))
            return;
        else if (!m_UITargetsLeft && Application.loadedLevelName.Contains("Sandbox"))
            return;
        else if (!m_UITargetsLeft)
        {
            Debug.LogError("No Target UI objs set");
            return;
        }
        else
        {
            m_UITargetsLeft.vSetNumberOfImages();
            m_UITargetsShot.vSetNumberOfImages();
        }        
    }

    public void TargetShot()
    {
        m_TargetsLeft--;
        vUpdateTargetUIs();

        UpdateShader();
    }

    public void Shoot()
    {
        m_BullsShot++;
    }

    public void Restart()
    {
        Debug.Log("gamedata restart");

        m_TargetsLeft = m_TargetsTotl;
        m_BullsShot = 0;
        m_TimeFrames = 0;
        m_TimeSecs = 0;

        UpdateShader();
        vUpdateTargetUIs();
    }

    public void Fell()
    {
        m_TimesFell++;
    }

    private void UpdateShader()
    {
        if (!Application.loadedLevelName.Contains("San"))
        {
            float _currShaderVal = fCurrShaderIncrement * 0.01f;
            foreach (GameObject _targ in agoTargets)
                _targ.GetComponent<TargetFragmentation>().SetShaderSeeThrough(_currShaderVal);
        }
    }
}
