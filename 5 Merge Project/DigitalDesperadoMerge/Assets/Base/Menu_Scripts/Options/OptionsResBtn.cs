using UnityEngine;
using System.Collections;

public class OptionsResBtn : MonoBehaviour {

    [SerializeField] private float Width = 800;
    [SerializeField] private float Height = 600;

    public void SetResolution()
    {
        Debug.Log("SetResolution");
    }
}
