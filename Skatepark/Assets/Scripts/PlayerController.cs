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
        JumpPlayer(); // Add jump function here
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = transform.forward; 
            transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
        }
    }

    void JumpPlayer()
    {
        // Check if the player is grounded and pressing the jump button (space key)
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // This method checks if the player is grounded by using a small raycast
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
