using UnityEngine;
using System.Collections;

public class OptionsResBtn : MonoBehaviour {

    [SerializeField] private int Width = 800;
    [SerializeField] private int Height = 600;

    public void SetResolution()
    {
        GameSettings.Instance.SetResolution(Width,Height);
    }
}
