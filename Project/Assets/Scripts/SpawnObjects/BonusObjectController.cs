using System.Runtime.CompilerServices;
using UnityEngine;

public class BonusObjectController : SpawnObjectController
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        GameManager.Instance.AddToInventory(this);
        PlayerController.Instance.GetBonus();
        transform.parent.GetComponent<SpawnManager>().DestroySpawnObject(index);
    }
}
