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
	
	void OnTriggerStay(Collider other) {
		if (other.gameObject.GetComponent<TargetFragmentation> ()) {
			other.gameObject.GetComponent<TargetFragmentation> ().vExplode ();
			//Destroy (gameObject);
		}
	}
	
}
