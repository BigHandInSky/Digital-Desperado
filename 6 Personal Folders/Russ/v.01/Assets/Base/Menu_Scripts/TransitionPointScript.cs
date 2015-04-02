using UnityEngine;
using System.Collections;

public class TransitionPointScript : MonoBehaviour {

    public GameObject goMenu;
    public GameObject goOther;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CameraTransitionScript>())
        {
            if (other.gameObject.GetComponent<CameraTransitionScript>().iTransToMenuNum == 0)
            {
                other.gameObject.GetComponent<CameraTransitionScript>().vTransition(goMenu.transform.position, goMenu.transform.rotation);
            }
            else
            {
                other.gameObject.GetComponent<CameraTransitionScript>().vTransition(goOther.transform.position, goOther.transform.rotation);
            }
        }
    }
}
