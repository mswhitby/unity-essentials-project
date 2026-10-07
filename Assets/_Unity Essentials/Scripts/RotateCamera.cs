using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Transform playerBody; 

    private float verticalRotation = 1f;

    //public bool active;
    //public float horizontalInput;
    //public float verticalInput;

    void Start()
    {
        // Cap the framerate to 60 FPS so your computer stops working overtime
        Application.targetFrameRate = 60;

        // Optional: Ensure local rotation starts synced with the camera's initial angle
        verticalRotation = transform.localEulerAngles.x;
        
    }

    void Update()
    {
        // 0. Only rotate when the Right Mouse Button (1) is held down. 
        // Use 0 for Left Mouse Button if preferred.

        //active = Input.GetMouseButton(0);
        bool active = Input.GetMouseButton(0);

        if (active)
        {
            Cursor.lockState = CursorLockMode.Locked; // Locks mouse to the center of screen
            Cursor.visible = false;                   // Hides the cursor arrow
        } else
        {
            Cursor.lockState = CursorLockMode.None; // Unlocks the mouse
            Cursor.visible = true;                  // Shows the cursor arrow again
        }

        if (active)
        {
            // 1. Get relative mouse movements
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");

            // 2. Rotate the PLAYER left/right around the Y-axis
            if (playerBody != null)
            {
                playerBody.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Fallback: rotates just the camera if playerBody isn't assigned
                transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime, Space.World);
            }

            // 3. Track and clamp vertical rotation for the CAMERA
            verticalRotation -= verticalInput * rotationSpeed * Time.deltaTime;
            verticalRotation = Mathf.Clamp(verticalRotation, -10f, 45f);

            // 4. Apply vertical rotation locally to the camera, preserving its current horizontal direction
            transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0f);
        }
    }
}
