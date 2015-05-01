using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapSpawnerScript : MonoBehaviour {

    public enum ObjType
    {
        Set,
        Scalable
    }

    [SerializeField] private ObjType EnumSetting;
    [SerializeField] private GameObject ObjToSpawn;
    [SerializeField] private List<GameObject> Objects = new List<GameObject>();

    private float SizeScal;
    private float PosScal;

    void Start()
    {
        SizeScal = GetComponentInParent<MapUI>().SizeScale;
        PosScal = GetComponentInParent<MapUI>().PosScale;
    }

    public void ClearImages()
    {
        foreach (GameObject obj in Objects)
        {
            obj.SendMessage("vDelete");
        }

        Objects.Clear();
    }

    public void Create(Vector3 _pos, Vector3 _size, float _zRot)
    {
        if (EnumSetting == ObjType.Set)
            vCreateMapUIObj(_pos, _size, _zRot);
        else if (EnumSetting == ObjType.Scalable)
            vCreateMapUIObj(_pos, _size, _zRot);
    }

    private void vCreateMapUIObj(Vector3 _pos, float _zAxisRot)
    {
		/*GameObject mapUIImg = (GameObject)Instantiate(ObjToSpawn, gameObject.transform.position, gameObject.transform.rotation);
        mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);
        mapUIImg.GetComponent<RectTransform>().localScale = Vector3.one;
        Vector2 _spawn = new Vector2(_pos.x, _pos.z);
        mapUIImg.GetComponent<RectTransform>().localPosition = _spawn * PosScal;
        mapUIImg.GetComponent<RectTransform>().Rotate(0f, 0f, _zAxisRot);

        Objects.Add(mapUIImg);
		*/

		GameObject mapUIImg = (GameObject)Instantiate(ObjToSpawn, gameObject.transform.position, gameObject.transform.rotation);
		mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);
		mapUIImg.GetComponent<RectTransform> ().localPosition = new Vector3(_pos.x, _pos.z, 0);

		Vector3 _temp = new Vector3(SizeScal * 3, SizeScal * 3, 1f);
		mapUIImg.GetComponent<RectTransform>().localScale = _temp;

		mapUIImg.GetComponent<RectTransform>().Rotate(0f, 0f, _zAxisRot);
		
		Objects.Add(mapUIImg);
	}
	private void vCreateMapUIObj(Vector3 _pos, Vector3 _scale, float _zAxisRot)
    {
        /*GameObject mapUIImg = (GameObject)Instantiate(ObjToSpawn, gameObject.transform.position, gameObject.transform.rotation);
        mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);

        Vector3 _temp = new Vector3(1 * _scale.x, 1 * _scale.z, 1f);
        //print(_temp);
        mapUIImg.GetComponent<RectTransform>().localScale = _temp;
        Vector2 _spawn = new Vector2(_pos.x, _pos.z);
        mapUIImg.GetComponent<RectTransform>().localPosition = _spawn * PosScal;
        mapUIImg.GetComponent<RectTransform>().Rotate(0f, 0f, _zAxisRot);

        Objects.Add(mapUIImg);*/

		GameObject mapUIImg = (GameObject)Instantiate(ObjToSpawn, gameObject.transform.position, gameObject.transform.rotation);
		mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);
		mapUIImg.GetComponent<RectTransform> ().localPosition = new Vector3(_pos.x, _pos.z, 0);

		Vector3 _temp = new Vector3(SizeScal * _scale.x * 0.9f, SizeScal * _scale.z * 0.9f, 1f);
		mapUIImg.GetComponent<RectTransform>().localScale = _temp;

		mapUIImg.GetComponent<RectTransform>().Rotate(0f, 0f, _zAxisRot);
		Objects.Add(mapUIImg);
	}

}
