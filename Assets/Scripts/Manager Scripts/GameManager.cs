using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetCursorLock(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isLocked = Cursor.lockState == CursorLockMode.Confined;
            SetCursorLock(!isLocked);
        }
    }

    public void SetCursorLock(bool lockCursor)
    {
        Cursor.lockState = lockCursor ? CursorLockMode.Confined : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }
}
