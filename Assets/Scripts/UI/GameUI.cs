using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Transform transformJugador;
    public BoxCollider2D colliderMeta;
    public TextMeshProUGUI textoDistancia;
    public SliderLogic barraTiempo;
    public SliderLogic barraEnergia;
    
    public float RemainingTime
    {
        get
        {
            return barraTiempo.Value;
        }
        set
        {
            barraTiempo.Value = Mathf.Min(value, barraTiempo.MaxValue);
        }
    }

    public float MaxTime
    {
        get
        {
            return barraTiempo.MaxValue;
        }
        set
        {
            barraTiempo.MaxValue = value;
        }
    }

    public float RemainingEnergy
    {
        get
        {
            return barraEnergia.Value;
        }
        set
        {
            barraEnergia.Value = Mathf.Min(value, barraEnergia.MaxValue);
        }
    }

    public float MaxEnergy
    {
        get
        {
            return barraEnergia.MaxValue;
        }
        set
        {
            barraEnergia.MaxValue = value;
        }
    }

    void Update()
    {
        if (transformJugador != null)
            CalculateDistance();
    }

    private void CalculateDistance()
    {
        Vector2 closestPoint = colliderMeta.ClosestPoint(transformJugador.position);
        float distance = Vector2.Distance(transformJugador.position, closestPoint);

        textoDistancia.text = string.Format("{0:0.0}m", distance);
    }
}
