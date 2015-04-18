using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioObj : MonoBehaviour {

    public void Setup(AudioClip _clip)
    {
        gameObject.GetComponent<AudioSource>().clip = _clip;
        StartCoroutine(Delete(_clip.length));
    }

    IEnumerator Delete(float _length)
    {
        yield return new WaitForSeconds(_length);

        Destroy(this.gameObject);
    }
    
    public void DeleteAudioObj()
    {
        Destroy(this.gameObject);
    }
}
