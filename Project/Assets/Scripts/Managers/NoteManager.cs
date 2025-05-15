using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public void CloseNote()
    {
        GameManager.Instance.CloseNote();
    }
}
