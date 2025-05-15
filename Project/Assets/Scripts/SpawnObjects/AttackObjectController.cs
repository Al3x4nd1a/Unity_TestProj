using UnityEngine;

public class AttackObjectController : SpawnObjectController
{
    [SerializeField] private int m_damage;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        GameManager.Instance.AddToInventory(this);
        PlayerController.Instance.GetHit(m_damage);
        transform.parent.GetComponent<SpawnManager>().DestroySpawnObject(index);
    }
}
