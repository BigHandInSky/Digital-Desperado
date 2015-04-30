using UnityEngine;
using System.Collections;

public class FragmentAutoDelete : MonoBehaviour {

    private float m_fAutoDelete = 10f;

	void Start () 
    {
        StartCoroutine(Countdown());
	}

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(m_fAutoDelete);

        DestroyObject(this.gameObject);
    }
}
