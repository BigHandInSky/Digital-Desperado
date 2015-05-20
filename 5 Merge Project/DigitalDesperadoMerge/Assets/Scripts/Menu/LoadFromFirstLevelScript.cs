using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadFromFirstLevelScript : MonoBehaviour {

    [SerializeField]
    private Slider LoadingBar;
    private int iloadProgress = 0;

    private AsyncOperation async;

	void Start () 
    {
        StartCoroutine(Load());
	}

    IEnumerator Load()
    {
        yield return new WaitForEndOfFrame();
        async = Application.LoadLevelAsync("Main");

        while (!async.isDone)
        {
            iloadProgress = (int)(async.progress * 100f);
            LoadingBar.value = iloadProgress;

            yield return null;
        }
    }
}
