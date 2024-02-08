using FishNet.Object;
using UnityEngine;
using FishNet.Connection;

public class PlayerController : MonoBehaviour
{
    public float mouseSensitivity = 500f;
    float movementSpeed = 6f;
    public float xRotation = 0f;

    [SerializeField] Camera PlayerCamera;
    private CharacterController controller;

    private float xInput;
    private float yInput;
    private float mouseX;
    private float mouseY;
    Vector3 movemenentVector;


    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
       controller = GetComponent<CharacterController>();
}

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }


    void HandleMouseLook()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        mouseX = Mathf.Clamp(mouseX, -90f, 90f);

        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement() 
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        movemenentVector = transform.right * xInput + transform.forward * yInput;
        movemenentVector = movemenentVector.normalized;
        controller.Move(movemenentVector * movementSpeed * Time.deltaTime);
    }

}
