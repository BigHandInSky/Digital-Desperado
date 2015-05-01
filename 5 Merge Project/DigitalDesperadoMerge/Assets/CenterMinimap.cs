using UnityEngine;
using System.Collections;

public class CenterMinimap : MonoBehaviour {
	
	
	public GameObject canvasPlatform;
	public GameObject canvasTowers;
	public GameObject canvasTarget;
	public GameObject canvasEndLevel;
	public GameObject canvasPlayer;

	public float f_maxX;
	public float f_minX;
	public float f_maxZ;
	public float f_minZ;

	public float avX;
	public float avZ;

	// Update is called once per frame
	void Update () {

		f_maxX = 0;
		f_minX = 0;
		f_maxZ = 0;
		f_minZ = 0;

		foreach (Transform child in canvasPlatform.transform) 
		{
			if(child.position.x >= f_maxX)
				f_maxX = child.position.x;
			if(child.position.x <= f_minX)
				f_minX = child.position.x;
			if(child.position.z >= f_maxZ)
				f_maxZ = child.position.z;
			if(child.position.z <= f_minZ)
				f_minZ = child.position.z;
		}

		foreach (Transform child in canvasTowers.transform) {
			if(child.position.x >= f_maxX)
				f_maxX = child.position.x;
			if(child.position.x <= f_minX)
				f_minX = child.position.x;
			if(child.position.z >= f_maxZ)
				f_maxZ = child.position.z;
			if(child.position.z <= f_minZ)
				f_minZ = child.position.z;
		}


		foreach (Transform child in canvasTarget.transform) {
			if(child.position.x >= f_maxX)
				f_maxX = child.position.x;
			if(child.position.x <= f_minX)
				f_minX = child.position.x;
			if(child.position.z >= f_maxZ)
				f_maxZ = child.position.z;
			if(child.position.z <= f_minZ)
				f_minZ = child.position.z;
		}

		foreach (Transform child in canvasEndLevel.transform) {
			if(child.position.x >= f_maxX)
				f_maxX = child.position.x;
			if(child.position.x <= f_minX)
				f_minX = child.position.x;
			if(child.position.z >= f_maxZ)
				f_maxZ = child.position.z;
			if(child.position.z <= f_minZ)
				f_minZ = child.position.z;
		}

		foreach (Transform child in canvasPlayer.transform) {
			if(child.position.x >= f_maxX)
				f_maxX = child.position.x;
			if(child.position.x <= f_minX)
				f_minX = child.position.x;
			if(child.position.z >= f_maxZ)
				f_maxZ = child.position.z;
			if(child.position.z <= f_minZ)
				f_minZ = child.position.z;
		}

		avX = (f_maxX + f_minX) / 2 * 10;
		avZ = (f_maxZ + f_minZ) / 2 * 10;

		canvasPlatform.GetComponent<RectTransform>().localPosition = new Vector3(avX, avZ, 0);
		canvasTowers.GetComponent<RectTransform>().localPosition = new Vector3(avX, avZ, 0);
		canvasTarget.GetComponent<RectTransform>().localPosition = new Vector3(avX, avZ, 0);
		canvasEndLevel.GetComponent<RectTransform>().localPosition = new Vector3(avX, avZ, 0);
		canvasPlayer.GetComponent<RectTransform>().localPosition = new Vector3(avX, avZ, 0);

		//Debug.Log (transform.position + "  " + transform.GetComponent<RectTransform>().position + "  " + transform.GetComponent<RectTransform>().localPosition);


	}
}
