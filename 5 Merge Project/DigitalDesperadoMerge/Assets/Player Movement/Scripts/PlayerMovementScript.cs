using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    // Mouse rotation float
    private float fMouseVRot;

    // Movement speed
    private float fPlayerBaseSpeed = 13f;
    private int iPlayerSideSpeedPercent = 70;
    private int iPlayerBackSpeedPercent = 55;

    // Movement/Camera Enabled bools
    private bool bIsMovementEnabled = true;
    private bool bIsCameraEnabled = true;

    // Axes float
    private float fHorizontal;
    private float fVertical;

    // Mouse settings
    private float fMouseSensitivity = 5;
    private float fMouseClampRange = 59;

    // Jumping floats
    private float fJumpHeight = 14f;
    private float fJumpSpeedPercent = 80.0f;
    private bool bHasJumped = false;

    // Artificial gravity floats
    private float fJumpGrav = -15.5f;
    private float fFallGrav = -11.5f;
    private float fVerticalVelocity;

    // Gets character controller
    public CharacterController ccPlayerController;

    // Camera object
    public GameObject GOCamera;

    private KeyCode ExitToMenu = KeyCode.Escape;

    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName.Contains("Tutorial"))
            AllowControls(true, true);
        else
            AllowControls(false, true);
    }

    void FixedUpdate()
    {
        if (bIsCameraEnabled)
            CameraMovement();

        if (bIsMovementEnabled)
        {
            PlayerMovement();
            PlayerJump();
        }

        //if (bIsCameraEnabled && bIsMovementEnabled)
            //HeadBobbing();

        //if (Input.GetKey(ExitToMenu))
            //Application.LoadLevel("Main");
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
        // Horizontal and vertical movement axes and speeds
        fVertical = Input.GetAxis("Vertical") * fPlayerBaseSpeed;
        fHorizontal = Input.GetAxis("Horizontal") * fPlayerBaseSpeed / 100 * iPlayerSideSpeedPercent;

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

    // Player jump button check function
    void PlayerJump()
    {
        //Debug.Log("Is player on ground: " + ccPlayerController.isGrounded);

        // Increases fall speed over time
        if (fVerticalVelocity <= 15f)
            fVerticalVelocity += fJumpGrav * Time.deltaTime;

        // When spacebar and on floor is true
        if (Input.GetKey(KeyCode.Space) && ccPlayerController.isGrounded && !bHasJumped)
        {
            // Revereses gravity for jump
            fVerticalVelocity = fJumpHeight;
            bHasJumped = true;
        }
        else if (Input.GetKey(KeyCode.Space) && !ccPlayerController.isGrounded && !bHasJumped)
        {
            fVerticalVelocity = fJumpHeight;
            bHasJumped = true;
        }
        // Resets fall speed if already on ground
        else if (ccPlayerController.isGrounded)
        {
            bHasJumped = false;
            fVerticalVelocity = fJumpGrav;
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
