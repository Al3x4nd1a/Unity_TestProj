using UnityEngine;

public abstract class SpawnObjectController : MonoBehaviour
{
    public int index { get; set; }

    [SerializeField] protected string m_objectName; 
    public string objectName { get => m_objectName; }

    public void Initialize(int indx)
    {
        index = indx;
    }

    protected abstract void OnTriggerEnter(Collider other);
}
