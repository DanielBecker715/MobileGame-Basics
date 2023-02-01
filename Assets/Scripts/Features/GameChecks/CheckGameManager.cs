using Newtonsoft.Json.Linq;
using System.Collections;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Ping = UnityEngine.Ping;

public class CheckGameManager : MonoBehaviour
{
    [Header("General Settings")]
    private string gameName = "mobilegame";

    [Header("Network Settings")]
    public string serverAddress = "darkvoidstudios.com";
    public string urlVersionAPI = "https://api.darkvoidstudios.com/mobilegame/version/index.php";
    public string urlMaintenanceAPI = "https://api.darkvoidstudios.com/maintenance/index.php";
    public int maxPing = 800;

    [Header("Text Settings")]
    public TextMeshProUGUI outputCurrentStep;

    //Error messages
    private string msgVersion = "Wrong Game-Version";
    private string msgConnection = "No Internet-Connection";
    private string msgHighPing = "Your ping is too high:";

    /// <summary>
    /// Automatically loads the "kick screen" scene if one check failed
    /// </summary>
    public IEnumerable initiateAllChecks()
    {
        yield return StartCoroutine(checkConnection());
        yield return StartCoroutine(checkVersion());
        yield return StartCoroutine(checkServerStatus());
        //yield return StartCoroutine(checkBan());
    }


    /// <summary>
    /// Checks the player connection and the average pings
    /// </summary>
    /// <returns>Returns true if the user was banned from the game and false if not</returns>
    public IEnumerator checkConnection()
    {
        Debug.Log("Checking Players Connection");
        outputCurrentStep.SetText("Checking Players Connection");
        //Internet check
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //No Connection
            Debug.Log(msgConnection);
            KickScreenManager.kickPlayer(KickScreenManager.getKickScreenName, KickScreenManager.kickCauses.failedGameCheck, msgConnection);
            yield return false;
        }
        Debug.Log("Connection established");

        int ping = getPing();
        Debug.Log("Ping: "+ping+"ms");
        if (ping > maxPing)
        {
            Debug.Log(msgHighPing+" "+ping+"ms");
            KickScreenManager.kickPlayer(KickScreenManager.getKickScreenName, KickScreenManager.kickCauses.failedGameCheck, msgHighPing + " " + ping + "ms");
        }
    }

    public int getPing()
    {
        IPAddress[] ipaddress = null;
        //Ping check
        try {
            ipaddress = Dns.GetHostAddresses(serverAddress);
        }
        catch
        {
            Debug.LogWarning("Servers are currently down!");
            KickScreenManager.kickPlayer(KickScreenManager.getKickScreenName, KickScreenManager.kickCauses.failedGameCheck, "Servers are currently down!");
            return 0;
        }

        Ping ping = new Ping(ipaddress[0].ToString());
        while (!ping.isDone)
        {
            continue;
        }
        return ping.time;
    }

    /// <summary>
    /// Checks the game version via the darkvoid api.
    /// If client version and server version don't match, the player will be kicked to the "kick screen" scene.
    /// </summary>
    public IEnumerator checkVersion()
    {
        string gameVersion = null;
        Debug.Log("Checking Game-Version");
        outputCurrentStep.SetText("Checking Game-Version");

        using (UnityWebRequest request = UnityWebRequest.Get(urlVersionAPI))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Server response:" + request.downloadHandler.text);
                gameVersion = request.downloadHandler.text;
            } else {
                Debug.Log(request.error);
                KickScreenManager.kickPlayer(KickScreenManager.getKickScreenName, KickScreenManager.kickCauses.failedGameCheck, request.error);
            }
            yield return request;
        }

        if (gameVersion != Application.version.ToString()) {
            KickScreenManager.kickPlayer(KickScreenManager.getKickScreenName, KickScreenManager.kickCauses.failedGameCheck, msgVersion);
        } else {
            Debug.Log("Game is up to date");
        }
    }

    /// <summary>
    /// Creates a request to the darkvoidstudios maintenance api and checks the availability of all services
    /// </summary>
    /// <returns></returns>
    public IEnumerator checkServerStatus()
    {
        JArray jsonResponse = null;
        Debug.Log("Checking Server Status");
        outputCurrentStep.SetText("Checking Server Status");

        using (UnityWebRequest request = UnityWebRequest.Get(urlMaintenanceAPI))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Server response:" + request.downloadHandler.text);
                jsonResponse = JArray.Parse(request.downloadHandler.text);
                //jsonResponse = JsonUtility.FromJson<JArray>(request.downloadHandler.text);
            }
            else
            {
                Debug.Log(request.error);
                KickScreenManager.kickPlayer(KickScreenManager.getKickScreenName, KickScreenManager.kickCauses.failedGameCheck, request.error);
            }
            yield return request;
        }

        //loops through every json array
        foreach (JObject item in jsonResponse)
        {
            string maintenance_location = item.GetValue("maintenance_location").ToString();
            if (maintenance_location == gameName)
            {
                string estimated_end_time = item.GetValue("estimated_end_time").ToString();
                string reason = item.GetValue("reason").ToString();

                KickScreenManager.setKickReason(reason);
                KickScreenManager.setMaintenanceEstimatedEndTime(estimated_end_time);

                string message =
                    "Found a maintenance!\n" +
                    "Estimated End Time: " + estimated_end_time + "\n" +
                    "Reason: " + reason + "\n"
                    ;
                Debug.LogWarning(message);
                KickScreenManager.kickPlayer(KickScreenManager.getKickScreenName, KickScreenManager.kickCauses.server_maintenance, KickScreenManager.getKickReason());
                break;

            }
        }
    }
}
