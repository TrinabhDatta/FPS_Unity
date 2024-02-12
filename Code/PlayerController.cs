using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public bool isWalking = false;
    public bool isJumping = false;
    public bool isGrounded = false;

    [SerializeField] Camera PlayerCamera;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    private CharacterController controller;

    private float xRotation;
    private float xInput;
    private float yInput;
    private float mouseX;
    private float mouseY;

    Vector3 movemenentVector;
    Vector3 velocity;
    private PlayerStats playerStats;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleJumping();
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        mouseX = Input.GetAxis("Mouse X") * playerStats.mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * playerStats.mouseSensitivity * Time.deltaTime;

        isWalking = Input.GetKey(KeyCode.LeftShift);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement() 
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        movemenentVector = transform.right * xInput + transform.forward * yInput;
        movemenentVector = movemenentVector.normalized;

        if (isWalking == true)
        {
            controller.Move(movemenentVector * playerStats.crouchSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(movemenentVector * playerStats.runningMovementSpeed * Time.deltaTime);
        }        
    }

    void HandleJumping()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerStats.jumpHeight * -2f * playerStats.gravity);
        }

        velocity.y += playerStats.gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        
    }

}
