using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
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
        {
            transform.localScale = new Vector3(direction.x, 1.0f, 1.0f);
        }

        if ((direction.y > 0.0f || jumpBtn != 0.0f) && isTouchingFloor)
        {
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            isTouchingFloor = false;
        }
        
        if (gm.GetRemainingTime() < 30.0f)
        {
            float freq = gm.GetRemainingTime() > 20.0f ? 2.0f : (gm.GetRemainingTime() > 10.0f ? 5.0f : 20.0f);

            playerRenderer.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.0f), 0.5f + 0.5f * Mathf.Sin(freq * Time.time));
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
