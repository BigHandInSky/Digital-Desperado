using UnityEngine;
using System.Collections;

public class TargetFragmentation : MonoBehaviour {

	[SerializeField] private GameObject cubeFrag;
    [SerializeField] private Material ShotMat;

	public void vExplode()
	{
        GameData.Instance.TargetShot();
		Instantiate (cubeFrag, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}
