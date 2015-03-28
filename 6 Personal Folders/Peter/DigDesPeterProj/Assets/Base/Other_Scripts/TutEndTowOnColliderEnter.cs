using UnityEngine;
using System.Collections;

public class TutEndTowOnColliderEnter : MonoBehaviour {

    [SerializeField]
    private string sNextTutScene;
    [SerializeField]
    private string sPlayerTag;

    void OnColliderEnter(Collision other)
    {
        Debug.Log("triggered");

        if (other.gameObject.tag == sPlayerTag)
        {
            Application.LoadLevel(sNextTutScene);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");

        if (other.gameObject.tag == sPlayerTag)
        {
            Application.LoadLevel(sNextTutScene);
        }
    }
}
