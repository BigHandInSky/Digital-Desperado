using UnityEngine;
using System.Collections;

public class GameEndLvl : MonoBehaviour {

    [SerializeField] private GameObject goObjToActivate;
    [SerializeField] private GameObject goObjToDeActivate;
    [SerializeField] private GameLdrBrdGetData LeaderBoard;
    private const string sPlayerTag = "Player";

    void OnCollisionEnter(Collision _coll)
    {
        if (_coll.gameObject.tag == sPlayerTag)
            DoAction();
    }

    void OnTriggerEnter(Collider _coll)
    {
        if (_coll.gameObject.tag == sPlayerTag)
            DoAction();
    }

    private void DoAction()
    {
        GameData.Instance.vStopCounting();
        goObjToActivate.SetActive(true);
        goObjToDeActivate.SetActive(false);
        LeaderBoard.Load();

        AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.EndGame);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
