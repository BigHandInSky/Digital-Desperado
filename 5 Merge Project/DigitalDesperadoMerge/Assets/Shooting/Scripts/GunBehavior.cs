using UnityEngine;
using System.Collections;

public class GunBehavior : MonoBehaviour {


	public GameObject obj;
	private float xAxis = 0;
	private float yAxis = 0;

	private bool isShooting = false;

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
		transform.eulerAngles = new Vector3 (obj.transform.eulerAngles.x + Mathf.Sin(xAxis) * 2, transform.eulerAngles.y + ( (Input.GetAxis("Vertical") > 0) ? Mathf.Sin(yAxis) * 0.5f : Mathf.Sin(yAxis) * 0.02f ), transform.eulerAngles.z);
	}
}
