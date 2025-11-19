using Unity.Cinemachine;
using UnityEngine;

public class FallShake : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    SurfaceCheck groundCheck;
    private float fallSpeed = 0.0f;
    private bool hasCrashedOntoFloor = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && fallSpeed <= -10.0f && !hasCrashedOntoFloor)
        {
            hasCrashedOntoFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            hasCrashedOntoFloor = false;
    }

    void LateUpdate()
    {
        if (groundCheck.IsColliding())
            fallSpeed = rb2d.linearVelocityY;
    }

    private void DisableShake()
    {
        
    }
}
