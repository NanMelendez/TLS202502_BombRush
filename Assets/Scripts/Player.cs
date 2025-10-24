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
    public Transform wallChecker;
    public LayerMask wallLayer;
    public InputActionReference playerControls;
    public InputActionReference playerJumpControl;
    public InputActionReference playerSprintControl;
    public Animator animator;
    public GameManager gm;

    private bool isTouchingFloor;
    private bool isFalling;
    private bool isSliding;
    private int velMod;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isTouchingFloor = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = playerControls.action.ReadValue<Vector2>();
        float jumpBtn = playerJumpControl.action.ReadValue<float>();
        float sprintBtn = playerSprintControl.action.ReadValue<float>();

        HorizontalMovement(direction.x, sprintBtn);
        VerticalMovement(direction.y, jumpBtn);
        wallSlide(direction.x);

        if (gm.GetRemainingTime() < 30.0f)
        {
            float freq = gm.GetRemainingTime() > 20.0f ? 2.0f : (gm.GetRemainingTime() > 10.0f ? 5.0f : 20.0f);

            playerRenderer.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.0f), 0.5f + 0.5f * Mathf.Sin(freq * Time.time));
        }
        
        if (gm.GetRemainingTime() == 0.0f)
        {
            animator.SetTrigger("tiempoTerminado");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingFloor = true;
            isFalling = false;
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

    private void HorizontalMovement(float hzDir, float sprint)
    {
        float sprintFactor = 1.0f;

        velMod = 0;

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

        rb2d.linearVelocityX = hzDir * speed * sprintFactor;
        animator.SetInteger("velMod", velMod);
    }

    private void VerticalMovement(float vtDir, float jumping)
    {
        rb2d.gravityScale = 1.0f;

        // if (vtDir != 0.0f)
        // {
        //     rb2d.gravityScale = 2.5f;
        // }
        // else
        // {
        //     rb2d.gravityScale = 1.0f;
        // }

        if ((vtDir > 0.0f || jumping != 0.0f) && isTouchingFloor)
        {
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            isTouchingFloor = false;
            animator.SetTrigger("estaSaltando");
        }

        if (rb2d.linearVelocityY < 0.0f && !isTouchingFloor)
        {
            isFalling = true;
            rb2d.gravityScale = gravFactor;
        }

        animator.SetBool("estaCayendo", isFalling);
    }

    private bool checkWall()
    {
        return Physics2D.OverlapCircle(wallChecker.position, 0.5f, wallLayer);
    }

    private void wallSlide(float hzMov)
    {
        isSliding = false;

        if (checkWall() && !isTouchingFloor && velMod > 0)
        {
            isSliding = true;
            rb2d.linearVelocityY = Mathf.Clamp(rb2d.linearVelocityY, -slideSpeed, float.MaxValue);
        }

        animator.SetBool("estaDeslizando", isSliding);
    }
}
