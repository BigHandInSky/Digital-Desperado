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
        BtnClick
    }

    public void PlaySound(Effects _type)
    {
        CreateObj(Clips[(int)_type]);
    }
    public void PlaySound(int _type)
    {
        CreateObj(Clips[_type]);
    }

    private void CreateObj(AudioClip _clipToPlay)
    {
        GameObject _clone = Instantiate(ObjToSpawn);
        _clone.GetComponent<AudioObj>().Setup(_clipToPlay);
    }
}
