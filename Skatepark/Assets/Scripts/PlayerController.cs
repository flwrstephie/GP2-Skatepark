using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public float jumpForce = 5f; 
    private Rigidbody rb;       
    private bool isGrounded;    

    public Transform skateboard; // Reference to the skateboard

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        MovePlayer();
        JumpPlayer();
    }

    void MovePlayer()
{
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    if (direction.magnitude >= 0.1f)
    {
        // Use camera's forward direction for movement
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // Ignore vertical rotation of the camera
        cameraForward.Normalize();

        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0; // Ignore vertical rotation of the camera
        cameraRight.Normalize();

        // Calculate movement direction based on camera orientation
        Vector3 moveDir = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // Rotate the player to face the movement direction
        float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        // Move the player
        rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
    }
}


    void JumpPlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Only allow jumping if the player is moving and grounded
        if (isGrounded && direction.magnitude >= 0.1f && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent multiple jumps while in the air
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Set isGrounded to true only if the player touches the ground
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
