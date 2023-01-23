using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UICheckGameController : MonoBehaviour
{
    public CheckGameManager checkGame;

    //Scene settings
    private int nextSceneIndex;
    private bool allowLoadingToNextScene = false;

    [Header("FadeIn Settings")]
    [Tooltip("Important to check when Animations are done")]
    public WaitForAnimationTrigger testFadeIn;
    public Animator fadeInAnimator;
    public GameObject fadeInObject;

    [Header("FadeOut Settings")]
    [Tooltip("Important to check when Animations are done")]
    public WaitForAnimationTrigger testFadeOut;
    public Animator fadeOutAnimator;
    public GameObject fadeOutObject;

    [Header("Version Settings")]
    public TextMeshProUGUI VersionText;

    private IEnumerator Start()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(loadAsyncNextScene(nextSceneIndex));
        printVersion();
        yield return StartCoroutine(playFadeOutAnim());

        yield return StartCoroutine(checkGame.checkConnection());
        yield return StartCoroutine(delayGame(0.25F));

        yield return StartCoroutine(checkGame.checkVersion());
        yield return StartCoroutine(delayGame(0.25F));

        yield return StartCoroutine(checkGame.checkServerStatus());
        yield return StartCoroutine(delayGame(0.25F));

        //yield return StartCoroutine(checkGame.checkBan());
        //yield return StartCoroutine(delayGame(1));

        yield return StartCoroutine(playFadeInAnim());
        allowLoadingToNextScene = true;
    }

    //Print game version
    public string printVersion()
    {
        string version = Application.version;
        VersionText.text = version;
        return version;
    }

    private IEnumerator playFadeOutAnim()
    {
        fadeOutObject.SetActive(true);
        fadeOutAnimator.Play("Loadingfadeout", 0, 0.0f);

        while(!testFadeOut.checkAnimTriggerDropped("FadeOut")) {
            yield return null;
        }
        Debug.Log("FadeOut animation done");
    }

    private IEnumerator playFadeInAnim()
    {
        fadeInObject.SetActive(true);
        fadeInAnimator.Play("Loadingfadein", 0, 0.0f);

        while (!testFadeIn.checkAnimTriggerDropped("FadeIn")) {
            yield return null;
        }
        Debug.Log("FadeIn animation done");
    }

    private IEnumerator loadAsyncNextScene(int sceneIndex)
    {
        //Start an asynchronous operation to load the main game scene
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        Debug.Log("Preloading scene: " + SceneManager.GetSceneByBuildIndex(nextSceneIndex).name);

        while (async.progress < 0.9f)
        {
            // Report progress etc.
            string progress = "Progress: " + async.progress.ToString();
            Debug.Log(progress);
            yield return null;
        }

        Debug.Log("Preloading complete -> Waiting for the approval of the next scene");

        while (!allowLoadingToNextScene)
        {
            yield return null;
        }
        Debug.Log("Loading new scene");
        async.allowSceneActivation = true;
    }

    private IEnumerator delayGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
