using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameRdyCountdown : MonoBehaviour {

    [SerializeField] private RectTransform ObjToScale;
    [SerializeField] private Text ObjTextToSet;

    [SerializeField] private List<GameObject> ObjsToActivateWhenComplete;
    [SerializeField] private List<GameObject> ObjsToDeActivateWhenComplete;
    [SerializeField] private GameObject SpeedUpTextObj;

    [SerializeField] private PlayerMovementScript PlayerControlObj;
    [SerializeField] private GameEndLvl EndTrigger;

    public void StartCountdown()
    {
        SpeedUpTextObj.SetActive(true);

        StartCoroutine("Countdown");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        AudioManagerMusic.Instance.SetMusic(AudioManagerMusic.MusicType.InGame);
    }

    IEnumerator Countdown()
    {
        AudioManagerEffects.Instance.PlaySound(AudioManagerEffects.Effects.Countdown);

        float _Timer = 1f;

        int _Count = 10;
        ObjTextToSet.text = _Count.ToString();

        Vector3 _Scale = new Vector3();

        while (_Count > 0)
        {
            if (Input.anyKey && !Input.GetKey(GameSettings.Instance.Rset) && _Count > 3)
            {
                AudioManagerEffects.Instance.PlaySound(AudioManagerEffects.Effects.Countdown);

                _Timer = 0.99f;
                _Scale.x = _Timer;
                _Scale.y = _Timer;
                _Scale.z = _Timer;

                _Count = 3;
                ObjTextToSet.text = _Count.ToString();

                SpeedUpTextObj.SetActive(false);
                continue;
            }
            else if (_Timer < 0.15f)
            {
                if (_Count == 1)
                    ObjToScale.localScale = Vector3.zero;
                else
                    AudioManagerEffects.Instance.PlaySound(AudioManagerEffects.Effects.Countdown);

                _Timer = 0.99f;
                _Count--;
                ObjTextToSet.text = _Count.ToString();

            }
            else
            {
                _Timer -= Time.deltaTime;
                _Scale.x = _Timer;
                _Scale.y = _Timer;
                _Scale.z = _Timer;

                ObjToScale.localScale = _Scale;
            }
            
            if (_Count <= 3 && !EndTrigger.bCanEnd)
            {
                EndTrigger.bCanEnd = true;
            }
            
            yield return new WaitForEndOfFrame();
        }

        Finished();
    }

    private void Finished()
    {

        PlayerControlObj.AllowControls(true, true);
        PlayerShootLaser.bCanShoot = true;
        //EndTrigger.bCanEnd = true;

        foreach (GameObject obj in ObjsToActivateWhenComplete)
        {
            obj.SetActive(true);

            if (obj.GetComponent<GameUIArrowScript>()
                || obj.GetComponent<GameUITextScript>()
                || obj.GetComponent<GameUIEnabledAbility>())
            {
                obj.SendMessage("Reset");
            }
        }

        GameData.Instance.StartData();

        foreach (GameObject obj in ObjsToDeActivateWhenComplete)
            obj.SetActive(false);

        GeneralControlKeys.Instance.bCanExit = true;
    }
}
