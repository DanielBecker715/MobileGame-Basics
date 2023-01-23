using UnityEngine;
using TMPro;
using System.Reflection;

[ExecuteAlways]
public class TextManager : MonoBehaviour
{
    [Header("Language Controller")]
    public LanguageController languageController;

    [Header("Translation texts")]
    //Dont rename or change translation variables
    [SerializeField]
    public string german = "german";
    [SerializeField]
    public string english = "english";
    [SerializeField]
    public string spanish = "spanish";
    [SerializeField]
    public string french = "french";

    [Header("Text Locations")]
    public bool disableHighlightedText = false;

    [Header("Text Locations")]
    public TextMeshProUGUI normalText;
    public TextMeshProUGUI highlightedText;


    void Start()
    {
        //Autoimport Script
        languageController = (LanguageController)GameObject.FindObjectOfType(typeof(LanguageController));

        setNormalText(getTextInCurrentLanguage());
        setHighlightedText(getTextInCurrentLanguage());
    }

    /// <summary>
    /// Returns the correctly translated text in the current language
    /// </summary>
    private string getTextInCurrentLanguage()
    {
        //Gets current language choosed by player
        var currentLanguage = languageController.returnLatestLanguageFromStorage();

        var currentScript = gameObject.GetComponent(typeof(TextManager)) as TextManager;

        //Get all SerializeFields from current script
        FieldInfo[] fields = currentScript.GetType().GetFields();

        foreach (var field in fields)
        {
            if (field.Name == currentLanguage.ToString().ToLower())
            {
                return field.GetValue(currentScript).ToString();
            }
        }
        return string.Empty;
    }


    /// <summary>
    /// Sets UI the texts with a string
    /// </summary>
    private void setNormalText(string text)
    {
        normalText.text = text;
    }


    /// <summary>
    /// Sets UI the texts with a string
    /// </summary>
    private void setHighlightedText(string text)
    {
        if (!disableHighlightedText)
        {
            highlightedText.text = text;
        }
    }


    /// <summary>
    /// Updates the text field with the same value
    /// </summary>
    public void refreshText()
    {
        setNormalText(getTextInCurrentLanguage());
        setHighlightedText(getTextInCurrentLanguage());
    }
}