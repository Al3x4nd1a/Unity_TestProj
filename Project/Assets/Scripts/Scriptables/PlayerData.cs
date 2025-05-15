using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptables/Characters/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Health")]
    [SerializeField] private int m_maxHP;
    public int maxHP { get => m_maxHP; }
    public int currHP { get; set; }

    [Header("Movement")]
    [SerializeField] private float m_walkSpeed;
    public float walkSpeed { get => m_walkSpeed; }
    [SerializeField] private float m_runSpeed;
    public float runSpeed { get => m_runSpeed; }
    [SerializeField] private float m_rotationSpeed;
    public float rotationSpeed { get => m_rotationSpeed; }

    [Header("Interactions")]
    [SerializeField] private int m_minBonusDuration;
    public int minBonusDuration { get => m_minBonusDuration; }
    [SerializeField] private int m_maxBonusDuration;
    public int maxBonusDuration { get => m_maxBonusDuration; }

    [Header("Animator Hashes")]
    [SerializeField] private string m_moveParameter = "IsMoving";
    public int moveHash { get => Animator.StringToHash(m_moveParameter); }
    [SerializeField] private string m_velocityParameter = "Velocity";
    public int velocityHash { get => Animator.StringToHash(m_velocityParameter); }
}
