using UnityEngine;
using System.Collections;

public class PlayerShootLaser : MonoBehaviour {

    //bool called by GUI to enable/disable shooting as needed
    public bool bCanShoot = false;
	//Prefabs of the laser object
	public GameObject prefabLaser;
    public GameObject gunMuzzle;
	//Damage value of the laser
	public int iDamage = 50;
	//The more you increase the range variable, the more it will be easier for the player to hit the target
	public float range = 0.55f;

	//Use to delay shooting sequence
	public float fShootTimer = 0.0f;

	public GameObject prefabBulletTrigger;
	public GameObject prefabBullet;

	public LayerMask layerMask;
	public LayerMask layerMaskPlayer;

	public GameObject platformFrag;

    private KeyCode Shoot = KeyCode.Mouse0;

    void Awake()
    {
        Shoot = GameSettings.Instance.Fire;
    }

    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName.Contains("Tutorial") || Application.loadedLevelName.Contains("Sandbox"))
            bCanShoot = true;
        else
            bCanShoot = false;
    }

	
	void Update () 
	{
		//On mouse click instantiate a new prefab laser and set the position
        if (fShootTimer <= 0)
        {
            if (Input.GetKey(Shoot) && bCanShoot)
                vShoot();
        }
        else
            fShootTimer -= Time.deltaTime;
	}


	public void vShoot()
    {
        fShootTimer = 0.5f;

        GameData.Instance.Shoot();
        AudioManagerEffects.Instance.PlaySound(AudioManagerEffects.Effects.Shoot);

        GameObject laser = Instantiate(prefabLaser, gunMuzzle.transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<LaserScript>().V3startPosition = laser.transform.position;
        laser.GetComponent<LaserScript>().V3endPosition = Camera.main.transform.position + Camera.main.transform.forward * 40;

        GameObject bullet = (GameObject)Instantiate(prefabBullet, Camera.main.transform.position - Camera.main.transform.forward, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 120;


        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500f, layerMask))
        {
            Vector3 v3 = (Camera.main.transform.forward).normalized;
            GameObject frag = Instantiate(platformFrag, hit.point + v3 * 0.5f, Quaternion.identity) as GameObject;
            foreach (Transform child in frag.transform)
            {
                if (child.gameObject.GetComponent<Renderer>() && hit.collider.gameObject.GetComponent<Renderer>())
                    child.gameObject.GetComponent<Renderer>().material = hit.collider.gameObject.GetComponent<Renderer>().material;
            }

            //GameObject crackObj = Instantiate (crackPrefab, hit.point, Quaternion.identity) as GameObject;
            //crackObj.transform.position = hit.point;

        }

        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * 1.0f, Camera.main.transform.forward, out hit, 500f, layerMaskPlayer))
        {
            if (hit.collider.gameObject.tag == "Fragment")
            {
                GameObject frag = Instantiate(platformFrag, hit.point, Quaternion.identity) as GameObject;
                foreach (Transform child in frag.transform)
                {
                    if (child.gameObject.GetComponent<Renderer>() && hit.collider.gameObject.GetComponent<Renderer>())
                        child.gameObject.GetComponent<Renderer>().material = hit.collider.gameObject.GetComponent<Renderer>().material;
                }
                Destroy(hit.collider.gameObject);
            }
        }
	}
}
