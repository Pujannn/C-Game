using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cameraTransform;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float lookSensitivity = 2f;
    public float cameraDistance = 5f;
    public Vector3 cameraOffset = new Vector3(0, 2, 0);


    private float cameraPitch = 0f;
    private bool isFreeLooking = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed: walkSpeed ;
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);

    }
}
