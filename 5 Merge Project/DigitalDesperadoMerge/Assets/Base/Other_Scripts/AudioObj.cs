using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioObj : MonoBehaviour {

    public void Setup(AudioClip _clip)
    {
        gameObject.GetComponent<AudioSource>().clip = _clip;
        gameObject.GetComponent<AudioSource>().volume = GameSettings.Instance.Volume;
        StartCoroutine(Delete(_clip.length));
    }

    IEnumerator Delete(float _length)
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(_length);

        DeleteAudioObj();
    }
    
    public void DeleteAudioObj()
    {
        Destroy(this.gameObject);
    }
}
