using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
[DefaultExecutionOrder(-1)]
public class HealthBarController : MonoBehaviour
{
    private Slider m_slider;

    private void Start()
    {
        m_slider = GetComponent<Slider>();
        m_slider.maxValue = PlayerController.Instance.Data.maxHP;
        SetHeath(PlayerController.Instance.Data.maxHP);
    }

    public void SetHeath(int health)
    {
        m_slider.value = health;
    }
}
