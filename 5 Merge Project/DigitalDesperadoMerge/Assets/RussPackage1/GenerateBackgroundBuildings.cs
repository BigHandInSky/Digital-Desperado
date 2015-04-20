using UnityEngine;
using System.Collections;

public class GenerateBackgroundBuildings : MonoBehaviour {


	public GameObject objCube1;
	public GameObject objCube2;
	public GameObject objCube3;

	private float xMax;
	private float xMin;
	private float zMax;
	private float zMin;

	private float centerX;
	private float centerZ;
	private float radiusX;
	private float radiusZ;

	// Use this for initialization
	void Start ()
	{
		foreach(Transform child in transform)
		{
			if(child.position.x >= xMax)
				xMax = child.position.x;
			if(child.position.x <= xMin)
				xMin = child.position.x;

			if(child.position.z >= zMax)
				zMax = child.position.z;
			if(child.position.z <= zMin)
				zMin = child.position.z;
		}

		centerX = (xMin + xMax) / 2;
		centerZ = (zMin + zMax) / 2;
		radiusX = Mathf.Abs(xMax - centerX) * 10;
		radiusZ = Mathf.Abs(zMax - centerZ) * 10;

		for (int i = 0; i < 6; i++)
		{
			float newPositionX = centerX + Mathf.Sin(360/6 * i * 3.14f / 180) * radiusX * (Random.value + 1);
			float newPositionZ = centerZ + Mathf.Cos(360/6 * i * 3.14f / 180) * radiusZ * (Random.value + 1);

			Instantiate (objCube1, new Vector3(newPositionX, 0, newPositionZ), Quaternion.identity);
		}

		for (int i = 0; i < 10; i++)
		{
			float newPositionX = centerX + Mathf.Sin(360/10 * i * 3.14f / 180) * radiusX * 2 * (Random.value + 1);
			float newPositionZ = centerZ + Mathf.Cos(360/10 * i * 3.14f / 180) * radiusZ * 2 * (Random.value + 1);
			
			Instantiate (objCube2, new Vector3(newPositionX, 0, newPositionZ), Quaternion.identity);
		}

	}

}
