using UnityEngine;

public class NoteObjectController : SpawnObjectController
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        GameManager.Instance.AddToInventory(this);
        GameManager.Instance.OpenNote();
        transform.parent.GetComponent<SpawnManager>().DestroySpawnObject(index);
    }
}
