using System.Collections;
using UnityEngine;

// Controls the movement and rotation of the camera

public class CameraController : MonoBehaviour 
{
    // Original Transform for the camera to reset to
    [SerializeField]
    Transform tOriginalTransform;

    // Move Speed
    [SerializeField]
    float fSpeed;
    // Turn Speed
    [SerializeField]
    float fMouseSensitivity;

	// Initialization
	void Start () 
    {
	}
	
	// Update 
	void Update () 
    {
        // Movement Axes
        float xAxis = Input.GetAxis("Horizontal") * Time.deltaTime;
        float yAxis = Input.GetAxis("Altitude") * Time.deltaTime;
        float zAxis = Input.GetAxis("Vertical") * Time.deltaTime;
        // Rotation Axes
        float mouseX = Input.GetAxis("Mouse Y") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse X") * Time.deltaTime;

        // Move the camera with the input from the Movement Axes
        transform.Translate((transform.right * xAxis * fSpeed) +
            (Vector3.up * yAxis * fSpeed) +
            (transform.forward * zAxis * fSpeed), Space.World);

        // If the Drag Button is held and the mouse is being dragged then rotate the camera.
        if (Input.GetButton("Drag"))
        {
            // Horizontal Rotation - Yaw
            Quaternion xRot = Quaternion.Euler(Vector3.right * mouseX * fMouseSensitivity);
            // Vertical Rotation - Pitch
            Quaternion yRot = Quaternion.Euler(Vector3.up * mouseY * fMouseSensitivity);

            // Calculate new rotation
            Quaternion rotation = transform.rotation * xRot * yRot;
            // Reset Z Rotation to 0 - Roll
            rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);

            // Apply new rotation
            transform.rotation = rotation;
        }
	}

    // Resets the transform of the camera to the original transform
    public void ResetPosition()
    {
        transform.position = tOriginalTransform.position;
        transform.rotation = tOriginalTransform.rotation;
    }
}
