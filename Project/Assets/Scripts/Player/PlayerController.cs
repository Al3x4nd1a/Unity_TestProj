using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private GameObject m_playerObject;
    [SerializeField] private PlayerData m_playerData;
    [Space, SerializeField] private CinemachineCamera m_camera;
    [SerializeField] private HealthBarController m_healthBarController;

    public Transform FollowCameraTransform { get => m_camera.transform; }
    public Transform Transform { get => m_playerObject.transform; }
    public CharacterController CharacterController { get => m_playerObject.GetComponent<CharacterController>(); }
    public Animator Animator { get => m_playerObject.GetComponent<Animator>(); }
    public PlayerData Data { get => m_playerData; }

    private PlayerStateMachine m_stateMachine;
    public CollisionChecker collisionChecker { get; private set; }
    public bool IsBoosted { get; private set; }

    private float m_lastBonusTime;
    private float m_currBonusDuration;

    private void Start()
    {
        collisionChecker = new CollisionChecker(m_playerObject.transform.Find("GroundCheck"));
        m_stateMachine = new PlayerStateMachine();
        SetBaseParams();
        m_stateMachine.SwitchState(m_stateMachine.idleState);
    }

    private void Update()
    {
        if(IsBoosted && Time.time - m_lastBonusTime >= m_currBonusDuration)
        {
            IsBoosted = false;
            Animator.SetFloat(Data.velocityHash, 0);
        }

        m_stateMachine.Update();
    }

    private void FixedUpdate()
    {
        m_stateMachine.FixedUpdate();
    }

    public void GetBonus()
    {
        IsBoosted = true;
        m_lastBonusTime = Time.time;
        m_currBonusDuration = GameManager.Instance.prng.Next(Data.minBonusDuration, Data.maxBonusDuration);
        Animator.SetFloat(Data.velocityHash, 1);
    }

    public void GetHit(int damage)
    {
        Data.currHP -= damage;
        m_healthBarController.SetHeath(Data.currHP);

        if(Data.currHP <= 0)
        {
            GameManager.Instance.SetGameOverScreen();
        }
    }

    public void SetBaseParams()
    {
        Data.currHP = Data.maxHP;
        m_healthBarController.SetHeath(Data.maxHP);
        m_lastBonusTime = m_currBonusDuration = 0f;
        IsBoosted = false;
    }
}
