using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SpriteRenderer playerRenderer;
    public float speed;
    public float jumpSpeed;
    public float slideSpeed;
    public Rigidbody2D rb2d;
    public float gravFactor;
    public Vector2 wallcheckSize;
    public float wallcheckCastDistance;
    public LayerMask wallLayer;
    public Vector2 groundcheckSize;
    public float groundcheckCastDistance;
    public LayerMask groundLayer;
    public InputActionReference playerControls;
    public InputActionReference playerJumpControl;
    public InputActionReference playerSprintControl;
    public Animator animator;
    public GameManager gm;
    private bool isFalling;
    private bool isSliding;
    private int velMod;
    private bool hasJumpedSinceGrounded;

    void Start()
    {
        isSliding = false;
        isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = playerControls.action.ReadValue<Vector2>();
        float jumpBtn = playerJumpControl.action.ReadValue<float>();
        float sprintBtn = playerSprintControl.action.ReadValue<float>();

        HorizontalMovement(Mathf.Round(direction.x), sprintBtn);
        VerticalMovement(Mathf.Round(direction.y), jumpBtn);

        if (gm.GetRemainingTime() < 30.0f)
        {
            float freq = gm.GetRemainingTime() > 20.0f ? 2.0f : (gm.GetRemainingTime() > 10.0f ? 5.0f : 20.0f);

            playerRenderer.color = Color.Lerp(Color.white, Color.red, 0.5f + 0.5f * Mathf.Sin(freq * Time.time));
        }
        else
            playerRenderer.color = Color.white;

        if (gm.GetRemainingTime() == 0.0f)
        {
            animator.SetTrigger("tiempoTerminado");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("pickTime"))
        {
            Destroy(collision.gameObject);
            gm.AddTime(10.0f);
        }

        if (collision.gameObject.CompareTag("pickEnergy"))
        {
            Destroy(collision.gameObject);
            gm.AddEnergy(25.0f);
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            if (collision.enabled)
            {
                collision.enabled = false;
                gm.Win();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundcheckCastDistance, groundcheckSize);
        Gizmos.DrawWireCube(transform.position + transform.right * transform.localScale.x * wallcheckCastDistance, wallcheckSize);
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, groundcheckSize, 0, -transform.up, groundcheckCastDistance, groundLayer);
    }

    bool IsHittingWall()
    {
        return Physics2D.BoxCast(transform.position, wallcheckSize, 0, transform.right * transform.localScale.x, wallcheckCastDistance, wallLayer);
    }

    private void HorizontalMovement(float hzDir, float sprint)
    {
        float sprintFactor = 1.0f;

        velMod = 0;
        isSliding = false;

        if (hzDir != 0.0f)
        {
            transform.localScale = new Vector3(hzDir, 1.0f, 1.0f);
            velMod = 1;

            if (sprint > 0.0f)
            {
                sprintFactor = 1.5f;
                velMod = 2;
            }
        }

        if (IsHittingWall() && !IsGrounded() && velMod > 0)
        {
            isSliding = true;
            // rb2d.linearVelocityY = Mathf.Clamp(rb2d.linearVelocityY, -slideSpeed, float.MaxValue);
            rb2d.linearVelocityY = -slideSpeed;
        }

        rb2d.linearVelocityX = hzDir * speed * sprintFactor;
        animator.SetInteger("velMod", velMod);
        animator.SetBool("estaDeslizando", isSliding);
    }

    private void VerticalMovement(float vtDir, float jumping)
    {
        rb2d.gravityScale = 1.0f;

        bool jumpSignal = vtDir > 0.0f || jumping != 0.0f;
        bool grounded = IsGrounded();

        if (grounded)
        {
            hasJumpedSinceGrounded = false;
            isFalling = false;
        }

        if (grounded && jumpSignal && !hasJumpedSinceGrounded)
        {
            rb2d.linearVelocityY = 0.0f;
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            animator.SetTrigger("estaSaltando");
            hasJumpedSinceGrounded = true;
        }

        if (rb2d.linearVelocityY < 0.0f && !grounded)
        {
            isFalling = true;
            rb2d.gravityScale = gravFactor + (vtDir < 0.0f ? 2.5f : 0.0f);
        }
        animator.SetBool("estaCayendo", isFalling);
    }
}
