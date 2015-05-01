using UnityEngine;
using System.Collections;

public class GunBehavior : MonoBehaviour {

    private enum BehaviourState
    {
        Shooting,
        Recharging,
        Other
    }

	public GameObject parentObj;
    public GameObject gunModel;
    public PlayerMovementScript moveScript;

	private float xAxis = 0;
	private float yAxis = 0;
    
    private KeyCode Shoot = KeyCode.Mouse0;

    private Vector3 ModelOrigin;
    private Vector3 fGunSineChange = new Vector3();

    private BehaviourState CurrentState = BehaviourState.Other;

    void Awake()
    {
        moveScript = GameObject.FindObjectOfType<PlayerMovementScript>();
        ModelOrigin = gunModel.transform.localPosition;

        if (!GameSettings.Instance)
            return;

        Shoot = GameSettings.Instance.Fire;
    }

    void Update()
    {
		xAxis += 0.02f;
		yAxis += 0.06f;

		if (Camera.main.GetComponent<PlayerShootLaser> ().bCanShoot 
            && Input.GetKey(Shoot) 
            && Camera.main.GetComponent<PlayerShootLaser> ().fShootTimer <= 0) 
        {
            CurrentState = BehaviourState.Shooting;
            gunModel.transform.localPosition = ModelOrigin;
		}

        if (CurrentState == BehaviourState.Shooting) 
        {
			gunModel.transform.Translate (new Vector3 (0, 0, -0.1f), Space.Self);

            if (gunModel.transform.localPosition.z < -0.4f)
                CurrentState = BehaviourState.Recharging;
		}
        else if (CurrentState == BehaviourState.Recharging) 
        {
			gunModel.transform.Translate (new Vector3 (0, 0, 0.05f), Space.Self);

			if(gunModel.transform.localPosition.z >= 0)
                CurrentState = BehaviourState.Other;
        }
        else if (moveScript.bHasJumped)
        {
            fGunSineChange.z = ModelOrigin.z;

            fGunSineChange.x = ModelOrigin.x + Mathf.Sin(Time.realtimeSinceStartup * 0.1f) * 0.06f;
            fGunSineChange.y = ModelOrigin.y + Mathf.Sin(Time.realtimeSinceStartup * 0.1f) * 0.04f;

            gunModel.transform.localPosition = fGunSineChange;
        }
        else if (moveScript.CurrSpeed.magnitude > 0f)
        {
            fGunSineChange.z = ModelOrigin.z + Mathf.Sin(Time.realtimeSinceStartup * Mathf.Abs(moveScript.CurrSpeed.y * 0.5f)) * 0.1f;
            fGunSineChange.x = ModelOrigin.x + Mathf.Sin(Time.realtimeSinceStartup * Mathf.Abs(moveScript.CurrSpeed.x * 0.4f)) * 0.04f;
            fGunSineChange.y = ModelOrigin.y + Mathf.Sin(Time.realtimeSinceStartup * 0.2f) * 0.04f;

            gunModel.transform.localPosition = fGunSineChange;
        }
        else
        {
            fGunSineChange.x = ModelOrigin.x;
            fGunSineChange.z = ModelOrigin.z;

            fGunSineChange.y = ModelOrigin.y + Mathf.Sin(Time.realtimeSinceStartup * 0.2f) * 0.04f;

            gunModel.transform.localPosition = fGunSineChange;
        }

		transform.eulerAngles = new Vector3 (parentObj.transform.eulerAngles.x + Mathf.Sin(xAxis) * 2, transform.eulerAngles.y + ( (Input.GetAxis("Vertical") != 0) ? Mathf.Sin(0) * 0.5f : Mathf.Sin(0) * 0.02f ), transform.eulerAngles.z);
	}
}
