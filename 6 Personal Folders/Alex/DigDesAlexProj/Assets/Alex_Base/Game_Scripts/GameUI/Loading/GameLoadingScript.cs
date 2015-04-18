using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameLoadingScript : MonoBehaviour {

    [SerializeField] private Image ImgObjectToSet;
    [SerializeField] private List<Sprite> LoadingImgAnims;
    [SerializeField] private float fImgSwitchInterval = 0.05f;

    public int debug = 30;

    //object to get the all clear on loading done
    [SerializeField] private GameObject ObjToActivateWhenComplete;
    [SerializeField] private GameObject ObjToDeActivateWhenComplete;

    void Start()
    {
        StartCoroutine("Switch");
    }

    IEnumerator Switch()
    {
        int _Count = 0;

        while (debug > 0)
        {
            debug--;

            if (_Count < LoadingImgAnims.Count - 1)
                _Count++;
            else
                _Count = 0;

            ImgObjectToSet.sprite = LoadingImgAnims[_Count];

            yield return new WaitForSeconds(fImgSwitchInterval);
        }

        ObjToActivateWhenComplete.SetActive(true);
        ObjToDeActivateWhenComplete.SetActive(false);
    }
}
