using UnityEngine;
using System.Collections;

public class Catcher : MonoBehaviour {

    private Transform tPlayerStartPosition;

    private const string sPlayerTag = "Player";
    private const string sFragTag = "Effect";
    private const string sTarTag = "Target";

    void OnCollisionEnter(Collision _coll)
    {
        if (_coll.gameObject.tag == sPlayerTag)
            DoAction(2, _coll.gameObject);
        else if (_coll.gameObject.tag == sFragTag)
            DoAction(1, _coll.gameObject);
        else if (_coll.gameObject.tag == sTarTag)
            DoAction(3, _coll.gameObject);
    }

    void OnTriggerEnter(Collider _coll)
    {
        //Debug.Log("catcher triggered by: " + _coll.gameObject.tag);

        if (_coll.gameObject.tag == sPlayerTag)
            DoAction(2, _coll.gameObject);
        else if (_coll.gameObject.tag == sFragTag)
            DoAction(1, _coll.gameObject);
        else if (_coll.gameObject.tag == sTarTag)
            DoAction(3, _coll.gameObject);
    }

    private void DoAction(int _type, GameObject _obj)
    {
        if (!tPlayerStartPosition)
            tPlayerStartPosition = GameObject.FindGameObjectWithTag("SpawnPosition").transform;

        if (_type == 1)
            DestroyObject(_obj);
        else if (_type == 2)
        {
            if (GameData.Instance)
                GameData.Instance.Fell();
            _obj.transform.position = tPlayerStartPosition.position;
            _obj.transform.rotation = tPlayerStartPosition.rotation;
            _obj.GetComponent<PlayerMovementScript>().Reset();
        }
        else if (_type == 3)
        {
            _obj.GetComponent<TargetFragmentation>().ResetPosition();
        }
    }
}
