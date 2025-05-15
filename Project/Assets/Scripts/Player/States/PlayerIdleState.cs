using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {

    }

    public override void Exit()
    {
       
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        var rawMovement = GameManager.Instance.InputHandler.GetMovementVector();
    
        if(rawMovement != Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.walkState);
        }
    }
}
