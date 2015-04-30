﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameRdyCountdown : MonoBehaviour {

    [SerializeField] private RectTransform ObjToScale;
    [SerializeField] private Text ObjTextToSet;
    //player controls script

    [SerializeField] private List<GameObject> ObjsToActivateWhenComplete;
    [SerializeField] private List<GameObject> ObjsToDeActivateWhenComplete;

    [SerializeField] private PlayerMovementScript PlayerControlObj;
    [SerializeField] private PlayerShootLaser PlayerShootObj;
    [SerializeField] private GameEndLvl EndTrigger;

    public void StartCountdown()
    {
        StartCoroutine("Countdown");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.InGame);
    }

    IEnumerator Countdown()
    {
        float _Timer = 1f;

        int _Count = 10;
        ObjTextToSet.text = _Count.ToString();

        Vector3 _Scale = new Vector3();

        while (_Count > 0)
        {
            _Timer -= Time.deltaTime;
            _Scale.x = _Timer;
            _Scale.y = _Timer;
            _Scale.z = _Timer;

            ObjToScale.localScale = _Scale;

            if(_Timer < 0.15f)
            {
                AudioManagerEffects.Instance.PlaySound(AudioManagerEffects.Effects.Countdown);
                _Timer = 0.99f;
                _Count--;
                ObjTextToSet.text = _Count.ToString();
            }

            yield return new WaitForEndOfFrame();
        }

        PlayerControlObj.AllowControls(true, true);
        PlayerShootObj.bCanShoot = true;
        EndTrigger.bCanEnd = true;

        foreach (GameObject obj in ObjsToActivateWhenComplete)
        {
            obj.SetActive(true);

            if(obj.GetComponent<GameUIArrowScript>()
                || obj.GetComponent<GameUITextScript>())
            {
                obj.SendMessage("Reset");
            }
        }

        GameData.Instance.StartData();

        foreach (GameObject obj in ObjsToDeActivateWhenComplete)
            obj.SetActive(false);
    }
}
