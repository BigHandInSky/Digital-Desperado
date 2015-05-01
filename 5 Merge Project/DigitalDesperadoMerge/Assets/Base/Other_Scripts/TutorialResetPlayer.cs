using UnityEngine;
using System.Collections;

public class TutorialResetPlayer : MonoBehaviour {

    [SerializeField]
    private Transform tResetPosition;
    [SerializeField]
    private string sPlayerTag;

    void OnColliderEnter(Collision other)
    {
        //Debug.Log("triggered");

        if (other.gameObject.tag == sPlayerTag)
        {
            other.gameObject.transform.position = tResetPosition.position;
            other.gameObject.GetComponent<PlayerMovementScript>().Reset();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggered");

        if (other.gameObject.tag == sPlayerTag)
        {
            other.gameObject.transform.position = tResetPosition.position;
            other.gameObject.GetComponent<PlayerMovementScript>().Reset();
        }
    }
}
