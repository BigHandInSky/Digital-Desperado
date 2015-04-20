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

                DoAction(0);
            }
            else
            {
                other.gameObject.GetComponent<CameraTransitionScript>().vTransition(goOther.transform.position, goOther.transform.rotation);
                goOther.SetActive(true);
                goMenu.SetActive(false);

                DoAction(1);
            }
        }
    }

    void DoAction(int _dir)
    {
        if(Type == TransitionType.ToLevel && _dir == 1)
        {
            LoadedLevels.Instance.vUpdateData();

            if (MenuLoadLevelsFromXML.Instance.Urls.Count < 1)
                FolderUIControl.Instance.OpenFolderUI();
        }
    }
}
