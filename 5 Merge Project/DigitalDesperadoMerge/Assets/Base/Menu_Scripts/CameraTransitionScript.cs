using UnityEngine;
using System.Collections;

//script that handles rotation and translating the camera to a point/rotation given
//works via the vTransition function that is called by other objects in the world through CallForCamTransition.cs
public class CameraTransitionScript : MonoBehaviour {

	//point to move to on scene load from cam starting point
	public GameObject startSettings;

    public int iTransToMenuNum = 0;

	//variables to hold point and rotation to go to
	Vector3 v3TransitionPoint;
	Quaternion qRotation;

	//speed that the camera should do things
	public float fTransitionSpeed = 1.0f;
	public float fRotationSpeed = 1.0f;

	void Start()
	{
		//if a point has been given in the inspector
		if(startSettings)
		{
			//start with point values given via the inspector
			v3TransitionPoint = startSettings.transform.position;
			qRotation = startSettings.transform.rotation;
		}
		else
		{
			//start with camera settings, rather than 0,0,0 / 0,0,0
			v3TransitionPoint = gameObject.transform.position;
			qRotation = gameObject.transform.rotation;

			print ("Camera with transition script has an unset startSettings GameObject : " + gameObject);
		}

	}

	void Update()
	{
		//if self is not the same as position, use moveTowards to do so smoothly
		if(gameObject.transform.position != v3TransitionPoint)
		{
			float traStep = fTransitionSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, v3TransitionPoint, traStep);
		}
		
		//if self is not the same as rotation, use rotateTowards to do so smoothly
		if(gameObject.transform.rotation != qRotation)
		{
			float rotStep = fRotationSpeed * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, qRotation, rotStep);
		}
	}

	//function which when called sets variables to move to
	public void vTransition(Vector3 _point, Quaternion _rotation)
	{
		v3TransitionPoint = _point;
		qRotation = _rotation;
	}
}