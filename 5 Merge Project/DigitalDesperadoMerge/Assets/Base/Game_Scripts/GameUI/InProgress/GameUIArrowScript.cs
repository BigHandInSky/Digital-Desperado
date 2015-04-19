using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIArrowScript : MonoBehaviour {

    [SerializeField]
    Image imImageToRotate;
    
    void Awake()
    {
        StartCoroutine("Rotate");
    }
    public void Reset()
    {
        StartCoroutine("Rotate");
    }

    IEnumerator Rotate()
    {
        while(true)
        {
            // Vector between the player and the level end
            Vector3 _direction = GameData.Instance.trEndLvlTow.position - GameData.Instance.trCamera.position;
            // Forward direction of the player
            Vector3 _forw = GameData.Instance.trCamera.forward;

            // Angle between player view and level end (Abs value)
            float _angle = Vector3.Angle(_forw, _direction);
            // Cross Vector of angle between player view and level end
            Vector3 _crossVector = Vector3.Cross(_forw, _direction);

            // If the cross product angle is less than 0, angle is positive
            if (_crossVector.y < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, _angle);
            }
            else // Otherwise, angle is negative
            {
                transform.localRotation = Quaternion.Euler(0, 0, -_angle);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}