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

	//Use to delay shooting sequence
	private float fShootTimer = 0.0f;


    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName.Contains("Tutorial"))
            bCanShoot = true;
        else
            bCanShoot = false;
    }

	
	void Update () 
	{
		//Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position +  Camera.main.transform.forward * 100, Color.red);
		fShootTimer -= Time.deltaTime;

		//On mouse click instantiate a new prefab laser and set the position
		if (Input.GetMouseButtonDown (0) && bCanShoot)
			vShoot ();
	}

	public void vShoot()
	{
		//if script timer reaches 0, allow the player to use the mouse click
		if (fShootTimer <= 0) {
			GetComponent<AudioSource> ().PlayOneShot (gunShot);
			
			GameObject laser = Instantiate (prefabLaser, gunMuzzle.transform.position, Quaternion.identity) as GameObject;
			laser.GetComponent<LaserScript> ().V3startPosition = laser.transform.position;
			laser.GetComponent<LaserScript> ().V3endPosition = Camera.main.transform.position + Camera.main.transform.forward * 100;
			
			RaycastHit hit;
			
			if (Physics.Raycast (Camera.main.transform.position, laser.GetComponent<LaserScript> ().V3endPosition, out hit)) {
				//Debug.Log (hit.transform.name);
				if (hit.transform.GetComponent<TargetFragmentation> ()) {
					hit.transform.GetComponent<TargetFragmentation> ().vExplode ();
				}
			}
			fShootTimer = 0.5f;
		}
	}
}
