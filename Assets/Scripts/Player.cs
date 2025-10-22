using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SpriteRenderer playerRenderer;
    public float speed;
    public float jumpSpeed;
    public Rigidbody2D rb2d;
    public InputActionReference playerControls;
    public InputActionReference playerJumpControl;
    public GameManager gm;

    private bool isTouchingFloor;

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

        rb2d.linearVelocityX = direction.x * speed;

        if (direction.x != 0.0f)
            playerRenderer.flipX = direction.x < 0.0f;

        if ((direction.y > 0.0f || jumpBtn != 0.0f) && isTouchingFloor)
        {
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            isTouchingFloor = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isTouchingFloor = true;
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
}
