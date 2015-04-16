using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    // Mouse rotation float
    float fMouseVRot;

    // Movement speed
    public float fPlayerBaseSpeed;
    public float fPlayerSideSpeedPercent;
    public float fPlayerBackSpeedPercent;

    // Movement bool
    bool bIsMovementEnabled = true;

    // Axes float
    float fHorizontal;
    float fVertical;

    // Mouse settings
    public float fMouseSensitivity;
    public float fMouseClampRange;

    // Jumping floats
    public float fJumpHeight;
    public float fJumpSpeedPercent;

    // Artificial gravity float
    float fVerticalVelocity;

    // Count down variables
    float fBetweenCountDownTime = 1;
    float fCountDown = 3;
    public Text TextCountDown;

    // Gets character controller
    public CharacterController ccPlayerController;

    // Camera object
    public GameObject GOCamera;

    /*void Start()
    {
        // Locks mouse to screen and makes visable
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }*/

    // Add player speed variables

    void Update()
    {
        //LockMouseCursor();
        //LevelStartCountDown();
    }

    void FixedUpdate()
    {
        CameraMovement();
        if (bIsMovementEnabled)
        {
            PlayerMovement();
            PlayerJump();
        }
        HeadBobbing();
    }
    /*
    // Starts count down at beginning
    void LevelStartCountDown()
    {
        // Checks movement enabled and countdown
        if (!bIsMovementEnabled)
        {
            fBetweenCountDownTime -= Time.deltaTime;

            for (int i = 0; i <= 3; i++)
            {
                // if 0 activate movement
                if (fCountDown == 0)
                {
                    TextCountDown.text = "GO!";
                    bIsMovementEnabled = true;
                    Invoke("DisableCountDownText", 1.0f);
                }
                else if (fBetweenCountDownTime <= 0)
                {
                    fBetweenCountDownTime = 1;
                    fCountDown--;
                    TextCountDown.text = fCountDown.ToString();
                }
            }
        }
    }*/

    // Disables count down text
    void DisableCountDownText()
    {
        TextCountDown.enabled = false;
    }
    /*
    // Locks mouse cursor
    void LockMouseCursor()
    {
        // Used to change mouse lock
        if (Input.GetKey(KeyCode.T))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }*/

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
        fHorizontal = Input.GetAxis("Horizontal") * fPlayerBaseSpeed / 100 * fPlayerSideSpeedPercent;

        // If player moving backwards
        if (fVertical <= -0.1f)
        {
            // Change speed
            fVertical = fVertical / 100 * fPlayerBackSpeedPercent;
        }

        // If player is in air change speed
        if(!ccPlayerController.isGrounded)
        {
            fVertical = fVertical / 100 * fJumpSpeedPercent;
            fHorizontal = fHorizontal / 100 * fJumpSpeedPercent;
        }

        // Applies movement to vectors
        Vector3 v3Velocity = new Vector3(fHorizontal, fVerticalVelocity, fVertical);

        v3Velocity = transform.rotation * v3Velocity;

        // Apply movement
        ccPlayerController.Move(v3Velocity * Time.deltaTime);
    }

    // Player jump button check function
    void PlayerJump()
    {
        Debug.Log("Is player on ground: " + ccPlayerController.isGrounded);

        // Increases fall speed over time
        fVerticalVelocity += Physics.gravity.y * Time.deltaTime;

        // When spacebar and on floor is true
        if (Input.GetKey(KeyCode.Space) && ccPlayerController.isGrounded)
        {
            // Revereses gravity for jump
            fVerticalVelocity = fJumpHeight;
            Debug.Log("Jumping");
        }
        // Resets fall speed if already on ground
        else if (ccPlayerController.isGrounded)
        {
            fVerticalVelocity = 0;
        }
    }

    // Headbobbing function
    void HeadBobbing()
    {
        // Calls movement animations
        // If player movement detected
        if (fHorizontal >= 0.1f || fHorizontal <= -0.1f || fVertical >= 0.1f || fVertical <= -0.1f)
        {
            // And off ground
            if (ccPlayerController.isGrounded)
            {
                StartAnimation();
            }
            else
            {
                StopAnimation();
            }
        }
        // Else if player is off ground or is idle
        else if (ccPlayerController.isGrounded == false || fHorizontal == 0.0f || fVertical == 0.0f)
        {
            StopAnimation();
        }
    }

    // Stops animation
    void StopAnimation()
    {
        GOCamera.GetComponent<Animator>().SetBool("bIsMoving", false);
    }

    // Start animation
    void StartAnimation()
    {
        GOCamera.GetComponent<Animator>().SetBool("bIsMoving", true);
        Debug.Log("Camera Bobbing");
    }
}
