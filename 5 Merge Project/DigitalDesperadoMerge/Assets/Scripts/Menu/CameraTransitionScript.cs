using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//script that handles rotation and translating the camera to a point/rotation given
//works via the vTransition function that is called by other objects in the world through CallForCamTransition.cs
public class CameraTransitionScript : MonoBehaviour {

    [SerializeField] private List<GameObject> Canvii = new List<GameObject>();
    [SerializeField] private MenuCanvasController StartCanvas;

	//point to move to on scene load from cam starting point
	public GameObject startSettings;
    public GameObject transitionToLevelsSide;
    public GameObject LevelLoadStartPos;

    public int iTransToMenuNum = 0;
    private int iPreviousNum = 0;

	//variables to hold point and rotation to go to
	Vector3 v3TransitionPoint;
	Quaternion qRotation;

	//speed that the camera should do things
    private float fTransitionSpeed = 7.03f;
    private float fRotationSpeed = 36.0f;
    
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

        int _num = 0;

        if (iTransToMenuNum == iPreviousNum)
            return;

        foreach(GameObject _obj in Canvii)
        {
            if (iTransToMenuNum == _num)
            {
                _obj.SetActive(true);
                StartCoroutine(DoAction(_num));
            }
            else if (_num != iPreviousNum)
            {
                _obj.SetActive(false);
            }
            _num++;
        }

        iPreviousNum = iTransToMenuNum;
	}

    public void vHalfPointTransition(Vector3 _point, Quaternion _rotation)
    {
        v3TransitionPoint = _point;
        qRotation = _rotation;
    }

    public void FirstLoad()
    {
        vTransition(startSettings.transform.position, startSettings.transform.rotation);
    }
    public void GotoLevelSide()
    {
        iTransToMenuNum = 1;

        transform.position = LevelLoadStartPos.transform.position;
        transform.rotation = LevelLoadStartPos.transform.rotation;

        vTransition(transitionToLevelsSide.transform.position, transitionToLevelsSide.transform.rotation);
    }

    IEnumerator DoAction(int _dir)
    {
        yield return new WaitForSeconds(0.01f);

        if (_dir == 0)
        {
            //menu
            StartCanvas.SetInteractables(true);
        }
        else
        {
            StartCanvas.SetInteractables(false);
        }
        
        if (_dir == 1)
        {
            //levels
            LoadedLevels.Instance.vUpdateData();
        }
        else if(_dir == 2)
        {
            //options
            OptionsSetter.Instance.SetOptions();
        }
        else if(_dir == 4)
        {
            //controls
            ControlsSetter.Instance.SetControls();
        }
    }
}