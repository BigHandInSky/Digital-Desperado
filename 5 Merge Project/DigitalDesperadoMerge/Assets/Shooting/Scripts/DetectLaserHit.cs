using UnityEngine;
using System.Collections;

public class DetectLaserHit : MonoBehaviour {

	public GameObject platformFrag;
	private float timer = 0;

	void Awake(){
		Physics.IgnoreLayerCollision(0, 11,true);
	}
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 2f)
			Destroy (gameObject);
	}

	/*void OnTriggerStay(Collider other) {
		if (other.gameObject.layer == 0 && gameObject.layer == 0 ) {
			Vector3 v3 = (transform.position - Camera.main.transform.position).normalized;
			GameObject frag = Instantiate (platformFrag, transform.position - v3 * 2, Quaternion.identity) as GameObject;
			foreach(Transform child in frag.transform)
			{
				if(child.gameObject.GetComponent<Renderer>() && other.GetComponent<Renderer>())
					child.gameObject.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
			}
			Destroy(gameObject);
		}
	}*/

	void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.GetComponent<TargetFragmentation> ()) {
			other.gameObject.GetComponent<TargetFragmentation> ().vExplode ();
			Destroy (gameObject);
		}
	}
}
