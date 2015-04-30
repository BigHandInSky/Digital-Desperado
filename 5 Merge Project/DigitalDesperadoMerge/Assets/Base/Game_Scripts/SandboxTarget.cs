using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SandboxTarget : MonoBehaviour 
{
    private float m_fCountdown = 1.5f;

    public void vRespawn()
    {
        StartCoroutine(Counter());
    }

    IEnumerator Counter()
    {
        yield return new WaitForSeconds(m_fCountdown);

        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetComponent<TargetFragmentation>().ResetPosition();
    }
}
