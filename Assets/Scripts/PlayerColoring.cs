using UnityEngine;

public class PlayerColoring : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color dangerColor;
    [SerializeField]
    private Color forcedFallColor;
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameManager gm;

    private float t = 0.0f;

    void Start()
    {
        sprite.color = defaultColor;
    }

    void Update()
    {
        sprite.color = defaultColor;

        if (gm.TiempoRestante <= 0.0f)
        {
            sprite.color = dangerColor;
            return;
        }

        sprite.color = IsForcedFalling() ? forcedFallColor : defaultColor;

        if (gm.TiempoRestante < gm.TiempoLimite / 2.0f)
        {
            float freq = 2.0f;

            if (gm.TiempoRestante <= gm.TiempoLimite / 3.0f)
                freq = 5.0f;

            if (gm.TiempoRestante <= gm.TiempoLimite / 4.0f)
                freq = 20.0f;

            t += Time.deltaTime;

            sprite.color = Color.Lerp(sprite.color, dangerColor, 0.5f + 0.5f * Mathf.Sin(freq * t));
        }
        else
            t = 0.0f;
    }

    private bool IsForcedFalling()
    {
        return !player.IsGrounded() && player.gameInputs.IsPressingDown();
    }
}
