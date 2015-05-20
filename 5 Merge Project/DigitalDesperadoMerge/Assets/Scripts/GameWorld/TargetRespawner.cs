using UnityEngine;
using System.Collections;

public class TargetRespawner : MonoBehaviour 
{
    [SerializeField]
    private GameObject Child;

    public void vRespawn()
    {
        Child.SetActive(true);
        Child.GetComponent<TargetFragmentation>().ResetPosition();
    }
}
