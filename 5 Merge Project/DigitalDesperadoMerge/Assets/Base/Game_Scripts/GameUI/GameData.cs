using UnityEngine;
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

    [SerializeField] private Transform m_Camera;
    public Transform trCamera { get { return m_Camera; } }
    [SerializeField] private Transform m_EndLvlTow;
    public Transform trEndLvlTow { get { return m_EndLvlTow; } }

    [SerializeField] private GameUITargetScript m_UITargetsLeft;
    [SerializeField] private GameUITargetScript m_UITargetsShot;

    private int m_TargetsLeft = 0;
    public int iTargsLft { get { return m_TargetsLeft; } }
    [SerializeField] private int m_TargetsTotl = 0;
    public int iTargsTtl { get { return m_TargetsTotl; } }

    private int m_BullsShot = 0;
    public int iBullsShot { get { return m_BullsShot; } }

    private int m_TimeFrames = 0;
    public int iTimeFr { get { return m_TimeFrames; } }
    private float m_TimeSecs = 0;
    public float fTimeScs { get { return m_TimeSecs; } }
    
    void Awake() 
    {
        m_DataInstance = this;
        StartData();
    }

    public void StartData()
    {
        m_TargetsLeft = m_TargetsTotl;
        StartCoroutine("UpdateTime");
        vUpdateTargetUIs();
    }

    IEnumerator UpdateTime()
    {
        while(true)
        {
            m_TimeFrames++;
            m_TimeSecs += Time.deltaTime;
            vUpdateTargetUIs();
            yield return new WaitForEndOfFrame();
        }
    }

    public void vStopCounting()
    {
        StopCoroutine("UpdateTime");
    }

    void vUpdateTargetUIs()
    {
        m_UITargetsLeft.vSetNumberOfImages();
        m_UITargetsShot.vSetNumberOfImages();
    }

    public void TargetShot()
    {
        m_TargetsLeft--;
        vUpdateTargetUIs();
    }

    public void Shoot()
    {
        m_BullsShot++;
    }

    public void Restart()
    {
        m_TargetsLeft = 0;
        m_TargetsTotl = 0;
        m_BullsShot = 0;
        m_TimeFrames = 0;
        m_TimeSecs = 0;
    }
}
