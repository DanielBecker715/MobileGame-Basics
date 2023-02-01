using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    [Header("Map Background")]
    public string mapName;
    public AsyncSceneLoader sceneLoader;

    [Header("Fade Settings")]
    public Animator animator;
    public GameObject fadeBackgroundObject;

    // Start is called before the first frame update
    void Start()
    {
        //Loads all needed scenes
        sceneLoader.addSceneToPreloading(mapName);
        sceneLoader.startPreloading();

        showFadeIn();
    }
    private void showFadeIn()
    {
        fadeBackgroundObject.SetActive(true);
        animator.Play("Loadingfadeout", 0, 0.0f);
    }
}
