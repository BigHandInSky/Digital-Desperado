using UnityEngine;
using System.Collections;

public class Catcher : MonoBehaviour {

    private Transform tPlayerStartPosition;

    private const string sPlayerTag = "Player";
    private const string sFragTag = "Effect";

    void OnCollisionEnter(Collision _coll)
    {
        if (_coll.gameObject.tag == sPlayerTag)
            DoAction(false, _coll.gameObject);
        else if (_coll.gameObject.tag == sFragTag)
            DoAction(true, _coll.gameObject);
    }

    void OnTriggerEnter(Collider _coll)
    {
        Debug.Log("catcher triggered by: " + _coll.gameObject.tag);

        if (_coll.gameObject.tag == sPlayerTag)
            DoAction(false, _coll.gameObject);
        else if (_coll.gameObject.tag == sFragTag)
            DoAction(true, _coll.gameObject);
    }

    private void DoAction(bool _delete, GameObject _obj)
    {
        tPlayerStartPosition = GameObject.FindGameObjectWithTag("SpawnPosition").transform; 

        if (_delete)
            DestroyObject(_obj);
        else
        {
            if (GameData.Instance)
                GameData.Instance.Fell();
            _obj.transform.position = tPlayerStartPosition.position;
            _obj.transform.rotation = tPlayerStartPosition.rotation;
        }
    }
}
