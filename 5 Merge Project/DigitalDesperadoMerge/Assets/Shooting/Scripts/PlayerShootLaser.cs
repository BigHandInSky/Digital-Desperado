using UnityEngine;
using System.Collections;

public class PlayerShootLaser : MonoBehaviour {

    //bool called by GUI to enable/disable shooting as needed
    public bool bCanShoot = false;
	//Prefabs of the laser object
	public GameObject prefabLaser;
    public GameObject gunMuzzle;
	//Audio clip
	public AudioClip gunShot;
	//Damage value of the laser
	public int iDamage = 50;
	//The more you increase the range variable, the more it will be easier for the player to hit the target
	public float range = 0.55f;

	//Use to delay shooting sequence
	public float fShootTimer = 0.0f;

	public GameObject prefabBulletTrigger;
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

			GameObject bullet = (GameObject)Instantiate(prefabBullet, Camera.main.transform.position + Camera.main.transform.forward, Quaternion.identity);
			bullet.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 120;

			//trigger object
			GameObject triggerEffect = (GameObject)Instantiate(prefabBulletTrigger, Camera.main.transform.position, Quaternion.identity);
			triggerEffect.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 120;

			fShootTimer = 0.5f;

			/*RaycastHit hit;
			int layerMask = (1 << 8 ) | (1 << 3);
			
			if (Physics.Raycast (Camera.main.transform.position, laser.GetComponent<LaserScript> ().V3endPosition, out hit, ~layerMask)) 
			{
				//if(hit.collider.gameObject.layer == )
				Debug.Log(hit.collider.gameObject.name);
				//Vector3 v3 = (hit.point - Camera.main.transform.position).normalized;
				//GameObject t = Instantiate (test, hit.point + v3, Quaternion.identity) as GameObject;
			}
			*/
		}
	}
}
