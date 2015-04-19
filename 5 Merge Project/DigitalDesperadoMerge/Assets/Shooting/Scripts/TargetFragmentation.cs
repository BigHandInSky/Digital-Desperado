using UnityEngine;
using System.Collections;

public class TargetFragmentation : MonoBehaviour {

	[SerializeField] private GameObject cubeFrag;
    [SerializeField] private Material ShotMat;
    private Vector3 StartPos;
    private Quaternion StartRot;

    void Start()
    {
        StartPos = gameObject.transform.position;
        StartRot = gameObject.transform.rotation;
    }

    public void ResetPosition()
    {
        gameObject.transform.position = StartPos;
        gameObject.transform.rotation = StartRot;
    }

	public void vExplode()
	{
        GameData.Instance.TargetShot();
        this.gameObject.SetActive(false);
        Instantiate(cubeFrag, transform.position, transform.rotation);
	}
}
