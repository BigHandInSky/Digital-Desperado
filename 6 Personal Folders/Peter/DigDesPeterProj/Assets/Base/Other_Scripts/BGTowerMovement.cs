using UnityEngine;
using System.Collections;

public class BGTowerMovement : MonoBehaviour {

    [SerializeField] private float fTowerSetting = 1.0f;
    
    [SerializeField] private float fSinePower = 1f;
    [SerializeField] private float fSineSpeed = 0.3f;
    [SerializeField] private float fRotateMaxSpeed = 4f;
    private Vector3 OrigPos;

    void Start()
    {
        OrigPos = gameObject.transform.position;
        StartCoroutine("Sine");
    }
    IEnumerator Sine()
    {
        float _Pow = fSinePower * fTowerSetting;
        float _Spd = fSineSpeed * fTowerSetting;
        float _Rot = Random.Range(0f, fRotateMaxSpeed);

        float _RandStart = Random.Range(0f, 15f);
        yield return new WaitForSeconds(_RandStart);

        _RandStart = 0f;
        while (true)
        {
            _RandStart += Time.deltaTime;
            gameObject.transform.position = OrigPos + Vector3.up * Mathf.Sin(_RandStart * _Spd) * _Pow;
            gameObject.transform.Rotate(0f, _Rot * Time.deltaTime, 0f);
            yield return new WaitForEndOfFrame();
        }
    }
}
