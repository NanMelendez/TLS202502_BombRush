using UnityEngine;

public class SurfaceCheck : MonoBehaviour
{
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private LayerMask layer;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    public bool IsColliding()
    {
        return Physics2D.BoxCast(transform.position, size, 0, direction * transform.lossyScale.x, 0, layer);
    }
}
