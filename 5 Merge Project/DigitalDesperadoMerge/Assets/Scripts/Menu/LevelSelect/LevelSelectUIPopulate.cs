using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectUIPopulate : MonoBehaviour
{
    private float Spacing = 7f;
    private float EntryHeight = 30f;

    [SerializeField] private GameObject ObjToSpawn;
    [SerializeField] private RectTransform ScrollContent;
    [SerializeField] private List<GameObject> Entries;

    public void Setup()
    {        
        ReSizeContentsObj(MenuLoadLevelsFromXML.Instance.Names.Count);

        int _val = 0;
        foreach (string _entry in MenuLoadLevelsFromXML.Instance.Names)
        {
            FillList(MenuLoadLevelsFromXML.Instance.Names[_val], _val);
            _val++;
        }
    }

    private void FillList(string _name, int _arr)
    {
        GameObject _clone = Instantiate(ObjToSpawn);
        _clone.GetComponent<RectTransform>().SetParent(ScrollContent);
        _clone.GetComponent<RectTransform>().localScale = Vector2.one;
        _clone.GetComponent<LevelSelectEntry>().SetupEntry(this.gameObject, _name, _arr);

        Entries.Add(_clone);
    }

    private void ReSizeContentsObj(int _size)
    {
        Debug.Log(_size);

        float _x = ScrollContent.sizeDelta.x;
        float _y = (EntryHeight * _size) + (Spacing * _size);

        ScrollContent.sizeDelta = new Vector2(_x, _y);

        Vector2 _NewPos = new Vector2(0f, -((ObjToSpawn.GetComponent<RectTransform>().sizeDelta.y * _size) + (Spacing * _size)));
        ScrollContent.localPosition = _NewPos;
    }

    public void Clear()
    {
        foreach (GameObject _obj in Entries)
            _obj.GetComponent<LevelSelectEntry>().Delete();

        Entries.Clear();
    }
}
