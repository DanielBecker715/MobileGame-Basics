using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SettingsPanel : MonoBehaviour
{
    private List<GameObject> settingsPanels = new List<GameObject>();

    private void Start()
    {
        //EditorGUILayout.HelpBox("", MessageType.Warning);

        settingsPanels.Clear();
        //Autofill's list with children gameobjects
        foreach (Transform child in transform)
        {
            settingsPanels.Add(child.gameObject);
        }
    }

    public void disableAllPanels()
    {
        //Deativates all panels
        for (int i = 0; i < settingsPanels.Count; i++)
        {
            settingsPanels[i].GetComponent<CanvasGroup>().alpha = 0;
            settingsPanels[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
            settingsPanels[i].GetComponent<CanvasGroup>().interactable = false;
        }
    }
    public void enablePanel(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
}
