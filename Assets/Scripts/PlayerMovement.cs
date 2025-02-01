using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 10f;
    public float jumpingPower = 20f;
    private bool isFacingRight = true;

    public GameManager gm;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Check for grounded condition in FixedUpdate
        if (IsGrounded() && rb.velocity.y <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Cube")){
            gm.cubeScore += 10; 
            Destroy(other.gameObject);
        }
    }
}
