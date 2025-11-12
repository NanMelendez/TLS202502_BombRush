using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    [SerializeField]
    private InputActionReference wasdControl;
    [SerializeField]
    private InputActionReference spaceControl;
    [SerializeField]
    private InputActionReference lshiftControl;

    private Vector2 direction;
    private float hzValue;
    private float vtValue;
    private float spaceValue;
    private float sprintValue;

    void OnEnable()
    {
        wasdControl.action.Enable();
        spaceControl.action.Enable();
        lshiftControl.action.Enable();
    }

    void OnDisable()
    {
        wasdControl.action.Disable();
        spaceControl.action.Disable();
        lshiftControl.action.Disable();
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
}
