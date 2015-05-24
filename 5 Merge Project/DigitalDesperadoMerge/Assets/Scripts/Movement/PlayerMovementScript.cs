using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    // Mouse rotation float
    private float fMouseVRot;

    // Movement speed
    private float fPlayerBaseSpeed = 11f;
    private int iPlayerSideSpeedPercent = 70;
    private int iPlayerBackSpeedPercent = 55;

    //speed variables
    private float fPosVarSpeed = 0f;
    private float fNegVarSpeed = 0f;
    private const float fVARIABLE_INC = 1.8f;
    private const float fVARIABLE_DEC = 0.45f;
    private const float fVARIABLE_MAX = 0.185f;
    private int iLastDir = -1;

    // Movement/Camera Enabled bools
    private static bool bIsMovementEnabled = true;
    private static bool bIsCameraEnabled = true;
    public static bool SetControls
    {
        set
        {
            bIsMovementEnabled = value;
            bIsCameraEnabled = value;
        }
    }

    // Axes float
    private float fHorizontal;
    private float fVertical;
    public Vector2 CurrSpeed { get { return new Vector2(fHorizontal, fVertical); } }

    // Mouse settings
    private float fMouseSensitivity = 5f;
    private float fMouseClampRange = 85f;

    // Jumping floats
    private float fJumpHeight = 13f;
    private float fJumpSpeedPercent = 80.0f;
    public bool bHasJumped = false;

    // Artificial gravity floats
    private float fJumpGrav = -15f;
    private float fFallGrav = -11.5f;
    //private const float fSPEED_INC = 2.4f;
    private float fVerticalVelocity;

    // Gets character controller
    public CharacterController ccPlayerController;

    // Camera object
    public GameObject GOCamera;

    private KeyCode Forward = KeyCode.W;
    private KeyCode Backward = KeyCode.S;
    private KeyCode Left = KeyCode.A;
    private KeyCode Right = KeyCode.D;
    private KeyCode Jump = KeyCode.Space;

    void Awake()
    {
        if (!GameSettings.Instance)
            return;

        fMouseSensitivity = GameSettings.Instance.Sens;
        Forward = GameSettings.Instance.Forw;
        Backward = GameSettings.Instance.Back;
        Left = GameSettings.Instance.Left;
        Right = GameSettings.Instance.Righ;
        Jump = GameSettings.Instance.Jump;
    }

    public void Reset()
    {
        fPosVarSpeed = 0f;
        fNegVarSpeed = 0f;
        fVerticalVelocity = 0f;
    }

    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName.Contains("Tutorial") || Application.loadedLevelName.Contains("Sandbox"))
            AllowControls(true, true);
        else
            AllowControls(false, true);
    }

    void Update()
    {
        if (bIsCameraEnabled)
            CameraMovement();
    }

    void FixedUpdate()
    {
        if (bIsMovementEnabled)
        {
            PlayerMovement();
            PlayerJump();
        }

        if (!Input.GetKey(Forward)
            && !Input.GetKey(Backward)
            && !Input.GetKey(Left)
            && !Input.GetKey(Right))
        {
            LastDirection(iLastDir);
        }

        //if (bIsCameraEnabled && bIsMovementEnabled)
            //HeadBobbing();
    }

    public void AllowControls(bool _MoveVal, bool _CamVal)
    {
        bIsMovementEnabled = _MoveVal;
        bIsCameraEnabled = _CamVal;
    }

    // Player camera function
    void CameraMovement()
    {
        // Speed and rotation of camera to mouse X axis
        float fMouseHRot = Input.GetAxis("Mouse X") * fMouseSensitivity;
        transform.Rotate(0, fMouseHRot, 0);

        // Speed of camera to mouse Y axis
        fMouseVRot -= Input.GetAxis("Mouse Y") * fMouseSensitivity;

        // Clamps and rotates camera to mouse Y axis
        fMouseVRot = Mathf.Clamp(fMouseVRot, -fMouseClampRange, fMouseClampRange);
        Camera.main.transform.localRotation = Quaternion.Euler(fMouseVRot, 0, 0);
    }

    // Player movement function
    void PlayerMovement()
    {
        fVertical = 0f;
        fHorizontal = 0f;
        
        if (Input.GetKey(Left))
        {
            iLastDir = 3;

            fHorizontal = -(1f + fNegVarSpeed) * fPlayerBaseSpeed / 100 * iPlayerSideSpeedPercent;

            if (fNegVarSpeed < fVARIABLE_MAX)
                fNegVarSpeed += fVARIABLE_INC * Time.deltaTime;
        }
        else if (Input.GetKey(Right))
        {
            iLastDir = 4;

            fHorizontal = (1f + fPosVarSpeed) * fPlayerBaseSpeed / 100 * iPlayerSideSpeedPercent;

            if (fPosVarSpeed < fVARIABLE_MAX)
                fPosVarSpeed += fVARIABLE_INC * Time.deltaTime;
        }

        if (Input.GetKey(Forward))
        {
            iLastDir = 1;

            fVertical = (1f + fPosVarSpeed) * fPlayerBaseSpeed;

            if (fPosVarSpeed < fVARIABLE_MAX)
                fPosVarSpeed += fVARIABLE_INC * Time.deltaTime;
        }
        else if (Input.GetKey(Backward))
        {
            iLastDir = 2;

            fVertical = -(1f + fNegVarSpeed) * fPlayerBaseSpeed;

            if (fNegVarSpeed < fVARIABLE_MAX)
                fNegVarSpeed += fVARIABLE_INC * Time.deltaTime;
        }

        // If player moving backwards
        if (fVertical <= -0.1f)
        {
            // Change speed
            fVertical = fVertical / 100 * iPlayerBackSpeedPercent;
        }

        // If player is in air change speed
        if (!ccPlayerController.isGrounded)
        {
            fVertical = fVertical / 100 * fJumpSpeedPercent;
            fHorizontal = fHorizontal / 100 * fJumpSpeedPercent;
        }

        // Applies movement to vectors
        if (fVerticalVelocity <= fFallGrav)
            fVerticalVelocity = fFallGrav;

        Vector3 v3Velocity = new Vector3(fHorizontal, fVerticalVelocity, fVertical);

        v3Velocity = transform.rotation * v3Velocity;

        // Apply movement
        ccPlayerController.Move(v3Velocity * Time.deltaTime);
    }

    private void LastDirection(int _Dir)
    {
        if (fPosVarSpeed > 0f && ccPlayerController.isGrounded)
            fPosVarSpeed -= fVARIABLE_DEC * Time.deltaTime;
        else if (fPosVarSpeed > 0f)
            fPosVarSpeed -= (fVARIABLE_DEC * 0.5f) * Time.deltaTime;

        if (fNegVarSpeed > 0f && ccPlayerController.isGrounded)
            fNegVarSpeed -= fVARIABLE_DEC * Time.deltaTime;
        else if (fNegVarSpeed > 0f)
            fNegVarSpeed -= (fVARIABLE_DEC * 0.5f) * Time.deltaTime;

        if (fPosVarSpeed < 0f)
        {
            fPosVarSpeed = 0f;
            return;
        }
        if (fNegVarSpeed < 0f)
        {
            fNegVarSpeed = 0f;
            return;
        }

        float _v = 0f;
        float _h = 0f;

        if (iLastDir == 1)
        {
            _v = fPosVarSpeed * fPlayerBaseSpeed;
        }
        else if (iLastDir == 2)
        {
            _v = -fNegVarSpeed * fPlayerBaseSpeed;
        }
        else if (iLastDir == 3)
        {
            _h = -fNegVarSpeed * fPlayerBaseSpeed;
        }
        else if (iLastDir == 4)
        {
            _h = fPosVarSpeed * fPlayerBaseSpeed;
        }

        Vector3 _Velocity = new Vector3(_h, 0f, _v);
        _Velocity = transform.rotation * _Velocity;
        ccPlayerController.Move(_Velocity * Time.deltaTime);
    }

    // Player jump button check function
    void PlayerJump()
    {
        //Debug.Log("Is player on ground: " + ccPlayerController.isGrounded);

        // Increases fall speed over time
        if (fVerticalVelocity <= 15f)
            fVerticalVelocity += fJumpGrav * Time.deltaTime;
        /*else
            fVerticalVelocity += fSPEED_INC * Time.deltaTime;*/

        // When spacebar and on floor is true
        if (Input.GetKey(Jump) && ccPlayerController.isGrounded && !bHasJumped)
        {
            // Revereses gravity for jump
            fVerticalVelocity = fJumpHeight;
            bHasJumped = true;
            AudioManagerEffects.Instance.PlaySound(AudioManagerEffects.Effects.Jump);
        }
        else if (Input.GetKey(Jump) && !ccPlayerController.isGrounded && !bHasJumped)
        {
            AudioManagerEffects.Instance.PlaySound(AudioManagerEffects.Effects.Jump);
            fVerticalVelocity = fJumpHeight;
            bHasJumped = true;
        }
        // Resets fall speed if already on ground
        else if (ccPlayerController.isGrounded)
        {
            bHasJumped = false;
            fVerticalVelocity = -1f;
        }
    }

    // Headbobbing function
    void HeadBobbing()
    {
        // Calls movement animations
        // If player is off ground or is idle
        if (!ccPlayerController.isGrounded || fVertical == 0f && fHorizontal == 0f)
        {
            SetAnimation(false, false, 0);
        }
        else if (ccPlayerController.isGrounded)
        {
            // Else player movement detected
            if (fVertical != 0f)
            {
                // And off ground
                // Forwards
                SetAnimation(true, true, 0);
            }
            else if (fHorizontal != 0f)
            {
                // Left
                if (fHorizontal <= -0.1f)
                    SetAnimation(true, false, 1);
                // Right
                else if (fHorizontal >= 0.1f)
                    SetAnimation(true, false, 2);
            }
        }
    }

    //start/stop animation
    void SetAnimation(bool _moveValue, bool _forwardValue, int _sideValue)
    {
        GOCamera.GetComponent<Animator>().SetBool("bIsMoving", _moveValue);
        GOCamera.GetComponent<Animator>().SetBool("bIsForward", _forwardValue);
        GOCamera.GetComponent<Animator>().SetInteger("iSidewaysValue", _sideValue);
    }
}
