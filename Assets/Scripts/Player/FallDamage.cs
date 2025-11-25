using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallDamage : MonoBehaviour
{
    [SerializeField]
    private float fallTimeThreshold;
    [SerializeField]
    private float fallSpeedThreshold;
    [SerializeField]
    private CamShake camShake;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Player player;
    private Rigidbody2D rb2d;
    private float fallSpeed;
    private float fallTimer;

    void Start()
    {
        rb2d = player.rb2d;
        fallTimer = 0.0f;
        fallSpeed = 0.0f;
    }

    void Update()
    {
        if (fallSpeed < 0.0f && !player.IsGrounded())
            fallTimer += Time.deltaTime;
    }

    void LateUpdate()
    {
        fallSpeed = rb2d.linearVelocityY;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.IsGrounded())
        {
            Debug.Log(string.Format("Impacto: {0:0.00}", fallSpeed) + string.Format(" Tiempo de caida: {0:0.00} s" , fallTimer));
            camShake.ShakeCamera(Mathf.Abs(fallSpeed) * 0.075f, fallTimer * 2.0f);

            

            fallTimer = 0.0f;
        }
    }
}
