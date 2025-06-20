using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private ProgressBarController progressBarController;
    public ProgressBarController ProgressBarController => progressBarController;

    [SerializeField] TMP_Text currentMovementTypeText;

    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        ActionManager.OnShowAttackMessage += ShowAttackMessage;
    }
    void OnDisable()
    {
        ActionManager.OnShowAttackMessage -= ShowAttackMessage;
    }

    public void ShowAttackMessage(string message)
    {
        attackText.text = message;
        attackText.gameObject.SetActive(true);

        CancelInvoke(nameof(HideAttackMessage));
        Invoke(nameof(HideAttackMessage), 1f);
    }

    private void HideAttackMessage()
    {
        attackText.gameObject.SetActive(false);
    }

    public void SetCurrentMovementTypeText(PlayerController playerController)
    {
        currentMovementTypeText.text =  playerController.MovementType == MovementTypeEnum.Directional ? "Movement Type = Directional" : "Movement Type = Strafe";
    }
}
