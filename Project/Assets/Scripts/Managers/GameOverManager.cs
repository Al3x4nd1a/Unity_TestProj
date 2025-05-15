using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.Restart();
    }
}
