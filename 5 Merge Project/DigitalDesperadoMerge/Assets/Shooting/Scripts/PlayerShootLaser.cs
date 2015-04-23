using UnityEngine;
using System.Collections;

public class PlayerShootLaser : MonoBehaviour {

    //bool called by GUI to enable/disable shooting as needed
    public bool bCanShoot = false;
	//Prefabs of the laser object
	public GameObject prefabLaser;
    public GameObject gunMuzzle;
	//Layermask for the raycast, not sure if we are going to use it
	public LayerMask layerMask;
	//Audio clip
	public AudioClip gunShot;
	//Damage value of the laser
	public int iDamage = 50;
	//The more you increase the range variable, the more it will be easier for the player to hit the target
	public float range = 0.55f;

	//Use to delay shooting sequence
	public float fShootTimer = 0.0f;


	public GameObject prefabBullet;

    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName.Contains("Tutorial"))
            bCanShoot = true;
        else
            bCanShoot = false;
    }

	
	void FixedUpdate () 
	{
		//Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * 100, Color.red);
		//Debug.DrawLine(Camera.main.transform.position + Camera.main.transform.right * range, Camera.main.transform.right * range + Camera.main.transform.position +  Camera.main.transform.forward * 100, Color.red);
		//Debug.DrawLine(Camera.main.transform.position - Camera.main.transform.right * range, Camera.main.transform.position +  Camera.main.transform.forward * 100 - Camera.main.transform.right * range, Color.red);
		//Debug.DrawLine(Camera.main.transform.position + Camera.main.transform.up * range, Camera.main.transform.position +  Camera.main.transform.forward * 100 + Camera.main.transform.up * range, Color.red);
		//Debug.DrawLine(Camera.main.transform.position - Camera.main.transform.up * range, Camera.main.transform.position +  Camera.main.transform.forward * 100 - Camera.main.transform.up * range, Color.red);

		fShootTimer -= Time.deltaTime;

		//On mouse click instantiate a new prefab laser and set the position
		if (Input.GetMouseButtonDown (0) && bCanShoot)
			vShoot ();
	}

	public void vShoot()
	{
		//if script timer reaches 0, allow the player to use the mouse click
        if (fShootTimer <= 0)
        {
            GameData.Instance.Shoot();
			GetComponent<AudioSource> ().PlayOneShot (gunShot);
			
			GameObject laser = Instantiate (prefabLaser, gunMuzzle.transform.position, Quaternion.identity) as GameObject;
			laser.GetComponent<LaserScript> ().V3startPosition = laser.transform.position;
			laser.GetComponent<LaserScript> ().V3endPosition = Camera.main.transform.position + Camera.main.transform.forward * 100;

			GameObject bullet = (GameObject)Instantiate(prefabBullet, Camera.main.transform.position, Quaternion.identity);
			bullet.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 120;

			/*
			 * 
			RaycastHit[] hits;
			hits = Physics.RaycastAll (Camera.main.transform.position, laser.GetComponent<LaserScript> ().V3endPosition, 500f);
			int i = 0;
			while (i < hits.Length) {
				RaycastHit hit = hits[i];
				Debug.Log(hit.transform.gameObject.name);
				if (hit.transform.GetComponent<TargetFragmentation> ()) {
					hit.transform.GetComponent<TargetFragmentation> ().vExplode ();
				}
				i++;
			}

			hits = Physics.RaycastAll (Camera.main.transform.position - Camera.main.transform.right * range, laser.GetComponent<LaserScript> ().V3endPosition - Camera.main.transform.right * range, 500f);
			i = 0;
			while (i < hits.Length) {
				RaycastHit hit = hits[i];
				if (hit.transform.GetComponent<TargetFragmentation> ()) {
					hit.transform.GetComponent<TargetFragmentation> ().vExplode ();
				}
				i++;
			}

			hits = Physics.RaycastAll (Camera.main.transform.position + Camera.main.transform.right * range, laser.GetComponent<LaserScript> ().V3endPosition + Camera.main.transform.right * range, 500f);
			i = 0;
			while (i < hits.Length) {
				RaycastHit hit = hits[i];
				if (hit.transform.GetComponent<TargetFragmentation> ()) {
					hit.transform.GetComponent<TargetFragmentation> ().vExplode ();
				}
				i++;
			}

			hits = Physics.RaycastAll (Camera.main.transform.position - Camera.main.transform.up * range, laser.GetComponent<LaserScript> ().V3endPosition - Camera.main.transform.up * range, 500f);
			i = 0;
			while (i < hits.Length) {
				RaycastHit hit = hits[i];
				if (hit.transform.GetComponent<TargetFragmentation> ()) {
					hit.transform.GetComponent<TargetFragmentation> ().vExplode ();
				}
				i++;
			}

			hits = Physics.RaycastAll (Camera.main.transform.position + Camera.main.transform.up * range, laser.GetComponent<LaserScript> ().V3endPosition + Camera.main.transform.up * range, 500f);
			i = 0;
			while (i < hits.Length) {
				RaycastHit hit = hits[i];
				if (hit.transform.GetComponent<TargetFragmentation> ()) {
					hit.transform.GetComponent<TargetFragmentation> ().vExplode ();
				}
				i++;
			}*/
			fShootTimer = 0.5f;
		}
	}
}
