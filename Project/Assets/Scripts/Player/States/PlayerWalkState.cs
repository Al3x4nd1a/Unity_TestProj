using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    private Vector2 m_rawInput;

    public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        m_rawInput = Vector2.zero;

        playerController.Animator.SetBool(playerController.Data.moveHash, true);
    }

    public override void Exit()
    {
        playerController.Animator.SetBool(playerController.Data.moveHash, false);
    }

    public override void FixedUpdate()
    {
        Vector3 direction = playerController.FollowCameraTransform.right * m_rawInput.x
            + playerController.FollowCameraTransform.forward * m_rawInput.y;
        direction.y = 0f;
        
        if(direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            playerController.Transform.rotation = Quaternion.RotateTowards(playerController.Transform.rotation, toRotation,
                playerController.Data.rotationSpeed * Time.deltaTime);
        }
        
        if(playerController.collisionChecker.IsGrounded())
        {
            Vector3 motion = direction * Time.deltaTime;
            if(playerController.IsBoosted)
            {
                motion *= playerController.Data.runSpeed;
            }
            else
            {
                motion *= playerController.Data.walkSpeed;
            }
            playerController.CharacterController.Move(motion);
        }
    }

    public override void Update()
    {
        m_rawInput = GameManager.Instance.InputHandler.GetMovementVector();
    
        if(m_rawInput == Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.idleState);
        }
    }
}
