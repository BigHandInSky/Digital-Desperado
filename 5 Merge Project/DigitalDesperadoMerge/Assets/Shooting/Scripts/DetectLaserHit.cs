using UnityEngine;
using System.Collections;

public class DetectLaserHit : MonoBehaviour {

	private float timer = 0;
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 2f)
			Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.GetComponent<TargetFragmentation>())
			other.gameObject.GetComponent<TargetFragmentation> ().vExplode ();
	}
}
