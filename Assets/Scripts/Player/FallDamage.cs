using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallDamage : MonoBehaviour
{
    [SerializeField]
    private float fallTimeThreshold;
    [SerializeField]
    private float fallSpeedThreshold;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float damageMultiplier;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private FallShake camShake;
    private float fallTime = 0;
    private float fallSpeed = 0;
    private bool isGrounded;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb2d.linearVelocityY < 0.0f && !isGrounded)
            fallTime += Time.deltaTime;
    }

    void LateUpdate()
    {
        fallSpeed = rb2d.linearVelocityY;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int colLayer = collision.gameObject.layer;

        if (colLayer == groundLayer)
        {
            isGrounded = true;
            Debug.Log(fallSpeed);
        }

        if (isGrounded && (fallTime > fallTimeThreshold || fallSpeed < fallSpeedThreshold) && !player.IsSliding())
        {
            camShake.ShakeCamera(5.0f, 1.5f);
            // Debug.Log(string.Format("Tiempo caída: {0:00}, Mínimo: {0:00}", fallTime, fallTimeThreshold));
            // Debug.Log(string.Format("Velocidad caída: {0:00}, Mínimo: {0:00}", fallSpeed, fallSpeedThreshold));
            // float calc1 = Mathf.Max(fallTime - fallTimeThreshold, 0.0f);
            // float calc2 = -0.5f * Mathf.Max(fallSpeed - fallSpeedThreshold, 0.0f);
            // Debug.Log("Fall multiplier: " + damageMultiplier);
            // Debug.Log("Time - threshold: " + calc1);
            // Debug.Log("-0.5 * (velY - threshold): " + calc2);
            // float totalDamage = damageMultiplier * (calc1 + calc2);
            // Debug.Log(string.Format("Perdiste {0:0.00}%", totalDamage));
        }

        if (player != null)
            if (isGrounded || player.IsSliding())
                fallTime = 0;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
            isGrounded = false;
    }
}
