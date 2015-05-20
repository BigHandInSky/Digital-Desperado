using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuCanvasController : MonoBehaviour 
{
    [SerializeField] private List<GameObject> Interactables = new List<GameObject>();

    public void SetInteractables(bool _value)
    {
        foreach(GameObject _obj in Interactables)
        {
            if (_obj.GetComponent<Button>())
                _obj.GetComponent<Button>().interactable = _value;
        }
    }

}
