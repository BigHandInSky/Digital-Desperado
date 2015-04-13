using UnityEngine;
using System.Collections;

public class MenuCubeDoShit : MonoBehaviour {

    [SerializeField] private float fSinePower = 0.1f;
    [SerializeField] private float fSineSpeed = 0.2f;
    private Vector3 OrigPos;

    void Start()
    {
        OrigPos = gameObject.transform.position;
        StartCoroutine("Sine", Vector3.left);
    }

    public void SetAxis(Vector3 _axisToMove)
    {
        StopAllCoroutines();
        StartCoroutine(Sine(_axisToMove));
    }

    IEnumerator Sine(Vector3 _axis)
    {
        while(true)
        {
            gameObject.transform.position = OrigPos + _axis * Mathf.Sin(Time.time * fSineSpeed) * fSinePower;
            yield return new WaitForEndOfFrame();
        }
    }
}
