using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LanguageController : MonoBehaviour
{
    private language defaultLanguage = language.Spanish;

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
    public language returnLatestLanguageFromStorage()
    {
        language lang;

        //if language exists
        if (Enum.TryParse<language>(PlayerPrefs.GetString("currentLanguage"), true, out lang)) {
            return lang;
        }
        return defaultLanguage;
    }

    /// <summary>
    /// Converts a string to an enum language. Upper and lower case is not observed 
    /// </summary>
    /// <returns>Returns the converted input value in an enum if it exists</returns>
    public language convertStringToEnum(string language)
    {
        language lang;

        //if language exists
        if (Enum.TryParse<language>(language, true, out lang))
        {
            return lang;
        }
        throw new NotImplementedException();
    }

    public void saveLanguageToStorage(language language)
    {
        PlayerPrefs.SetString("currentLanguage", language.ToString());
        Debug.Log("Changed language to: " + language);
    }
}
