using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	public Vector3 V3startPosition;
	public Vector3 V3endPosition;
	public float fSize = 3;

	private LineRenderer lineRenderer;

	void Start () {
		//Get and set the LineRenderer component
		lineRenderer = transform.GetComponent<LineRenderer> ();
		lineRenderer.SetWidth (fSize,fSize);
	}
	
	// Update is called once per frame
	void Update () {

		//Find the forward vector
		Vector3 V3fowardVector = V3endPosition - V3startPosition;
		V3fowardVector = V3fowardVector.normalized * 20;
		V3startPosition = V3startPosition + V3fowardVector * 2f * Time.deltaTime;
		V3endPosition = V3endPosition + V3fowardVector * Time.deltaTime;

		//Set the new position
		lineRenderer.SetPosition (0, V3startPosition);
		lineRenderer.SetPosition (1, V3endPosition);

		fSize -= 0.02f;
		lineRenderer.SetWidth (fSize, fSize);
		if(fSize <= 0)
			Destroy (gameObject);
	}
}
