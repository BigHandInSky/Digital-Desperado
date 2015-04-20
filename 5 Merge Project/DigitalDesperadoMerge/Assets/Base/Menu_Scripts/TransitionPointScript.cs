using UnityEngine;
using System.Collections;

public class TransitionPointScript : MonoBehaviour {

    public GameObject goMenu;
    public GameObject goOther;

    public enum TransitionType
    {
        Norm,
        ToLevel
    }
    public TransitionType Type = TransitionType.Norm;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CameraTransitionScript>())
        {
            if (other.gameObject.GetComponent<CameraTransitionScript>().iTransToMenuNum == 0)
            {
                other.gameObject.GetComponent<CameraTransitionScript>().vTransition(goMenu.transform.position, goMenu.transform.rotation);
                goMenu.SetActive(true);
                goOther.SetActive(false);
            }
            else
            {
                other.gameObject.GetComponent<CameraTransitionScript>().vTransition(goOther.transform.position, goOther.transform.rotation);
                goOther.SetActive(true);
                goMenu.SetActive(false);
            }
            DoAction();
        }
    }

    void DoAction()
    {
        if(Type == TransitionType.ToLevel)
        {
            LoadedLevels.Instance.vUpdateData();
        }
    }
}
