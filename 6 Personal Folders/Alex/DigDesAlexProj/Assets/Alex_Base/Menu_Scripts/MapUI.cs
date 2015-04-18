using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//script which handles data from LoadedLevelsData, then creates a 2D map from it
public class MapUI : MonoBehaviour {

    [SerializeField] private List<GameObject> m_MapImages = new List<GameObject>();

    [SerializeField] private GameObject MapPlyImg;
    [SerializeField] private GameObject MapTrgImg;
    [SerializeField] private List<GameObject> MapLvlsImgs;
    [SerializeField] private List<GameObject> MapTwrsImgs;
    [SerializeField] private GameObject MapEndImg;

    [SerializeField] private float MapPosScale = 5f;
    [SerializeField] private float MapSizeScale = 0.20f;

    public void vClearMap()
    {
        foreach (GameObject obj in m_MapImages)
        {
            obj.SendMessage("vDelete");
        }

        m_MapImages.Clear();
    }
    
    public void vSetupMapUIPlayer(Vector2 _Pos, float _yAxisRot)
    {
        vCreateMapUIObj(_Pos, _yAxisRot, MapPlyImg);
    }
    public void vSetupMapUIEndTower(Vector3 _Pos, float _yAxisRot)
    {
        vCreateMapUIObj(_Pos, _yAxisRot, MapEndImg);
    }
    public void vSetupMapUITarget(Vector3 _Pos, float _yAxisRot)
    {
        vCreateMapUIObj(_Pos, _yAxisRot, MapTrgImg);
    }
    public void vSetupMapUILevel(Vector3 _Pos, Vector3 _Scale, float _yAxisRot)
    {
        vCreateMapUIObj(_Pos, _Scale, _yAxisRot, MapLvlsImgs[0]);
    }
    public void vSetupMapUITower(Vector3 _Pos, Vector3 _Scale, float _yAxisRot)
    {
        vCreateMapUIObj(_Pos, _Scale, _yAxisRot, MapTwrsImgs[0]);
    }

    private void vCreateMapUIObj(Vector3 _pos, float _zAxisRot, GameObject _obj)
    {
        GameObject mapUIImg = (GameObject)Instantiate(_obj, gameObject.transform.position, gameObject.transform.rotation);
        mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);
        mapUIImg.GetComponent<RectTransform>().localScale = Vector3.one;
        Vector2 _spawn = new Vector2(_pos.x, _pos.z);
        mapUIImg.GetComponent<RectTransform>().localPosition = _spawn * MapPosScale;
        //mapUIImg.GetComponent<RectTransform>().Rotate(0f,0f,_zAxisRot);

        m_MapImages.Add(mapUIImg);
    }
    private void vCreateMapUIObj(Vector3 _pos, Vector3 _scale, float _zAxisRot, GameObject _obj)
    {
        GameObject mapUIImg = (GameObject)Instantiate(_obj, gameObject.transform.position, gameObject.transform.rotation);
        mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);

        Vector3 _temp = new Vector3(MapSizeScale * _scale.x, MapSizeScale * _scale.z, 1f);
        print(_temp);
        mapUIImg.GetComponent<RectTransform>().localScale = _temp;
        Vector2 _spawn = new Vector2(_pos.x, _pos.z);
        mapUIImg.GetComponent<RectTransform>().localPosition = _spawn * MapPosScale;
        mapUIImg.GetComponent<RectTransform>().Rotate(0f, 0f, _zAxisRot);

        m_MapImages.Add(mapUIImg);
    }
}
