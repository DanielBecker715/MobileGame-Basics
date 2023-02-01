using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class LanguageController : MonoBehaviour
{
    private language defaultLanguage = language.English;

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

    /// <summary>
    /// If the language has changed something weird happens in the ui (wrong positions).
    /// If the scene gets reloaded everything runs normal again
    /// </summary>
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
