using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioManagerMusic : MonoBehaviour 
{
    private static AudioManagerMusic m_DataInstance;
    public static AudioManagerMusic Instance
    {
        get
        {
            if (!m_DataInstance) { m_DataInstance = FindObjectOfType<AudioManagerMusic>(); }
            return m_DataInstance;
        }
    }
    private AudioSource Source;

    void Awake()
    {
        m_DataInstance = this;
        Source = gameObject.GetComponent<AudioSource>();
        SetMusic(MusicType.Menus);
    }
    
    public enum MusicType
    {
        Menus,
        Loading,
        InGame,
        EndGame
    }

    [SerializeField] private List<AudioClip> MenuClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> LoadingClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> InGameClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> EndGameClips = new List<AudioClip>();

    public void SetMusic(MusicType _type)
    {
        Source.Stop();
        StopAllCoroutines();

        if (!GameSettings.Instance.Music)
            return;
        
        StartCoroutine(PlayMusic(_type, 0.5f));
    }

    IEnumerator PlayMusic(MusicType _type, float _length)
    {
        yield return new WaitForSeconds(_length);
        Source.Stop();

        int _Tune = 0;

        switch (_type)
        {
            case MusicType.Menus:
                _Tune = Random.Range(0, MenuClips.Count);
                Source.PlayOneShot(MenuClips[_Tune]);
                StartCoroutine(PlayMusic(MusicType.Menus, MenuClips[_Tune].length));
                break;

            case MusicType.Loading:
                _Tune = Random.Range(0, LoadingClips.Count);
                Source.PlayOneShot(LoadingClips[_Tune]);
                StartCoroutine(PlayMusic(MusicType.Loading, LoadingClips[_Tune].length));
                break;

            case MusicType.InGame:
                _Tune = Random.Range(0, InGameClips.Count);
                Source.PlayOneShot(InGameClips[_Tune]);
                StartCoroutine(PlayMusic(MusicType.InGame, InGameClips[_Tune].length));
                break;

            case MusicType.EndGame:
                _Tune = Random.Range(0, EndGameClips.Count);
                Source.PlayOneShot(EndGameClips[_Tune]);
                StartCoroutine(PlayMusic(MusicType.EndGame, EndGameClips[_Tune].length));
                break;
        }
    }
}
