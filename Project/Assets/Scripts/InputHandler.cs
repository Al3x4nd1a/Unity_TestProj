using UnityEngine;

[DefaultExecutionOrder(-2)]
public class InputHandler : MonoBehaviour
{
    private InputActions m_inputActions;

    public bool IsOpenListBttnPressed { get => m_inputActions.Action.OpenList.WasPressedThisFrame(); }

    private void OnEnable()
    {
        if(m_inputActions == null)
        {
            m_inputActions = new InputActions();
        }

        m_inputActions.Enable();
    }

    private void OnDisable()
    {
        m_inputActions.Disable();
    }

    public Vector2 GetMovementVector()
    {
        return m_inputActions.Action.Movement.ReadValue<Vector2>();
    }
}
