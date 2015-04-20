using UnityEngine;
using System.Collections;

public class LoadFromFirstLevelScript : MonoBehaviour {

	void Start () 
    {
        StartCoroutine(Wait());
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        Application.LoadLevel("Main");
    }
}
