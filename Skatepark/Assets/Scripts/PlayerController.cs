using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;
    private float originalSpeed;
    private float originalJumpForce;
    private Coroutine speedBoostCoroutine; 
    private Coroutine jumpBoostCoroutine;  

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = speed;
        originalJumpForce = jumpForce;
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
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = transform.forward;
            transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
        }
    }

    void JumpPlayer()
    {
        // Check if the player is grounded and moving (horizontal or vertical input is non-zero)
        if (isGrounded && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public IEnumerator ApplySpeedBoost(float duration, float multiplier)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine); 
            speed = originalSpeed;             
        }

        speedBoostCoroutine = StartCoroutine(SpeedBoost(duration, multiplier));
        yield return null;
    }

    private IEnumerator SpeedBoost(float duration, float multiplier)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
        speedBoostCoroutine = null; 
    }

    public IEnumerator ApplyJumpBoost(float duration, float multiplier)
    {
        if (jumpBoostCoroutine != null)
        {
            StopCoroutine(jumpBoostCoroutine); 
            jumpForce = originalJumpForce;     
        }

        jumpBoostCoroutine = StartCoroutine(JumpBoost(duration, multiplier));
        yield return null;
    }

    private IEnumerator JumpBoost(float duration, float multiplier)
    {
        jumpForce *= multiplier;
        yield return new WaitForSeconds(duration);
        jumpForce = originalJumpForce;
        jumpBoostCoroutine = null; 
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
