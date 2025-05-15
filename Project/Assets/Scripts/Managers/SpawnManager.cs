using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnManager : MonoBehaviour
{
    private List<AsyncOperationHandle<GameObject>> m_spawnedObjects;

    [Header("Parameters For Spawn")]
    [SerializeField] private uint m_maxObjectsNumber;
    [SerializeField, Range(0, float.MaxValue)] private float m_spawnRadius;
    [SerializeField, Range(0,1.5f)] private float m_heightAboveGround;

    [Header("Objects To Spawn")]
    [SerializeField] private AssetReferenceGameObject[] m_objects;

    private int m_objIndx;

    private void Start()
    {
        m_spawnedObjects = new List<AsyncOperationHandle<GameObject>>();
        m_objIndx = 0;

        for (uint i = 0; i < m_maxObjectsNumber; ++i)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        AssetReferenceGameObject objForSpawn = m_objects[GameManager.Instance.prng.Next(0, m_objects.Length)];
        Vector3 objPosition;
        do
        {
            objPosition = transform.position + Random.insideUnitSphere * m_spawnRadius;
            objPosition.y = m_heightAboveGround;
        } while(IsPositionUsed(objPosition));

        AsyncOperationHandle<GameObject> objHandler = objForSpawn.InstantiateAsync(objPosition, Quaternion.identity, transform);
        objHandler.Completed += (operation) =>
        {
            objHandler.Result.GetComponent<SpawnObjectController>().Initialize(m_objIndx++);
            m_spawnedObjects.Add(objHandler);
        };
    }

    private bool IsPositionUsed(Vector3 position)
    {
        Vector3 playerPosition = PlayerController.Instance.Transform.position;
        return (playerPosition.x == position.x && playerPosition.z == position.z);
    }

    public void DestroySpawnObject(int indx)
    {
        var obj = m_spawnedObjects.Find(obj => obj.Result.GetComponent<SpawnObjectController>().index == indx);
        m_spawnedObjects.Remove(obj);
        Addressables.ReleaseInstance(obj);
    }

    public void RespawnAllObjects()
    {
        foreach(var obj in m_spawnedObjects)
        {
            Addressables.ReleaseInstance(obj);
        }
        m_spawnedObjects.Clear();

        for (uint i = 0; i < m_maxObjectsNumber; ++i)
        {
            SpawnObject();
        }
    }
}
