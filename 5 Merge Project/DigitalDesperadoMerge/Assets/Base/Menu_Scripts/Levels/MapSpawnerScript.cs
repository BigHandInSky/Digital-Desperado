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
    public void vCreateMapUIObj(Vector3 _pos, Vector3 _scale, float _zAxisRot)
    {
		GameObject mapUIImg = (GameObject)Instantiate(ObjToSpawn, gameObject.transform.position, gameObject.transform.rotation);
		mapUIImg.GetComponent<RectTransform>().SetParent(gameObject.transform);
        mapUIImg.GetComponent<RectTransform>().localPosition = new Vector3(_pos.x * PosScal, _pos.z * PosScal, 0);

		Vector3 _temp = new Vector3(SizeScal * _scale.x * 0.9f, SizeScal * _scale.z * 0.9f, 1f);
		mapUIImg.GetComponent<RectTransform>().localScale = _temp;

		mapUIImg.GetComponent<RectTransform>().Rotate(0f, 0f, _zAxisRot);
		Objects.Add(mapUIImg);
	}
}
