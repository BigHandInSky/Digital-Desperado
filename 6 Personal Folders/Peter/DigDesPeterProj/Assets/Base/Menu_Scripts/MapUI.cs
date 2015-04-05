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

    public void vClearMap()
    {
        foreach (GameObject obj in m_MapImages)
        {
            obj.SendMessage("vDelete");
        }

        m_MapImages.Clear();
    }
    
    public void vSetupMapUIPlayer(Vector2 _Pos)
    {
        vCreateMapUIObj(_Pos,MapPlyImg);
    }
    public void vSetupMapUIEndTower(Vector2 _Pos)
    {
        vCreateMapUIObj(_Pos, MapEndImg);
    }
    public void vSetupMapUITargets(List<Vector2> _poss)
    {
        foreach(Vector2 variable in _poss)
        {
            vCreateMapUIObj(variable, MapEndImg);
        }
    }
    public void vSetupMapUILevels(List<Vector2> _poss, List<Vector2> _scales)
    {
        int i = 0;
        foreach (Vector2 variable in _poss)
        {
            vCreateMapUIObj(variable, _scales[i], MapLvlImg);
        }
    }
    public void vSetupMapUITowers(List<Vector2> _poss, List<Vector2> _scales)
    {
        int i = 0;
        foreach (Vector2 variable in _poss)
        {
            vCreateMapUIObj(variable, _scales[i], MapTwrImg);
        }
    }

    private void vCreateMapUIObj(Vector2 _pos, GameObject _obj)
    {
        GameObject mapUIImg = (GameObject)Instantiate(_obj, gameObject.transform.position, gameObject.transform.rotation);
        mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);
        mapUIImg.GetComponent<RectTransform>().localScale = Vector3.one;
        mapUIImg.GetComponent<RectTransform>().localPosition = _pos;

        m_MapImages.Add(mapUIImg);
    }
    private void vCreateMapUIObj(Vector2 _pos, Vector2 _scale, GameObject _obj)
    {
        GameObject mapUIImg = (GameObject)Instantiate(_obj, gameObject.transform.position, gameObject.transform.rotation);
        mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);

        Vector3 _temp = new Vector3(_scale.x, _scale.y, 1f);
        mapUIImg.GetComponent<RectTransform>().localScale = _temp;
        mapUIImg.GetComponent<RectTransform>().localPosition = _pos;

        m_MapImages.Add(mapUIImg);
    }
}
