﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUITargetScript : MonoBehaviour {

    [SerializeField]
    private GameObject m_TargetToSpawn;
    [SerializeField]
    private bool m_CheckTargetsLeft;
    [SerializeField]
    private List<GameObject> m_TargetUIs = new List<GameObject>();
    private int iLastNum = 0;

    public void vSetNumberOfImages()
    {
        //temp int
        int _num = 0;

        //get either what is left, or what has been shot by total - left
        if (m_CheckTargetsLeft == true)
            _num = GameData.Instance.iTargsLft;
        else
            _num = (GameData.Instance.iTargsTtl - GameData.Instance.iTargsLft);

        if (_num == iLastNum)
            return;

        iLastNum = _num;

        if (_num != 0)
        {
            foreach (GameObject obj in m_TargetUIs)
            {
                DestroyObject(obj);
            }
            m_TargetUIs.Clear();

            //add new objects as children, who will automatically align
            for (int loop = 0; loop < _num; loop++)
            {
                GameObject targUIClone = Instantiate(m_TargetToSpawn);
                targUIClone.GetComponent<RectTransform>().SetParent(this.gameObject.GetComponent<RectTransform>());
                targUIClone.GetComponent<RectTransform>().localScale = Vector3.one;
                m_TargetUIs.Add(targUIClone);
            }
        }    
        else
        {
            foreach (GameObject obj in m_TargetUIs)
            {
                DestroyObject(obj);
            }
            m_TargetUIs.Clear();
        }
    }
}
