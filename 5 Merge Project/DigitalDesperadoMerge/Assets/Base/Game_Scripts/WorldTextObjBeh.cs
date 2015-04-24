using UnityEngine;
using System.Collections;

public class WorldTextObjBeh : MonoBehaviour {

    [SerializeField] private float fSinePower = 1f;
    [SerializeField] private float fSineSpeed = 0.3f;
    private Vector3 OrigPos;

    void Start()
    {
        OrigPos = gameObject.transform.position;
        StartCoroutine("Sine");
    }
    IEnumerator Sine()
    {
        float _RandStart = Random.Range(1f, 5f);
        while (true)
        {
            _RandStart += Time.deltaTime;
            gameObject.transform.position = OrigPos + Vector3.up * Mathf.Sin(_RandStart * fSineSpeed) * fSinePower;
            yield return new WaitForEndOfFrame();
        }
    }
}
