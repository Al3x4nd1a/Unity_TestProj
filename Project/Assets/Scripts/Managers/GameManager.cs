using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(InputHandler))]
[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>
{
    public InputHandler InputHandler { get; private set; }

    [SerializeField] private int m_seed;
    [SerializeField] private string m_inventoryName;
    [Space, SerializeField] private SpawnManager m_spawnManager;
    [SerializeField] private GameObject m_noteHandler;
    [SerializeField] private GameObject m_openListBttn;
    [SerializeField] private GameObject m_inventory;

    [Header("Addressables")]
    [SerializeField] private AssetReferenceGameObject m_gameOverUI;
    private AsyncOperationHandle<GameObject> m_gameOverUIHandler;
    [SerializeField] private AssetReferenceGameObject m_noteUI;
    private AsyncOperationHandle<GameObject> m_noteUIHandler;
    [SerializeField] private AssetReferenceGameObject m_textUI;
    private List<AsyncOperationHandle<GameObject>> m_textUIHandler;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask m_groundLayerMask;
    public LayerMask groundLayerMask { get => m_groundLayerMask; }

    public System.Random prng { get; private set; }
    private SaveLoadController m_saveLoadController;

    private void Start()
    {
        InputHandler = GetComponent<InputHandler>();
        prng = new System.Random(m_seed);
        m_saveLoadController = new SaveLoadController(Application.persistentDataPath, m_inventoryName);
        m_textUIHandler = new List<AsyncOperationHandle<GameObject>>();
    }

    private void Update()
    {
        if(InputHandler.IsOpenListBttnPressed)
        {
            OpenList();
        }
    }

    public void OpenList()
    {
        CanvasGroup inventoryGroup = m_inventory.GetComponent<CanvasGroup>();
        inventoryGroup.interactable = !inventoryGroup.interactable;
        inventoryGroup.alpha = inventoryGroup.alpha > 0? 0 : 1;
    }

    public void AddToInventory(SpawnObjectController obj)
    {
        m_saveLoadController.Save(obj.objectName);

        Transform content = m_inventory.transform.GetChild(0).GetChild(0);

        List<string> inventory = m_saveLoadController.Load();
        string lastObj = inventory.Last();
        int objIndx = inventory.Count - 1;

        AsyncOperationHandle<GameObject> objHandler = m_textUI.InstantiateAsync(content);
        objHandler.Completed += (operation) =>
        {
            objHandler.Result.GetComponent<TMP_Text>().text = inventory[objIndx];
            m_textUIHandler.Add(objHandler);
        };
    }

    private void ReleaseInventory()
    {
        CanvasGroup inventoryGroup = m_inventory.GetComponent<CanvasGroup>();
        inventoryGroup.interactable = false;
        inventoryGroup.alpha = 0;

        foreach (var textUI in m_textUIHandler)
        {
            Addressables.ReleaseInstance(textUI);
        }
        m_textUIHandler.Clear();
    }

    public void SetGameOverScreen()
    {
        Time.timeScale = 0f;
        m_gameOverUIHandler = m_gameOverUI.InstantiateAsync();
    }

    public void Restart()
    {
        m_saveLoadController.Erase();
        m_spawnManager.RespawnAllObjects();
        PlayerController.Instance.SetBaseParams();
        Addressables.ReleaseInstance(m_gameOverUIHandler);
        ReleaseInventory();
        CloseNote();
        Time.timeScale = 1f;
    }

    public void OpenNote()
    {
        if (m_noteHandler.transform.childCount != 0)
        {
            return;
        }
        
        m_noteUIHandler = m_noteUI.InstantiateAsync(m_noteHandler.transform);
    }

    public void CloseNote()
    {
        if(m_noteUIHandler.Result == null)
        {
            return;
        }

        Addressables.ReleaseInstance(m_noteUIHandler);
    }
}
