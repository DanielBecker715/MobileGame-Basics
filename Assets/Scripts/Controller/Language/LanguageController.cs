using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LanguageController : MonoBehaviour
{
    private language defaultLanguage = language.English;

    public string[] availableLanguages = new string[]
    {
         language.German.ToString(),
         language.English.ToString(),
         language.Spanish.ToString(),
         language.French.ToString()
    };

    public enum language
    {
        German,
        English,
        Spanish,
        French
    };


    /// <summary>
    /// Searches the playerprefs for the last language used. If not found, the default language is returned
    /// Upper and lower case is not observed 
    /// </summary>
    /// <returns>Returns enum of the current language.</returns>
    public language getLanguageFromStorage()
    {
        language lang;

        //if language exists
        if (Enum.TryParse<language>(PlayerPrefs.GetString("currentLanguage"), true, out lang)) {
            return lang;
        }
        return defaultLanguage;
    }

    /// <summary>
    /// Switches the language by converting a string to an enum language. Upper and lower case is not observed 
    /// </summary>
    /// <returns>Returns the converted input value in an enum if it exists</returns>
    public void switchLanguage(string language)
    {
        language lang;

        //if language exists
        if (!Enum.TryParse<language>(language.ToString(), true, out lang))
        {
            throw new NotImplementedException();
        }
        saveLanguageToStorage(lang);
    }

    public void saveLanguageToStorage(language language)
    {
        PlayerPrefs.SetString("currentLanguage", language.ToString());
        Debug.Log("Changed language to: " + language);
    }

    public void refreshAllTexts()
    {
        TextManager[] allTextManagers = FindObjectsOfType(typeof(TextManager)) as TextManager[];

        foreach (var textManager in allTextManagers)
        {
            textManager.refreshText();
        }
    }
}
