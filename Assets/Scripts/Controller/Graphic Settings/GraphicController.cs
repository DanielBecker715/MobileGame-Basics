using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class GraphicController : MonoBehaviour
{
    [Header("Fullscreen")]
    public Toggle fullscreenToggle;

    [Header("Resolution Dropdown")]
    public Dropdown resolutionDropdown;

    [Header("RefreshRate Dropdown")]
    public Dropdown refreshRateDropdown;

    [Header("Field Of View Slider")]
    public Slider fieldOfViewSlider;

    private void Start()
    {
        //On system start up load settings:

        setFieldOfView(getFieldOfView());
        

        //TODO ADD Eventlistener if user changes full screen or screen size them self

    }

    // ################################
    /* #####    Fullscreen     ##### */
    // ################################


    public void setFullScreen(bool value)
    {
        PlayerPrefs.SetInt("graphicFullscreen", Convert.ToInt32(value));
        Screen.fullScreen = value;

        //Needed for gamestart / Slider gets updates
        fullscreenToggle.isOn = value;
    }
    public float getFullScreenStatus()
    {
        //Must be a playerpref, because at startup all saved settings are loaded
        return PlayerPrefs.GetInt("graphicFieldOfView");
    }

    public void fullScreenEventListener()
    {

    }

    // ################################
    /* #####    Resolutions    ##### */
    // ################################

    public Resolution[] getAllResolutions()
    {
        return Screen.resolutions;
    }

    public Resolution getCurrentResolution()
    {
        //Must be a playerpref, because at startup all saved settings are loaded
        Resolution currentResolution = Screen.currentResolution;

        currentResolution.height = PlayerPrefs.GetInt("graphicResolutionHeight");
        currentResolution.width = PlayerPrefs.GetInt("graphicResolutionWidth");

        return currentResolution;
    }

    public void setResolution(int width, int height)
    {
        int refreshRate = getRefreshRate();

        if (Screen.fullScreen)
        {
            PlayerPrefs.SetInt("graphicResolutionWidth", width);
            PlayerPrefs.SetInt("graphicResolutionHeight", height);


            Screen.SetResolution(width, height, true, refreshRate);
        } else
        {
            Screen.SetResolution(width, height, false, refreshRate);
        }
    }

    // ################################
    /* #####    RefreshRate    ##### */
    // ################################

    public void setRefreshRate(int refreshRate)
    {
        Resolution resolution = getCurrentResolution();
        if (Screen.fullScreen)
        {
            Screen.SetResolution(resolution.width, resolution.height, true, refreshRate);
        } else
        {
            Screen.SetResolution(resolution.width, resolution.height, false, refreshRate);
        }

        PlayerPrefs.SetInt("graphicRefreshRate", refreshRate);

        //Get index by value text
        int index = refreshRateDropdown.options.FindIndex((i) => { return i.text.Equals(refreshRate); });

        refreshRateDropdown.value = index;
    }

    public int getRefreshRate()
    {
        return PlayerPrefs.GetInt("graphicRefreshRate");
    }

    // ################################
    /* #####   Field Of View   ##### */
    // ################################
    public void setFieldOfView(float value)
    {
        PlayerPrefs.SetFloat("graphicFieldOfView", value);
        //mainCamera.fieldOfView = value;

        //Needed for gamestart / Slider gets updates
        fieldOfViewSlider.value = value;
    }
    public float getFieldOfView()
    {
        //Must be a playerpref, because at startup all saved settings are loaded
        return PlayerPrefs.GetFloat("graphicFieldOfView");
    }

}