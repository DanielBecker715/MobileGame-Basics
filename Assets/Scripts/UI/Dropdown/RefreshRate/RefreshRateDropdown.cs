using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class RefreshRateDropdown : MonoBehaviour
{
    [Header("Graphic Controller / Where the logic happens")]
    public GraphicController graphicController;

    [Header("Input of a dropdown object / entered object will be filled automatically")]
    public TMP_Dropdown dropdown;

    private Resolution[] allResolutions;

    private int currentResolutionIndex = 0;

    void Start()
    {
        //Autoimport Script
        graphicController = (GraphicController)GameObject.FindObjectOfType(typeof(GraphicController));

        dropdown.AddOptions(getHeartRates());
        dropdown.value = currentResolutionIndex;
        dropdown.RefreshShownValue();

        loadEventListener();
    }

    private List<string> getHeartRates()
    {
        allResolutions = Screen.resolutions;

        dropdown.ClearOptions();


        List<int> heartRates = new List<int>();

        for (int i = 0; i < allResolutions.Length; i++)
        {
            //Current iterating refresh rate
            int refreshRate = allResolutions[i].refreshRate;

            //Check if refreshRate is already in list
            if (!heartRates.Contains(refreshRate))
            {
                heartRates.Add(refreshRate);
            }
        }

        //Sort list ascending
        heartRates.Sort();
        //Reserve list for descending
        heartRates.Reverse();

        List<string> fpsList = new List<string>();
        //Convert int list to string list
        for (int i = 0; i < heartRates.Count; i++)
        {
            fpsList.Add(heartRates[i].ToString() + "Hz");
        }

        //Select current refreshRate in dropdown value 
        for (int i = 0; i < heartRates.Count; i++)
        {
            if (heartRates[i] == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
                break;
            }
        }
        return fpsList;
    }
    private void loadEventListener()
    {
        //Eventlistener / Detects when dropdown value gets changed
        dropdown.onValueChanged.AddListener(delegate
        {
            //Search the number the the dropdown's current value
            int refreshRate = Int32.Parse(Regex.Match(currentSelectedValue(), @"\d+").Value);

            //Changes the heartRate
            graphicController.setRefreshRate(refreshRate);
        });
    }
    private string currentSelectedValue()
    {
        return dropdown.options[dropdown.value].text;
    }
}
