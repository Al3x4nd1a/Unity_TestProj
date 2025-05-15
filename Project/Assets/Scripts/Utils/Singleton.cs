using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Component
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }

        Instance = this as T;
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if(Instance == (this as T))
        {
            Instance = null;
        }
    }
}
