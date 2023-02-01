using UnityEngine;
using UnityEngine.SceneManagement;

public static class KickScreenManager
{
    public static string getKickScreenName = "KickScreen";

    /// <summary>
    /// Sets the reason (message) why the player was kicked out</returns>
    /// </summary>
    public static void setKickReason(string reason)
    {
        PlayerPrefs.SetString("kickScreenReason", reason);
    }

    /// <returns>Returns the last reason (message) why the player was kicked out</returns>
    public static string getKickReason()
    {
        return PlayerPrefs.GetString("kickScreenReason");
    }

    /// <summary>
    /// Sets the reason estimated end time for the latest maintenance</returns>
    /// </summary>
    public static void setMaintenanceEstimatedEndTime(string time)
    {
        PlayerPrefs.SetString("kickScreenMaintenanceEstimatedEndTime", time);
    }


    /// <returns>Returns the estimated end time from the last maintenance</returns>
    public static string getMaintenanceEstimatedEndTime()
    {
        return PlayerPrefs.GetString("kickScreenMaintenanceEstimatedEndTime");
    }

    /// <summary>
    /// This is the latest cause why the player was kicked out (from the enum kickCauses)
    /// </summary>
    public static string latestKickCause { get; set; }

    public enum kickCauses
    {
        failedGameCheck,
        server_maintenance
    }

    //Load EndScreen / kicks the player
    public static void kickPlayer(string sceneName, kickCauses reason, string kickMessage)
    {
        setKickReason(kickMessage);
        KickScreenManager.latestKickCause = reason.ToString();
        SceneManager.LoadScene(sceneName);
    }
}
