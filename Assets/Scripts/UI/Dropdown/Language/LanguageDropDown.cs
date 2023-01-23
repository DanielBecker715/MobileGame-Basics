using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class LanguageDropDown : MonoBehaviour
{
    [Header("Language Controller")]
    public LanguageController languageController;

    [Header("Input of a dropdown object / entered object will be filled automatically")]
    public TMP_Dropdown dropdown;

    private void Start()
    {
        //Autoimport Script
        languageController = (LanguageController)GameObject.FindObjectOfType(typeof(LanguageController));

        prepareDropDown();
        loadEventListener();
    }

    private void prepareDropDown()
    {
        //Gets all languages from enum and convert it into List
        List<string> allLanguages = new List<string>(System.Enum.GetNames(typeof(LanguageController.language)));

        //Removes all values from dropdown menu
        dropdown.options.RemoveRange(0, dropdown.options.Count);

        //Adds all languages to dropdown
        dropdown.AddOptions(allLanguages);

        //Select saved lang on startup
        for (int i = 0; i < allLanguages.Count; i++)
        {
            if (PlayerPrefs.GetString("currentLanguage").ToLower() == allLanguages[i].ToLower()) {
                dropdown.value = i;
                break;
            }
        }
    }

    public void loadEventListener()
    {
        //Eventlistener / Detects when dropdown value gets changed
        dropdown.onValueChanged.AddListener(delegate
        {
            languageController.saveLanguageToStorage(languageController.convertStringToEnum(currentSelectedValue()));

            TextManager[] allTextManagers = FindObjectsOfType(typeof(TextManager)) as TextManager[];

            foreach(var textManager in allTextManagers)
            {
                textManager.refreshText();
            }
        });
    }

    private string currentSelectedValue()
    {
        return dropdown.options[dropdown.value].text;
    }
}
