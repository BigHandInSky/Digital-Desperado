using UnityEngine;
using System.Collections;

public class TransitionPointScript : MonoBehaviour {

    public GameObject goMenu;
    public GameObject goOther;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CameraTransitionScript>().iTransToMenuNum == 0)
        {
            other.gameObject.GetComponent<CameraTransitionScript>().vHalfPointTransition(goMenu.transform.position, goMenu.transform.rotation);
        }
        else
        {
            other.gameObject.GetComponent<CameraTransitionScript>().vHalfPointTransition(goOther.transform.position, goOther.transform.rotation);
        }
    }
}
