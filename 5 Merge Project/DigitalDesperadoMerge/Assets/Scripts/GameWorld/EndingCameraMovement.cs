using UnityEngine;
using System.Collections;

public class EndingCameraMovement : MonoBehaviour {
	
	private Vector3 [] points = new Vector3[12];
	private int index = 0;
	private bool state = true;

	public GameEndLvl gameEndLv;
	public GameObject objCamera;
	public Transform levelRoot;
	private Vector3 v3Center;

	// Use this for initialization
	void Start () {

		float xMax = 0;
		float xMin = 0;
		float zMax = 0;
		float zMin = 0;

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

		v3Center = new Vector3 ((xMin + xMax) / 2, GameObject.FindGameObjectWithTag("Player").transform.position.y, (zMin + zMax) / 2);
		objCamera.SetActive(false);
		//int radius = 65;
		int radius = 85;
		for(int i = 0; i < 360; i+=30)
		{
			points[i/30] = new Vector3(radius * Mathf.Sin(i * 3.14f/180),40, radius * Mathf.Cos(i * 3.14f/180));
		}

		transform.position = points [0];
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(!gameEndLv.bCanEnd)
		{
			objCamera.SetActive(true);

			if(state)
			{
				Vector3 moveTo = points[index];
				Vector3 v3 = (moveTo - transform.position).normalized;
                transform.position = transform.position + (v3 * 2f) * Time.deltaTime;
				
				transform.LookAt(v3Center);
				if((transform.position - points[index]).magnitude < 50)
					state = false;
			}else
			{
				state = true;
				index++;
				if(index > 11)
					index = 0;
			}
		}
		else
		{
			objCamera.SetActive(false);
		}
	}
	
}
