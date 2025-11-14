using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class GameInputs : MonoBehaviour
{
    [SerializeField]
    private InputActionReference wasdControl;
    [SerializeField]
    private InputActionReference spaceControl;
    [SerializeField]
    private InputActionReference lshiftControl;
    [SerializeField]
    private InputActionReference escControl;
    [SerializeField]
    private GameManager gm;

    private Vector2 direction;
    private float hzValue;
    private float vtValue;
    private float spaceValue;
    private float sprintValue;
    private float escValue;

    void OnEnable()
    {
        wasdControl.action.Enable();
        spaceControl.action.Enable();
        lshiftControl.action.Enable();
        escControl.action.Enable();
        escControl.action.started += ChangePauseStateCallback;
    }

    void OnDisable()
    {
        wasdControl.action.Disable();
        spaceControl.action.Disable();
        lshiftControl.action.Disable();
        escControl.action.Disable();
        escControl.action.started -= ChangePauseStateCallback;
    }

    public Vector2 Direction
    {
        get
        {
            return new Vector2(Mathf.Round(hzValue), Mathf.Round(vtValue));
        }
    }

    void Update()
    {
        direction = wasdControl.action.ReadValue<Vector2>();
        hzValue = direction.x;
        vtValue = direction.y;
        spaceValue = spaceControl.action.ReadValue<float>();
        sprintValue = lshiftControl.action.ReadValue<float>();
        escValue = escControl.action.ReadValue<float>();
    }

    private void ChangePauseStateCallback(InputAction.CallbackContext context)
    {
        if (context.interaction is PressInteraction)
            gm.EstadoPausa = !gm.EstadoPausa;
    }

    public bool IsMovingHorizontally()
    {
        return Direction.x != 0.0f;
    }

    public bool IsMovingVertically()
    {
        return Direction.y != 0.0f;
    }

    public bool IsPressingDown()
    {
        return vtValue < 0.0f;
    }

    public bool IsPressingUp()
    {
        return vtValue > 0.0f;
    }

    public bool IsPressingSpace()
    {
        return spaceValue > 0.0f;
    }

    public bool IsSprinting()
    {
        return sprintValue > 0.0f;
    }

    public bool IsPressingEscape()
    {
        return escValue > 0.0f;
    }
}
