using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Transform vrCamera;
    public float moveSpeed = 2f; 
    public float boostMultiplier = 3f;

    public float lookSensitivity = 250f;

    public InputActionProperty moveAction;
    public InputActionProperty boostAction;

    private float pitch = 0f; // (up/down)
    private float yaw = 0f;   // (left/right)
    public void Start()
    {
        vrCamera = transform;
    }
    private void Update()
    {
        // if (vrCamera != null && XRSettings.enabled)
        // {
        //     HandleVRInput();
        // }
        // else
        // {
        //     HandleDefaultInput();
        // }
        HandleDefaultInput();
    }

    private void HandleVRInput()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        bool isBoosting = boostAction.action.ReadValue<float>() > 0;

        float currentSpeed = isBoosting ? moveSpeed * boostMultiplier : moveSpeed;

        Vector3 moveDirection = vrCamera.forward * input.y + vrCamera.right * input.x;
        moveDirection.y = 0;
        transform.position += moveDirection.normalized * currentSpeed * Time.deltaTime;
    }

    private void HandleDefaultInput()
    {
        float forwardInput = Input.GetAxis("Vertical"); // W/S or Up/Down
        float strafeInput = Input.GetAxis("Horizontal"); // A/D or Left/Right 
        bool isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Fire3"); // Shift or "Fire3" button

        float currentSpeed = isBoosting ? moveSpeed * boostMultiplier : moveSpeed;

        Vector3 moveDirection = new Vector3(strafeInput, 0, forwardInput).normalized;

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.Self);

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -90f, 90f); //over-rotation

            vrCamera.transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}
