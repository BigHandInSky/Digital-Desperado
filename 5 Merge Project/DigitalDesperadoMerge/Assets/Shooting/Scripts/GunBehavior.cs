using UnityEngine;
using System.Collections;

public class GunBehavior : MonoBehaviour {


	public GameObject parentObj;
	public GameObject gunModel;

	private float xAxis = 0;
	private float yAxis = 0;
	private bool isShooting = false;
	private bool recharge = false;

	// Update is called once per frame
	void Update () {
		xAxis += 0.02f;
		yAxis += 0.06f;

		/*
		 * Dont Delete
		if (Input.GetMouseButtonDown (0)) {
			isShooting = true;
			Debug.Log("Something happen");
		}

		if(isShooting)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime);

			if(transform.rotation == Quaternion.identity)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShootLaser>().vShoot();
				isShooting = false;
			}
		}
		else
		 */

		if (Camera.main.GetComponent<PlayerShootLaser> ().bCanShoot && Input.GetMouseButtonDown (0) && Camera.main.GetComponent<PlayerShootLaser> ().fShootTimer <= 0) {
			isShooting = true;
		}

		if (isShooting && !recharge) {
			gunModel.transform.Translate (new Vector3 (0, 0, -0.1f), Space.Self);
			if (gunModel.transform.localPosition.z < -0.4f)
				recharge = true;
		} else if (recharge) {
			gunModel.transform.Translate (new Vector3 (0, 0, 0.05f), Space.Self);
			if(gunModel.transform.localPosition.z >= 0)
				recharge = false;
				isShooting = false;
		}

		transform.eulerAngles = new Vector3 (parentObj.transform.eulerAngles.x + Mathf.Sin(xAxis) * 2, transform.eulerAngles.y + ( (Input.GetAxis("Vertical") != 0) ? Mathf.Sin(0) * 0.5f : Mathf.Sin(0) * 0.02f ), transform.eulerAngles.z);
	}
}
