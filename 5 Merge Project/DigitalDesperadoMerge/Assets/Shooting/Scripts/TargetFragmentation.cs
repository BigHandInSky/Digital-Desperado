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
        StartPos.y += 1f;
        StartRot = gameObject.transform.rotation;
    }

    public void ResetPosition()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        gameObject.transform.position = StartPos;
        gameObject.transform.rotation = StartRot;
    }

	public void vExplode()
	{
        //Debug.Log("vExplode");
        GameData.Instance.TargetShot();
        Instantiate(cubeFrag, transform.position, transform.rotation);

        if (GetComponentInParent<SandboxTarget>())
            GetComponentInParent<SandboxTarget>().vRespawn();

        this.gameObject.SetActive(false);
	}
}
