using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class ResolutionDropdown : MonoBehaviour
{
    [Header("Graphic Controller / Where the logic happens")]
    public GraphicController graphicController;

    [Header("Input of a dropdown object / entered object will be filled automatically")]
    public TMP_Dropdown dropdown;

    private Resolution[] allResolution;

    private int currentResolutionIndex = 0;

    void Start()
    {
        //Autoimport Script
        graphicController = (GraphicController)GameObject.FindObjectOfType(typeof(GraphicController));

        dropdown.AddOptions(getResolutions());
        dropdown.value = currentResolutionIndex;
        dropdown.RefreshShownValue();

        loadEventListener();
    }

    private List<string> getResolutions()
    {
        //Setting up Resolutions
        allResolution = Screen.resolutions;
        dropdown.ClearOptions();
        List<string> resolutions = new List<string>();
        for (int i = 0; i < allResolution.Length; i++)
        {
            string option = allResolution[i].width + " x " + allResolution[i].height;
            if (resolutions.Contains(option))
                continue;
            resolutions.Add(option);
            if (allResolution[i].width == Screen.currentResolution.width &&
                    allResolution[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //Reverse list for descending
        resolutions.Reverse();
        return resolutions;
    }
    private void loadEventListener()
    {
        //Eventlistener / Detects when dropdown value gets changed
        dropdown.onValueChanged.AddListener(delegate
        {
            string dropdownValue = currentSelectedValue();
            string[] res = dropdownValue.Split(new string[] { " x " }, StringSplitOptions.None);

            int width = Int32.Parse(res[0]);
            int height = Int32.Parse(res[1]);

            //int numbers = Int32.Parse(Regex.Match(value, @"\d+").Value);

            Debug.Log("RES: " + width + " " + height);
            //Changes resolution
            graphicController.setResolution(width, height);
        });
    }
    private string currentSelectedValue()
    {
        return dropdown.options[dropdown.value].text;
    }
}
