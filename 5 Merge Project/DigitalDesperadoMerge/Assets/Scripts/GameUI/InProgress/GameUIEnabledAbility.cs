using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIEnabledAbility : MonoBehaviour 
{
    [SerializeField] private PlayerMovementScript MoveScript;
    [SerializeField] private PlayerShootLaser ShootScript;

    public enum AbilityType
    {
        Jump,
        Shoot,
        EndBG
    }
    [SerializeField] private AbilityType UIType;

    [SerializeField] private Color colEnabled;
    [SerializeField] private Color colDisabled;

    [SerializeField] private Image ImgComponent;
    [SerializeField] private Text TxtObj;

    private bool CR_running = false;

    void Start()
    {
        if (!CR_running)
            StartCoroutine(CheckForAbility());
    }

    public void Reset()
    {
        StartCoroutine(CheckForAbility());
    }

    IEnumerator CheckForAbility()
    {
        CR_running = true;

        while (true)
        {
            if (UIType == AbilityType.Jump)
            {
                SetColour(!MoveScript.bHasJumped);
            }
            else if (UIType == AbilityType.Shoot)
            {
                SetColour(ShootScript.fShootTimer < 0f);
            }
            else if (UIType == AbilityType.EndBG)
            {
                SetColour(GameData.Instance.iTargsLft < 1);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void SetColour(bool _val)
    {
        if (_val)
        {
            ImgComponent.color = colEnabled;
            TxtObj.color = colEnabled;
        }
        else
        {
            ImgComponent.color = colDisabled;
            TxtObj.color = colDisabled;
        }
    }
}
