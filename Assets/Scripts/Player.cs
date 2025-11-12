using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float slideSpeed;
    public Rigidbody2D rb2d;
    public float baseGravity;
    public float gravFallFactor;
    public SurfaceCheck wallCheck;
    public SurfaceCheck groundCheck;
    public GameInputs gameInputs;
    public Animator animator;
    public SpriteRenderer slimeRenderer;
    public GameManager gm;
    private bool isFalling;
    private bool isSliding;
    private int velMod;
    private bool hasJumpedSinceGrounded;
    private bool gamePausedOrOver;

    void Start()
    {
        isSliding = false;
        isFalling = true;
        slimeRenderer.enabled = false;
        gamePausedOrOver = false;
    }

    void Update()
    {
        if (!gamePausedOrOver)
        {
            HorizontalMovement();
            VerticalMovement();

            if (gm.TiempoRestante == 0.0f)
            {
                animator.SetTrigger("tiempoTerminado");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("pickTime"))
        {
            Destroy(collision.gameObject);
            gm.MasTiempo(10.0f);
        }

        if (collision.gameObject.CompareTag("pickEnergy"))
        {
            Destroy(collision.gameObject);
            gm.MasEnergia(25.0f);
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            if (collision.enabled)
            {
                collision.enabled = false;
                gamePausedOrOver = true;
                gm.Victoria();
            }
        }
    }

    public bool IsGrounded()
    {
        return groundCheck.IsColliding();
    }

    bool IsHittingWall()
    {
        return wallCheck.IsColliding();
    }

    private void HorizontalMovement()
    {
        float sprintFactor = 1.0f;

        velMod = 0;
        isSliding = false;

        if (gameInputs.IsMovingHorizontally())
        {
            transform.localScale = new Vector3(gameInputs.Direction.x, 1.0f, 1.0f);
            velMod = 1;

            if (gameInputs.IsSprinting())
            {
                sprintFactor = 1.5f;
                velMod = 2;
            }
        }

        if (IsHittingWall() && !IsGrounded() && velMod > 0)
        {
            isSliding = true;
            rb2d.linearVelocityY = -slideSpeed;
        }

        slimeRenderer.enabled = isSliding;

        rb2d.linearVelocityX = gameInputs.Direction.x * speed * sprintFactor;
        animator.SetInteger("velMod", velMod);
        animator.SetBool("estaDeslizando", isSliding);
    }

    private void VerticalMovement()
    {
        rb2d.gravityScale = baseGravity;
        bool isForcingFall = gameInputs.IsPressingDown();
        bool isJumping = gameInputs.IsPressingUp() || gameInputs.IsPressingSpace();
        bool grounded = IsGrounded();

        if (grounded)
        {
            hasJumpedSinceGrounded = false;
            isFalling = false;
        }

        if (grounded && isJumping && !hasJumpedSinceGrounded)
        {
            rb2d.linearVelocityY = 0.0f;
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            animator.SetTrigger("estaSaltando");
            hasJumpedSinceGrounded = true;
        }

        if ((rb2d.linearVelocityY < 0.0f || isForcingFall) && !grounded)
        {
            isFalling = true;
            rb2d.gravityScale = baseGravity + gravFallFactor + (isForcingFall ? 3.5f : 0.0f);
        }

        animator.SetBool("estaCayendo", isFalling);
    }

    public bool IsSliding()
    {
        return isSliding;
    }
}
