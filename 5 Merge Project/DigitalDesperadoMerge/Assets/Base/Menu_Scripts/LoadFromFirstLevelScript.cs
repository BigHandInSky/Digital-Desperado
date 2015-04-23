using UnityEngine;
using System.Collections;

public class LoadFromFirstLevelScript : MonoBehaviour {

	void Start () 
    {
        StartCoroutine(Wait());
	}

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        Application.LoadLevelAsync("Main");
    }
}
