public class PlayerStateMachine
{
    private PlayerBaseState m_currState;

    public PlayerIdleState idleState { get; private set; }
    public PlayerWalkState walkState { get; private set; }

    public PlayerStateMachine()
    {
        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
    }

    public void Update()
    {
        m_currState?.Update();
    }

    public void FixedUpdate()
    {
        m_currState?.FixedUpdate();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        m_currState?.Exit();
        m_currState = newState;
        m_currState.Enter();
    }
}
