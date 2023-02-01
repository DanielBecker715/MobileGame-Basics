using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionMenu : MonoBehaviour
{
    public string homepageUrl = "https://darkvoidstudios.com";
    public string supportEmail = "support@darkvoidstudios.com";
    public string termOfServiceUrl = "https://darkvoidstudios.com/privacy";

    public void openHomepage()
    {
        Application.OpenURL(homepageUrl);
    }

    public void sendEmailToSupport()
    {
        Application.OpenURL("mailto:" + supportEmail);
    }

    public void openTermsOfService()
    {
        Application.OpenURL(termOfServiceUrl);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
