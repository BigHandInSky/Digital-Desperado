using UnityEngine;
using System.Collections;

public class GameEndLvl : MonoBehaviour {

    [SerializeField] private GameObject goObjToActivate;
    [SerializeField] private GameObject goObjToDeActivate;
    [SerializeField] private EndScreenStart EndScreenObj;
    private const string sPlayerTag = "Player";

    [SerializeField] private PlayerMovementScript PlayerControlObj;
    [SerializeField] private PlayerShootLaser PlayerShootObj;

    public bool bCanEnd = false;

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
        if (bCanEnd)
        {
            GeneralControlKeys.Instance.bCanRestartOrMenu = false;

            PlayerControlObj.AllowControls(false, false);
            PlayerShootLaser.bCanShoot = false;
            bCanEnd = false;

            GameData.Instance.vStopCounting();
            goObjToActivate.SetActive(true);
            goObjToDeActivate.SetActive(false);
            EndScreenObj.SetupEndScreen();

            AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.Other);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
