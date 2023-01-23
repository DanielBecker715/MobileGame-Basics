using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[System.Serializable]
public class PanelManager : MonoBehaviour
{
    public CanvasGroupActivator canvasGroupActivator;

    [Header("Panel List")]
    [Tooltip("First panel will be shown, all others will be hidden with canvas group aplha")]
    public PanelAttributes[] panels;

    [System.Serializable]
    public class PanelAttributes
    {

        public bool activation;
        public bool showCanvasGroup;
        public GameObject menuPanel;
        public Animation animation;
        public AnimationClip cameraAnimation;

        public PanelAttributes(bool activation, bool showCanvasGroup, GameObject menuPanel, Animation animation, AnimationClip cameraAnimation)
        {
            this.activation = activation;
            this.showCanvasGroup = showCanvasGroup;
            this.menuPanel = menuPanel;
            this.animation = animation;
            this.cameraAnimation = cameraAnimation;
        }
    }
    public void LoadInspectorSettings()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].menuPanel.SetActive(panels[i].activation);
            if (!panels[i].showCanvasGroup)
            {
                canvasGroupActivator.disableCanvasGroup(panels[i].menuPanel.GetComponent<CanvasGroup>());
            }
            else
            {
                canvasGroupActivator.enableCanvasGroup(panels[i].menuPanel.GetComponent<CanvasGroup>());
            }
        }
    }

    /// <summary>
    /// Enables the parameter GameObject and makes canvas group to 0
    /// </summary>
    /// <param name="menuPanel"></param>
    public void EnablePanelVisibility(GameObject menuPanel)
    {
        menuPanel.SetActive(true);
        canvasGroupActivator.enableCanvasGroup(menuPanel.GetComponent<CanvasGroup>());
    }
    
    /// <summary>
    /// Disables the parameter GameObject and makes canvas group to 0
    /// </summary>
    /// <param name="menuPanel"></param>
    public void DisablePanelVisibility(GameObject menuPanel)
    {
        menuPanel.SetActive(false);
        canvasGroupActivator.disableCanvasGroup(menuPanel.GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Enables all Panels that are listed in Panelmanager and sets the canvas group to 0
    /// </summary>
    public void EnableAllPanelVisibilitys()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].menuPanel.SetActive(true);
            canvasGroupActivator.enableCanvasGroup(panels[i].menuPanel.GetComponent<CanvasGroup>());
        }
    }

    /// <summary>
    /// Disables all Panels that are listed in Panelmanager and sets the canvas group to 0
    /// </summary>
    public void DisableAllPanelVisibilitys()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].menuPanel.SetActive(false);
            canvasGroupActivator.disableCanvasGroup(panels[i].menuPanel.GetComponent<CanvasGroup>());
        }
    }
}
