public abstract class PlayerBaseState
{
    protected readonly PlayerStateMachine stateMachine;
    protected readonly PlayerController playerController;

    protected PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = PlayerController.Instance;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
