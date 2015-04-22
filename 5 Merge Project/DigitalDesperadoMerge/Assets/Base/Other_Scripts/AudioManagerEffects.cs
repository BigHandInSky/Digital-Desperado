using UnityEngine;
using System.Collections;

public class AudioManagerEffects : MonoBehaviour {

    private static AudioManagerEffects m_DataInstance;
    public static AudioManagerEffects Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<AudioManagerEffects>(); }
            return m_DataInstance;
        }
    }

    void Awake()
    {
        m_DataInstance = this;
    }

    [SerializeField] private GameObject ObjToSpawn;
    [SerializeField] private System.Collections.Generic.List<AudioClip> Clips = new System.Collections.Generic.List<AudioClip>();

    public enum Effects
    {
        BtnClick = 0,
        Error = 1,
        NewEffect = 2
    }

    public void PlaySound(Effects _type)
    {
        SelectClip(_type);
    }
    public void PlaySound(int _type)
    {
        SelectClip((Effects)_type);
    }

    private void SelectClip(Effects _clipType)
    {
        int _selected = 0;

        switch (_clipType)
        {
            case Effects.BtnClick:
                _selected = Random.Range(0,3);
                break;
        }

        CreateObj(Clips[_selected]);
    }

    private void CreateObj(AudioClip _clipToPlay)
    {
        if (GameSettings.Instance == null) 
        {
            Debug.LogError("AudioManager: No Game Settings");
            return;
        }
        else if (!GameSettings.Instance.Effects)
            return;

        GameObject _clone = Instantiate(ObjToSpawn);
        _clone.transform.parent = gameObject.transform;
        _clone.transform.localPosition = Vector3.zero;
        _clone.GetComponent<AudioObj>().Setup(_clipToPlay);
    }
}
