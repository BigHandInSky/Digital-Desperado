using UnityEngine;
using System.Collections;

public class TargetFragmentation : MonoBehaviour {

	public GameObject cubeFrag;

	public void vExplode()
	{
		Instantiate (cubeFrag, transform.position, transform.rotation);
		Destroy (this);
	}
}
