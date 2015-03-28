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

    [SerializeField] private GameObject m_UITargetsLeft;
    [SerializeField] private GameObject m_UITargetsShot;

    private int m_TargetsLeft;
    public int iTargsLft { get { return m_TargetsLeft; } }
    private int m_TargetsTotl;
    public int iTargsTtl { get { return m_TargetsTotl; } }

    private int m_BullsShot;
    public int iBullsShot { get { return m_BullsShot; } }

    private int m_TimeFrames;
    public int iTimeFr { get { return m_TimeFrames; } }
    private float m_TimeSecs;
    public float fTimeScs { get { return m_TimeSecs; } }
    
    void Awake() 
    {
        m_TargetsLeft = 3;
        m_TargetsTotl = 5;
        m_BullsShot = 1;
        vUpdateTargetUIs();

        m_DataInstance = this;
        StartCoroutine("UpdateTime");
    }

    IEnumerator UpdateTime()
    {
        while(true)
        {
            m_TimeFrames++;
            m_TimeSecs += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    void vStopCounting()
    {
        StopCoroutine("UpdateTime");
    }

    void vUpdateTargetUIs()
    {
        m_UITargetsLeft.SendMessage("vSetNumberOfImages");
        m_UITargetsShot.SendMessage("vSetNumberOfImages");
    }
}
