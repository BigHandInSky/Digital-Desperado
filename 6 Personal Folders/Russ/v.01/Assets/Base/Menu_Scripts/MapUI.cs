using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//script which handles data from LoadedLevelsData, then creates a 2D map from it
public class MapUI : MonoBehaviour {

    [SerializeField] private List<GameObject> m_MapImages = new List<GameObject>();

    [SerializeField] private GameObject MapPlyImg;
    [SerializeField] private GameObject MapTrgImg;
    [SerializeField] private GameObject MapLvlImg;
    [SerializeField] private GameObject MapTwrImg;
    [SerializeField] private GameObject MapEndImg;

    public void vRefresh(Vector2 _pos)
    {
        foreach (GameObject obj in m_MapImages)
        {
            obj.SendMessage("vDelete");
        }

        m_MapImages.Clear();
        SetupMap(_pos);
    }

    void SetupMap(Vector2 _newpos)
    {
        GameObject playerImg = (GameObject)Instantiate(MapPlyImg,gameObject.transform.position,gameObject.transform.rotation);
        playerImg.GetComponent<RectTransform>().SetParent(gameObject.transform);
        playerImg.GetComponent<RectTransform>().localScale = Vector3.one;
        playerImg.GetComponent<RectTransform>().localPosition = _newpos;

        m_MapImages.Add(playerImg);
    }
}
