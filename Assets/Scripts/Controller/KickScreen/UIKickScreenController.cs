using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using static CheckGameManager;

public class UIKickScreenController : MonoBehaviour
{
    [Header("General Settings")]
    public string checkGameSceneName = "CheckGame";
    private bool isDisplayTouched = false;

    [Header("ServerMaintenance Settings")]
    public GameObject gameObjectServerMaintenance = null;
    public TextMeshProUGUI maintenanceTitleText = null;
    public TextMeshProUGUI maintenanceReasonText = null;
    public TextMeshProUGUI maintenanceExpectedEndTime = null;

    [Header("FailedGameCheck Settings")]
    public GameObject gameObjectFailedGameCheck = null;
    public TextMeshProUGUI failedGameCheckReasonText = null;

    private void Start()
    {
        gameObjectServerMaintenance.SetActive(false);
        gameObjectFailedGameCheck.SetActive(false);

        switch (KickScreenManager.latestKickCause)
        {
            case nameof(KickScreenManager.kickCauses.server_maintenance):
                gameObjectServerMaintenance.SetActive(true);

                maintenanceReasonText.text = KickScreenManager.getKickReason();
                maintenanceExpectedEndTime.text = KickScreenManager.getMaintenanceEstimatedEndTime();
                break;
            case nameof(KickScreenManager.kickCauses.failedGameCheck):
                gameObjectFailedGameCheck.SetActive(true);
                failedGameCheckReasonText.text = KickScreenManager.getKickReason();
                break;
        }
    }

private void Update()
    {
        //Reconnect on touch
        if (!isDisplayTouched)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                isDisplayTouched = true;
                Debug.Log("Trying to reconnect");
                SceneManager.LoadScene(checkGameSceneName);
            }
        }
    }
}