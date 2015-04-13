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

    IEnumerator Rotate()
    {
        while(true)
        {
            Vector3 _screenMid = new Vector3(Screen.width/2, Screen.height/2, 0); 
            Vector3 _targPos = Camera.main.WorldToScreenPoint(GameData.Instance.trEndLvlTow.position);

            float tarAngle = ( Mathf.Atan2( _targPos.x - _screenMid.x, Screen.height - _targPos.y - _screenMid.y ) * Mathf.Rad2Deg ); 
            //tarAngle -= 90f;
            /*
            if (tarAngle < 0) 
            {
                tarAngle +=360;
            }*/

            Vector3 _direction = GameData.Instance.trCamera.position - ( Camera.main.WorldToScreenPoint(GameData.Instance.trEndLvlTow.position));
            Vector3 _forw = GameData.Instance.trCamera.forward;
            float _angle = Vector3.Angle(_direction,_forw);

            //Debug.Log("angle " + _angle + " | tarAngle " + tarAngle);

            if(_angle > 90)
            {
                transform.localRotation = Quaternion.Euler(0,0, -tarAngle);
            } 
            else
            {
                transform.localRotation = Quaternion.Euler(0,0, tarAngle);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}